using Libra.Handlers.Interfaces;

namespace Libra.App;

public static class ChatRoutes
{
    public static WebApplication SetChatRoutes(this WebApplication app)
    {
        app.MapGet("/messages/{chatId}", (Guid chatId, IChatHandler handler) => handler.GetMessages(chatId));
       
        return app;
    }
}