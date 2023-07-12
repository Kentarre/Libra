using Libra.Models.UserModels;

namespace Libra.Handlers.Interfaces;

public interface IUserHandler
{
    public IResult HandleGetById(Guid id);
    public IResult HandleGetByEmail(string email);
    public IResult HandleAddUser(AddUserModel model);
    public IResult HandleGetByUsername(string username);
    public IResult HandleAuthorize(string email, string password);
}