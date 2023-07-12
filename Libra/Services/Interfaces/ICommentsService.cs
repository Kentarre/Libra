using Libra.Models;
using MongoDB.Bson;

namespace Libra.Services.Interfaces;

public interface ICommentsService
{
    public Guid AddComment(Comment model);
    public List<dynamic> GetComments(Guid bookId);
}