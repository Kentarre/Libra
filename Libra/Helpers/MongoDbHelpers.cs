using MongoDB.Bson;
using MongoDB.Driver;

namespace Libra.Helpers;

public static class MongoDbHelpers
{
    public static IMongoCollection<T> CreateOrGetCollection<T>(this IMongoDatabase db, string collectionName)
    { 
        var filter = new BsonDocument("name", collectionName);
        var options = new ListCollectionNamesOptions { Filter = filter };

        if (db.ListCollectionNames(options).Any())
            return db.GetCollection<T>(collectionName);

        db.CreateCollection(collectionName);
        
        return db.GetCollection<T>(collectionName);;
    }
}