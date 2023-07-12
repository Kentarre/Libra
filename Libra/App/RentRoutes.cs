using Libra.Handlers;
using Libra.Handlers.Interfaces;
using Libra.Models;
using Libra.Models.RentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Libra.App;

public static class RentRoutes
{
    public static WebApplication SetRentRoutes(this WebApplication app)
    {
        app.MapGet("/rent/{id}", [Authorize] (Guid id, IRentHandler handler) => handler.HandleGetById(id));
        app.MapGet("/rent/book/{id}", [Authorize] (Guid id, IRentHandler handler) => handler.HandleGetById(id));
        app.MapGet("/rent/borrower/{id}", [Authorize] (Guid id, IRentHandler handler) => handler.HandleGetByBorrowerId(id));
        app.MapGet("/rent/confirm/{id}", [Authorize] (Guid id, IRentHandler handler) => handler.HandleConfirmRent(id));
        app.MapGet("/rent/stop/{id}", [Authorize] (Guid id, IRentHandler handler) => handler.HandleStopRent(id));

        app.MapPost("/rent", [Authorize] ([FromBody] AddRentModel body, IRentHandler handler) => handler.HandleAddRent(body));
        return app;
    }
}