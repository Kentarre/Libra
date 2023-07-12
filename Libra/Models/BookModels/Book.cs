using System.ComponentModel.DataAnnotations.Schema;
using Libra.Models.RentModels;

namespace Libra.Models.BookModels;

[Table("books")]
public class Book
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("owner_id")]
    public Guid OwnerId { get; set; }
    [Column("book_name")]
    public string BookName { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("author")]
    public string Author { get; set; }
    [Column("publishing_date")]
    public int? PublishingDate { get; set; }

    public List<BookPicture> Pictures { get; set; } = new();
    public List<Rent> Rents { get; set; } = new ();
    
    public bool IsAvailable => BookIsAvailable();

    private bool BookIsAvailable()
    {
        var rentsInProcessList = Rents.Where(x => x.Confirmed.HasValue && x.Ended != null).ToList();
        return Rents.Count == 0 || rentsInProcessList.Count > 0;
    }
}