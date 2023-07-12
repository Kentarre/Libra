using Libra.Models.BookModels;

namespace Libra.Handlers.Interfaces;

public interface IBookHandler
{
    public IResult GetAvailableBooks();
    public IResult HandleGetById(Guid id);
    public IResult HandleGetByOwnerId(Guid id);
    public IResult HandleGetByBookName(string bookName);
    public IResult HandleGetByBookAuthor(string author);
    public IResult HandleAddBook(AddBookModel book);
    public IResult HandleGetBooksGroup(List<Guid> bookIds);
}