using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Libra.Database.Interfaces;
using Libra.Exeptions;
using Libra.Models;
using Libra.Models.AuthModels;
using Libra.Models.RegistrationModels;
using Libra.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Libra.Services;

public class RegistrationService : IRegistrationService
{
    private const int MonthInMinutes = 43800;
    private const int AccessTokenExpInMinutes = 120;
    
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IUserService _userService;
    private readonly JwtConfig _jwtConfig;

    public RegistrationService(IUserService userService, IRegistrationRepository registrationRepository, IOptions<AppConfig> appConfig)
    {
        _userService = userService;
        _registrationRepository = registrationRepository;
        _jwtConfig = appConfig.Value.JwtConfig;
    }

    public RegistrationResult CreateRegistration(Guid userId, string password)
    {
        var registrationData = new Registration
        {
            UserId = userId,
            Salt = GenerateSalt(256),
            CreatedOn = DateTime.UtcNow
        };

        registrationData.PasswordHash = GenerateHash(registrationData.Salt, password);

        var registration = _registrationRepository.CreateRegistration(registrationData);

        var authResult = DoAuthorize(registration, password);

        return new RegistrationResult
        {
            AccessToken = authResult.AccessToken,
            RefreshToken = authResult.RefreshToken
        };
    }

    public AuthResult Authorize(string email, string password)
    {
        var user = _userService.GetByEmail(email);
        var registration = GetByUserId(user.Id);

        if (registration == null)
            throw new NotAuthorizedException();

        return DoAuthorize(registration, password);
    }

    public Registration GetByUserId(Guid id)
    {
        var registration =  _registrationRepository.GetByUserId(id);

        if (registration == null)
            throw new NotAuthorizedException();

        return registration;
    }

    private AuthResult DoAuthorize(Registration registration, string password)
    {
        var passwordHash = GenerateHash(registration.Salt, password);

        if (registration.PasswordHash != passwordHash)
            throw new NotAuthorizedException();

        return new AuthResult
        {
            AccessToken = GenerateToken(registration, AccessTokenExpInMinutes),
            RefreshToken = GenerateToken(registration, MonthInMinutes)
        };
    }

    private string GenerateSalt(int saltLimit)
    {
        var rnd = new RNGCryptoServiceProvider();
        var byteArray = new Byte[saltLimit];
        rnd.GetNonZeroBytes(byteArray);

        return Convert.ToBase64String(byteArray);
    }

    private string GenerateHash(string salt, string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = Encoding.UTF8.GetBytes(salt)
            .Concat(passwordBytes)
            .ToArray();

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(saltedPassword);

        return Convert.ToBase64String(hash);
    }

    private string GenerateToken(Registration registration, int expiryTime)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtConfig.Key);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("UserId", registration.UserId.ToString()),
        };

        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expiryTime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}