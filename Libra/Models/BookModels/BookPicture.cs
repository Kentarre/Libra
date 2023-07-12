using System.ComponentModel.DataAnnotations.Schema;

namespace Libra.Models.BookModels;

[Table("book_pictures")]
public class BookPicture
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("book_id")]
    public Guid BookId { get; set; }
    [Column("file_path")]
    public string FilePath { get; set; }
}