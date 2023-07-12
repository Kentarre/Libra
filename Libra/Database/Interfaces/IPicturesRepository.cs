using Libra.Models;
using Libra.Models.BookModels;

namespace Libra.Database.Interfaces;

public interface IPicturesRepository
{
    public BookPicture AddPicture(BookPicture bookPicture);
}