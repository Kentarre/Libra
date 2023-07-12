using Libra.Handlers;
using Libra.Handlers.Interfaces;
using Libra.Models.BookModels;
using Microsoft.AspNetCore.Authorization;

namespace Libra.App;

public static class BookRoutes
{
    public static WebApplication SetBookRoutes(this WebApplication app)
    {
        app.MapGet("/books", [Authorize] (IBookHandler handler) => handler.GetAvailableBooks());
        app.MapGet("/book/{id}", [Authorize] (Guid id, IBookHandler handler) => handler.HandleGetById(id));
        app.MapGet("/book/author/{author}", [Authorize] (string author, IBookHandler handler) => handler.HandleGetByBookAuthor(author));
        app.MapGet("/book/bookname/{bookname}",  [Authorize](string bookname, IBookHandler handler) => handler.HandleGetByBookName(bookname));
        app.MapGet("/book/owner/{id}", [Authorize] (Guid id, IBookHandler handler) => handler.HandleGetByOwnerId(id));

        // [FromForm] binding is not supported in MinimalAPI yet,
        // so we have to bind form to model manually from Request context
        app.MapPost("/book",
            [Authorize](HttpContext context, IBookHandler handler) =>
                handler.HandleAddBook(new AddBookModel(context.Request.Form)));
        app.MapPost("/books", [Authorize] (List<Guid> bookIds, IBookHandler handler) => handler.HandleGetBooksGroup(bookIds));
        return app;
    }
}