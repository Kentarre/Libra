using Libra.Database.Interfaces;
using Libra.Models.BookModels;
using Microsoft.EntityFrameworkCore;

namespace Libra.Database.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraContext _context;

    public BookRepository(LibraContext context)
    {
        _context = context;
    }
    
    public Book? GetById(Guid id)
    {
        return _context.Books
            .Include(x => x.Rents)
            .Include(x => x.Pictures)
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Book> GetByOwnerId(Guid id)
    {
        return _context.Books
            .Include(x => x.Rents)
            .Include(x => x.Pictures)
            .Where(x => x.OwnerId == id)
            .ToList();
    }

    public List<Book> GetByBookName(string bookName)
    {
        return _context.Books
            .Include(x => x.Rents)
            .Include(x => x.Pictures)
            .Where(x => x.BookName == bookName)
            .ToList();
    }

    public List<Book> GetByBookAuthor(string author)
    {
        return _context.Books
            .Include(x => x.Rents)
            .Include(x => x.Pictures)
            .Where(x => x.Author == author)
            .ToList();
    }

    public List<Book> GetAll()
    {
        return _context.Books
            .Include(x => x.Rents)
            .ToList();
    }
 
    public Book AddBook(Book book)
    {
        _context.Add(book);
        _context.SaveChanges();

        return book;
    }

    public List<Book> GetBooksGroup(List<Guid> bookIds)
    {
        return _context.Books
            .Include(x => x.Rents)
            .Where(e => bookIds.Contains(e.Id))
            .ToList();
    }
}