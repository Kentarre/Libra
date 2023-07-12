using Libra.Services.Interfaces;
using MongoDB.Driver;

namespace Libra.Services;

public class MongoService : IMongoService
{
    private readonly IConfiguration _config;

    public MongoService(IConfiguration config)
    {
        _config = config;
    }

    public IMongoDatabase GetDataBase(string database)
    {
        return new MongoClient(_config.GetConnectionString("MongoDB"))
            .GetDatabase(database);
    }
}