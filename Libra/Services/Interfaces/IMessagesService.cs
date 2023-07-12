using Libra.Models;

namespace Libra.Services.Interfaces;

public interface IMessagesService
{
    public List<dynamic> GetMessages(Guid chatId);
}