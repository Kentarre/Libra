namespace Libra.Models.RentModels;

public class AddRentModel
{
    public Guid BookId { get; set; }
    public Guid BorrowerId { get; set; }
}