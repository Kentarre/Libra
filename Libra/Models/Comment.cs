using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;

namespace Libra.Models;

public class Comment
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string Text { get; set; }
}