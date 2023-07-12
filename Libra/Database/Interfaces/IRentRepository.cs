using Libra.Models;
using Libra.Models.RentModels;

namespace Libra.Database.Interfaces;

public interface IRentRepository
{
    public Rent? GetById(Guid id);
    public List<Rent> GetByBookId(Guid id);
    public List<Rent> GetByBorrowerId(Guid id);
    public Rent AddRent(Rent rent);
    public Rent? ConfirmRent(Guid id);
    public Rent? StopRent(Guid id);
}