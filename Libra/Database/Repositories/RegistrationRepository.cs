using Libra.Database.Interfaces;
using Libra.Models.RegistrationModels;

namespace Libra.Database.Repositories;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly LibraContext _context;

    public RegistrationRepository(LibraContext context)
    {
        _context = context;
    }

    public Registration CreateRegistration(Registration registration)
    {
        _context.Add(registration);
        _context.SaveChanges();

        return registration;
    }

    public Registration? GetByUserId(Guid id)
    {
        return _context.Registrations
            .FirstOrDefault(x => x.UserId == id);
    }
}