using Libra.Models;
using Libra.Models.AuthModels;
using Libra.Models.RegistrationModels;

namespace Libra.Services.Interfaces;

public interface IRegistrationService
{
    public RegistrationResult CreateRegistration(Guid userId, string password);
    public AuthResult Authorize(string email, string password);
    public Registration GetByUserId(Guid id);
}