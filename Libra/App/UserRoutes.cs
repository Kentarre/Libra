using Libra.Handlers;
using Libra.Handlers.Interfaces;
using Libra.Models;
using Libra.Models.AuthModels;
using Libra.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libra.App;

public static class UserRoutes
{
    public static WebApplication SetUserRoutes(this WebApplication app)
    {
        app.MapGet("/user/{id}", [Authorize] (Guid id, IUserHandler handler) => handler.HandleGetById(id));
        app.MapGet("/user/email/{email}", [Authorize] (string email, IUserHandler handler) => handler.HandleGetByEmail(email));
        app.MapGet("/user/username/{username}", [Authorize] (string username, IUserHandler handler) => handler.HandleGetByUsername(username));

        app.MapPost("/user/authorize", [AllowAnonymous] ([FromBody] AuthModel model, IUserHandler handler) => handler.HandleAuthorize(model.Email, model.Password));
        app.MapPost("/user", [AllowAnonymous] ([FromBody] AddUserModel body, IUserHandler handler) => handler.HandleAddUser(body));
        return app;
    }
}