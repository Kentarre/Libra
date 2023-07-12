using Libra.Helpers;
using Libra.Models;
using Libra.Services.Interfaces;
using MongoDB.Bson;

namespace Libra.Services;

using MongoDB.Driver;

public class CommentsService : ICommentsService
{
    private readonly IMongoDatabase _database;
    private readonly IUserService _userService;
    private readonly Func<Guid, string> _getKey = bookId => $"comment:{bookId.ToString()}";
    
    public CommentsService(IUserService userService, IMongoService mongoService)
    {
        _userService = userService;
        _database = mongoService.GetDataBase("comments");
    }

    public Guid AddComment(Comment model)
    {
        var name = _userService.GetNameById(model.UserId);
        var bookCommentsCollection = _database.CreateOrGetCollection<BsonDocument>(_getKey(model.BookId));
        var comment = new BsonDocument
        {
            { "author", name },
            { "content", model.Text },
            { "createdAt", DateTime.UtcNow.ToString("f") }
        };
        
        bookCommentsCollection.InsertOne(comment);

        return model.BookId;
    }

    public List<dynamic> GetComments(Guid bookId)
    {
        var bookCommentsCollection = _database.GetCollection<dynamic>(_getKey(bookId));
        var filter = Builders<dynamic>.Filter.Empty;
        
        return bookCommentsCollection.Find(filter).ToList();
    }
}