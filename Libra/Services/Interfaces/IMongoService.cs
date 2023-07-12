using MongoDB.Driver;

namespace Libra.Services.Interfaces;

public interface IMongoService
{
    public IMongoDatabase GetDataBase(string database);
}