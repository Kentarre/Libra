using Libra.Helpers;
using Libra.Models;
using Libra.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Libra.Hubs;

public class MessengerHub : Hub
{
    private readonly IMongoDatabase _database;
    private readonly Func<Guid, string> _getKey = chatId => $"chat:{chatId.ToString()}";

    public MessengerHub(IMongoService mongoService)
    {
        _database = mongoService.GetDataBase("chats");
    }

    public async Task SetChat(string chatId)
    {
        Context.Items.Add(new KeyValuePair<object, object?>(Context.ConnectionId, chatId));
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }
    
    public async Task OnSend(MessageModel model)
    {
        var now = DateTime.UtcNow.ToString("F");
        
        var messageToSend = new
        {
            UserId = model.UserId,
            SenderName = model.Name,
            Message = model.Message,
            SentAt = now
        };
        
        var chatsCollection = _database.CreateOrGetCollection<BsonDocument>(_getKey(model.ChatId));
        var message = new BsonDocument
        {
            { "userId", messageToSend.UserId },
            { "senderName", messageToSend.SenderName },
            { "message", messageToSend.Message },
            { "sentAt", now }
        };
        
        await chatsCollection.InsertOneAsync(message);
        
        await Clients.Group(model.ChatId.ToString()).SendAsync("ReceiveMessage", messageToSend);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var chatId = Context.Items.FirstOrDefault(x => x.Key.ToString() == connectionId);

        if (chatId.Key == null) 
            return base.OnDisconnectedAsync(exception);
        
        Groups.RemoveFromGroupAsync(connectionId, chatId.Value!.ToString());
        Context.Items.Remove(connectionId);

        return base.OnDisconnectedAsync(exception);
    }
}