using Libra.Database.Interfaces;
using Libra.Models;
using Libra.Models.RentModels;
using Libra.Services.Interfaces;

namespace Libra.Services;

public class RentService : IRentService
{
    private readonly IRentRepository _rentRepository;

    public RentService(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }
    
    public Rent GetById(Guid id)
    {
        var rent = _rentRepository.GetById(id);

        if (rent == null)
            throw new Exception($"Rent with id: {id} not found");

        return rent;
    }

    public List<Rent> GetByBookId(Guid id)
    {
        return _rentRepository.GetByBookId(id);
    }

    public List<Rent> GetByBorrowerId(Guid id)
    {
        return _rentRepository.GetByBorrowerId(id);
    }

    public Rent AddRent(AddRentModel model)
    {
        if (model.BookId == null)
            throw new Exception("BookId can't be empty");
        if (model.BorrowerId == null)
            throw new Exception("BorrowerId can't be empty");

        var newRent = new Rent
        {
            BookId = model.BookId,
            BorrowerId = model.BorrowerId,
            Started = DateTime.UtcNow,
        };

        return _rentRepository.AddRent(newRent);
    }

    public Rent ConfirmRent(Guid id)
    {
        var rent = _rentRepository.ConfirmRent(id);

        if (rent == null)
            throw new Exception($"Rent to modify with id: {id} not found");

        return rent;
    }

    public Rent StopRent(Guid id)
    {
        var rent = _rentRepository.StopRent(id);

        if (rent == null)
            throw new Exception($"Rent to modify with id: {id} not found");

        return rent;
    }
}