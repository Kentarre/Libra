using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Libra;

public class GoogleCloudStorage
{
    private string ServiceAccountKeyPath { get; set; }
    private string BucketName { get; set; }
    private GoogleCredential Credentials { get; set; }
    private StorageClient Client { get; set; }
    
    public static GoogleCloudStorage Create()
    {
        return new GoogleCloudStorage();
    }

    public GoogleCloudStorage WithAccountKeyFile(string keyFile)
    {
        ServiceAccountKeyPath = $"{Environment.CurrentDirectory}/{keyFile}";
        return this;
    }

    public GoogleCloudStorage WithCredentials()
    {
        Credentials = GoogleCredential.FromFile(ServiceAccountKeyPath);
        return this;
    }

    public GoogleCloudStorage WithBucket(string bucketName)
    {
        BucketName = bucketName;
        return this;
    }

    public GoogleCloudStorage WithClient()
    {
        Client = StorageClient.Create(Credentials);
        return this;
    }

    public object UploadFile(string path, IFormFile file)
    {
        using var stream = file.OpenReadStream();
        
        return Client.UploadObject("libra_pictures_bucket", path, file.ContentType, stream);
    }
 }