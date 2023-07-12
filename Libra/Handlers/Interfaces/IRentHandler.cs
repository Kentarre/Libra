using Libra.Models.RentModels;

namespace Libra.Handlers.Interfaces;

public interface IRentHandler
{
    public IResult HandleGetById(Guid id);
    public IResult HandleGetByBookId(Guid id);
    public IResult HandleGetByBorrowerId(Guid id);
    public IResult HandleAddRent(AddRentModel model);
    public IResult HandleConfirmRent(Guid id);
    public IResult HandleStopRent(Guid id);
}