using Libra.Models;
using Libra.Models.RentModels;

namespace Libra.Services.Interfaces;

public interface IRentService
{
    public Rent GetById(Guid id);
    public List<Rent> GetByBookId(Guid id);
    public List<Rent> GetByBorrowerId(Guid id);
    public Rent AddRent(AddRentModel model);
    public Rent ConfirmRent(Guid id);
    public Rent StopRent(Guid id);
}