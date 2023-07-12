using System.Transactions;
using Libra.Database.Interfaces;
using Libra.Models;
using Libra.Models.UserModels;
using Libra.Services.Interfaces;

namespace Libra.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public User GetUserById(Guid id)
    {
        var user = _repository.GetById(id);

        if (user == null)
            throw new Exception($"User with Id: {id} not found");

        return user;
    }

    public string GetNameById(Guid userId)
    {
        var name = _repository.GetNameById(userId);
        
        if (name == string.Empty)
            throw new Exception($"User with Id: {userId} not found");

        return name;
    }

    public User GetByEmail(string email)
    {
        var user = _repository.GetByEmail(email);

        if (user == null)
            throw new Exception($"User with email: {email} not found");

        return user;
    }

    public User GetByUsername(string username)
    {
        var user = _repository.GetByUsername(username);

        if (user == null)
            throw new Exception($"User with username: {username} not found");

        return user;
    }

    public User AddUser(AddUserModel model)
    {
        if (model.Email == string.Empty)
            throw new Exception("Email field is empty");
        if (model.Username == string.Empty)
            throw new Exception("Username field is empty");
        if (model.Name == string.Empty)
            throw new Exception("Name field is empty");
        
        var userToAdd = new User
        {
            Name = model.Name,
            Username = model.Username,
            Email = model.Email,
            CreatedOn = DateTime.UtcNow
        };

        return _repository.AddUser(userToAdd);
    }
}