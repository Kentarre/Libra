using System.Net;
using Libra.Exeptions;
using Libra.Handlers.Interfaces;
using Libra.Models.UserModels;
using Libra.Services.Interfaces;

namespace Libra.Handlers;

public class UserHandler : IUserHandler
{
    private readonly IUserService _service;
    private readonly IRegistrationService _registrationService;
    
    public UserHandler(IUserService service, IRegistrationService registrationService)
    {
        _service = service;
        _registrationService = registrationService;
    }

    public IResult HandleGetById(Guid id)
    {
        try
        {
            return Results.Json(_service.GetUserById(id));
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest, 
                error = e.Message
            });
        }
    }

    public IResult HandleGetByEmail(string email)
    {
        try
        {
            return Results.Json(_service.GetByEmail(email));
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest, 
                error = e.Message
            });
        }
    }

    public IResult HandleAddUser(AddUserModel model)
    {
        try
        {
            var user = _service.AddUser(model);
            var registrationResult = _registrationService.CreateRegistration(user.Id, model.Password);
            
            return Results.Json(new
            {
                user,
                accessToken = registrationResult.AccessToken,
                refreshToken = registrationResult.RefreshToken
            });
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest, 
                error = e.Message
            });
        }
    }

    public IResult HandleGetByUsername(string username)
    {
        try
        {
            return Results.Json(_service.GetByUsername(username));
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest, 
                error = e.Message
            });
        }
    }

    public IResult HandleAuthorize(string email, string password)
    {
        try
        {
            var authResult = _registrationService.Authorize(email, password);
            
            return Results.Json(new
            {
                accessToken = authResult.AccessToken,
                refreshToken = authResult.RefreshToken
            });
        }
        catch (NotAuthorizedException)
        {
            return Results.Unauthorized();
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest,
                error = e.Message
            });
        }
    }
}