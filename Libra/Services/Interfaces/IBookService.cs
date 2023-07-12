using Libra.Models;
using Libra.Models.BookModels;

namespace Libra.Services.Interfaces;

public interface IBookService
{
    public List<Book> GetAvailableBooks();
    public Book GetById(Guid id);
    public List<Book> GetByOwnerId(Guid id);
    public List<Book> GetByBookName(string bookName);
    public List<Book> GetByBookAuthor(string author);
    public Book AddBook(AddBookModel model);
    public List<Book> GetBooksGroup(List<Guid> bookIds);
}