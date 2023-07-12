using Libra.Models;
using Libra.Models.UserModels;

namespace Libra.Services.Interfaces;

public interface IUserService
{
    public User GetUserById(Guid id);
    public string GetNameById(Guid userId);
    public User GetByEmail(string email);
    public User GetByUsername(string username);
    public User AddUser(AddUserModel model);
}