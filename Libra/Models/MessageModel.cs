namespace Libra.Models;

public class MessageModel
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set;  }
    public string Name { get; set; }
    public string Message { get; set; }
}