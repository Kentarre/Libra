using Libra.Models;
using Libra.Services.Interfaces;
using MongoDB.Driver;

namespace Libra.Services;

public class MessagesService : IMessagesService
{
    private readonly IMongoDatabase _database;
    private readonly Func<Guid, string> _getKey = chatId => $"chat:{chatId.ToString()}";

    public MessagesService(IMongoService service)
    {
        _database = service.GetDataBase("chats");
    }

    public List<dynamic> GetMessages(Guid chatId)
    {
        var chatsCollection = _database.GetCollection<dynamic>(_getKey(chatId));
        var filter = Builders<dynamic>.Filter.Empty;
        
        return chatsCollection.Find(filter).ToList();
    }
}