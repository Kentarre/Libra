using Libra.Database.Interfaces;
using Libra.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace Libra.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LibraContext _context;

    public UserRepository(LibraContext context)
    {
        _context = context;
    }
    
    public User? GetById(Guid id)
    {
        return _context.Users
            .Include(x => x.RentedBooks)
            .Include(x => x.Books)
            .ThenInclude(x => x.Rents)
            .Include(x => x.Books)
            .ThenInclude(x => x.Pictures)
            .FirstOrDefault(x => x.Id == id);
    }

    public string GetNameById(Guid userId)
    {
        return _context.Users.FirstOrDefault(x => x.Id == userId)?.Name ?? "";
    }

    public User? GetByEmail(string email)
    {
        return _context.Users
            .Include(x => x.RentedBooks)
            .Include(x => x.Books)
            .ThenInclude(x => x.Rents)
            .Include(x => x.Books)
            .ThenInclude(x => x.Pictures)
            .FirstOrDefault(x => x.Email == email);
    }

    public User? GetByUsername(string username)
    {
        return _context.Users
            .Include(x => x.RentedBooks)
            .Include(x => x.Books)
            .ThenInclude(x => x.Rents)
            .Include(x => x.Books)
            .ThenInclude(x => x.Pictures)
            .FirstOrDefault(x => x.Username == username);
    }

    public User AddUser(User user)
    {
        _context.Add(user);
        _context.SaveChanges();

        return user;
    }
}