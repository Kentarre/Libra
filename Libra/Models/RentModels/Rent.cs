using System.ComponentModel.DataAnnotations.Schema;

namespace Libra.Models.RentModels;

[Table("rent")]
public class Rent
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("book_id")]
    public Guid BookId { get; set; }
    [Column("borrower_id")]
    public Guid BorrowerId { get; set; }
    [Column("started")]
    public DateTime Started { get; set; }
    [Column("ended")]
    public DateTime? Ended { get; set; }
    [Column("confirmed")]
    public DateTime? Confirmed { get; set; }
    [Column("chat_id")]
    public Guid? ChatId { get; set; }
}