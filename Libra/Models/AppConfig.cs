namespace Libra.Models;

public class AppConfig
{
    public JwtConfig JwtConfig { get; set; }
    public GoogleCloudStorageConfig GoogleCloudStorageConfig { get; set; }
}

public class JwtConfig
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

public class GoogleCloudStorageConfig
{
    public string KeyFile { get; set; }
    public string BucketName { get; set; }
}