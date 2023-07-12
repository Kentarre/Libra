using Libra.Models;
using Libra.Models.UserModels;

namespace Libra.Database.Interfaces;

public interface IUserRepository
{
    public User? GetById(Guid id);
    public string GetNameById(Guid userId);
    public User? GetByEmail(string email);
    public User? GetByUsername(string username);
    public User AddUser(User user);
}