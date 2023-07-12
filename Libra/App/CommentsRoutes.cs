using Libra.Handlers.Interfaces;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libra.App;

public static class CommentsRoutes
{
    public static WebApplication SetCommentsRoutes(this WebApplication app)
    {
        app.MapPost("/comments", ([FromBody] Comment comment, ICommentsHandler handler) => handler.AddComment(comment));
        app.MapGet("/comments/{id}", (Guid id, ICommentsHandler handler) => handler.GetComments(id));
        return app;
    }
}