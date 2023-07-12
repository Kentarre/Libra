using Libra.Models;
using Libra.Models.BookModels;

namespace Libra.Database.Interfaces;

public interface IBookRepository
{
    public Book? GetById(Guid id);
    public List<Book> GetByOwnerId(Guid id);
    public List<Book> GetByBookName(string bookName);
    public List<Book> GetByBookAuthor(string author);
    public List<Book> GetAll();
    public Book AddBook(Book book);
    public List<Book> GetBooksGroup(List<Guid> bookIds);
}