using System.Net;
using Libra.Handlers.Interfaces;
using Libra.Models;
using Libra.Models.RentModels;
using Libra.Services.Interfaces;

namespace Libra.Handlers;

public class RentHandler : IRentHandler
{
    private readonly IRentService _rentService;

    public RentHandler(IRentService rentService)
    {
        _rentService = rentService;
    }
    
    public IResult HandleGetById(Guid id)
    {
        try
        {
            return Results.Json(_rentService.GetById(id));
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

    public IResult HandleGetByBookId(Guid id)
    {
        try
        {
            return Results.Json(_rentService.GetByBookId(id));
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

    public IResult HandleGetByBorrowerId(Guid id)
    {
        try
        {
            return Results.Json(_rentService.GetByBorrowerId(id));
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

    public IResult HandleAddRent(AddRentModel model)
    {
        try
        {
            var user = _rentService.AddRent(model);
            return Results.Json(user);
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

    public IResult HandleConfirmRent(Guid id)
    {
        try
        {
            return Results.Json(_rentService.ConfirmRent(id));
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

    public IResult HandleStopRent(Guid id)
    {
        try
        {
            return Results.Json(_rentService.StopRent(id));
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