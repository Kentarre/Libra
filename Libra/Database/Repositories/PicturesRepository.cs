using Libra.Database.Interfaces;
using Libra.Models.BookModels;

namespace Libra.Database.Repositories;

public class PicturesRepository : IPicturesRepository
{
    private readonly LibraContext _context;

    public PicturesRepository(LibraContext context)
    {
        _context = context;
    }
    
    public BookPicture AddPicture(BookPicture bookPicture)
    {
        _context.Add(bookPicture);
        _context.SaveChanges();

        return bookPicture;
    }
}