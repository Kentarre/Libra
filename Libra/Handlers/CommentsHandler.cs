using System.Net;
using Libra.Handlers.Interfaces;
using Libra.Models;
using Libra.Services.Interfaces;

namespace Libra.Handlers;

public class CommentsHandler : ICommentsHandler
{
    private readonly ICommentsService _commentsService;

    public CommentsHandler(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }

    public IResult GetComments(Guid id)
    {
        try
        {
            return Results.Json(_commentsService.GetComments(id));
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

    public IResult AddComment(Comment model)
    {
        try
        {
            return Results.Json(_commentsService.AddComment(model));
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