using Libra.Database.Interfaces;
using Libra.Models;
using Libra.Models.BookModels;
using Libra.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Libra.Services;

public class PictureService : IPictureService
{
    private readonly IPicturesRepository _repository;
    private readonly GoogleCloudStorageConfig _storageConfig;

    public PictureService(IPicturesRepository repository, IOptions<AppConfig> appConfig)
    {
        _repository = repository;
        _storageConfig = appConfig.Value.GoogleCloudStorageConfig;
    }
    
    public BookPicture AddPicture(BookPicture bookPicture)
    {
        return _repository.AddPicture(bookPicture);
    }

    public void SavePictureToStorage(BookPicture bookPicture, IFormFile file)
    {
        GoogleCloudStorage.Create()
            .WithAccountKeyFile(_storageConfig.KeyFile)
            .WithCredentials()
            .WithBucket(_storageConfig.BucketName)
            .WithClient()
            .UploadFile(bookPicture.FilePath, file);
    }
}