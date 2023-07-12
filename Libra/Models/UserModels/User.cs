using System.ComponentModel.DataAnnotations.Schema;
using Libra.Models.BookModels;
using Libra.Models.RentModels;

namespace Libra.Models.UserModels;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("rating")]
    public double Rating { get; set; }
    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
    [Column("profile_picture")]
    public string? ProfilePicture { get; set; }

    public List<Book> Books { get; set; } = new();
    public List<Rent> RentedBooks { get; set; } = new();
}