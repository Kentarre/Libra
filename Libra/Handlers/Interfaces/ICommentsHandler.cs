using Libra.Models;

namespace Libra.Handlers.Interfaces;

public interface ICommentsHandler
{
    public IResult GetComments(Guid id);
    public IResult AddComment(Comment model);
}