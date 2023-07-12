using System.Net;
using Libra.Handlers.Interfaces;
using Libra.Models.BookModels;
using Libra.Services.Interfaces;

namespace Libra.Handlers;

public class BookHandler : IBookHandler
{
    private readonly IBookService _service;

    public BookHandler(IBookService service)
    {
        _service = service;
    }

    public IResult GetAvailableBooks()
    {
        try
        {
            return Results.Json(_service.GetAvailableBooks());
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

    public IResult HandleGetById(Guid id)
    {
        try
        {
            return Results.Json(_service.GetById(id));
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

    public IResult HandleGetByOwnerId(Guid id)
    {
        try
        {
            return Results.Json(_service.GetByOwnerId(id));
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

    public IResult HandleGetByBookName(string bookName)
    {
        try
        {
            return Results.Json(_service.GetByBookName(bookName));
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

    public IResult HandleGetByBookAuthor(string author)
    {
        try
        {
            return Results.Json(_service.GetByBookAuthor(author));
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

    public IResult HandleAddBook(AddBookModel model)
    {
        try
        {
            var book = _service.AddBook(model);
            return Results.Json(book);
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

    public IResult HandleGetBooksGroup(List<Guid> bookIds)
    {
        try
        {
            return Results.Json(_service.GetBooksGroup(bookIds));
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