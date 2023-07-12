using System.ComponentModel.DataAnnotations.Schema;

namespace Libra.Models.RegistrationModels;

[Table("registrations")]
public class Registration
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("user_id")]
    public Guid UserId { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    [Column("salt")]
    public string Salt { get; set; }
    [Column("created_on")]
    public DateTime CreatedOn { get; set; }
}