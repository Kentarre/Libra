using Libra.Models;
using Libra.Models.BookModels;

namespace Libra.Services.Interfaces;

public interface IPictureService
{
    public BookPicture AddPicture(BookPicture bookPicture);
    public void SavePictureToStorage(BookPicture bookPicture, IFormFile file);
}