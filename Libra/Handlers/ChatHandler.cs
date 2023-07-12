using System.Net;
using Libra.Handlers.Interfaces;
using Libra.Models;
using Libra.Services.Interfaces;

namespace Libra.Handlers;

public class ChatHandler : IChatHandler
{
    private readonly IMessagesService _messagesService;

    public ChatHandler(IMessagesService messagesService)
    {
        _messagesService = messagesService;
    }

    public IResult GetMessages(Guid chatId)
    {
        try
        {
            return Results.Json(_messagesService.GetMessages(chatId));
        }
        catch (Exception e)
        {
            return Results.BadRequest(new
            {
                status = HttpStatusCode.BadRequest, 
                error = e.Message
            });
        }
    }
}