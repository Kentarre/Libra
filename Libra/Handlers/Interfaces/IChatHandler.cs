using Libra.Models;

namespace Libra.Handlers.Interfaces;

public interface IChatHandler
{
    public IResult GetMessages(Guid chatId);
}