using Libra.Database.Interfaces;
using Libra.Models.RentModels;

namespace Libra.Database.Repositories;

public class RentRepository : IRentRepository
{
    private readonly LibraContext _context;

    public RentRepository(LibraContext context)
    {
        _context = context;
    }
    
    public Rent? GetById(Guid id)
    {
        return _context.Rents
            .FirstOrDefault(x => x.Id == id);
    }

    public List<Rent> GetByBookId(Guid id)
    {
        return _context.Rents
            .Where(x => x.BookId == id)
            .ToList();
    }

    public List<Rent> GetByBorrowerId(Guid id)
    {
        return _context.Rents
            .Where(x => x.BorrowerId == id)
            .ToList();
    }

    public Rent AddRent(Rent rent)
    {
        _context.Add(rent);
        _context.SaveChanges();

        return rent;
    }

    public Rent? ConfirmRent(Guid id)
    {
        var rent = GetById(id);

        if (rent == null)
            return null;

        if (rent.Confirmed != null)
            throw new Exception("");
        
        rent.Confirmed = DateTime.UtcNow;

        _context.Update(rent);
        _context.SaveChanges();

        return rent;
    }

    public Rent? StopRent(Guid id)
    {
        var rent = GetById(id);

        if (rent == null)
            return null;

        if (rent.Ended != null)
            throw new Exception("");
        
        rent.Ended = DateTime.UtcNow;

        _context.Update(rent);
        _context.SaveChanges();

        return rent;
    }
}