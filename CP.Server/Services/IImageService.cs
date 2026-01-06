namespace CP.Server.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(Stream fileStream, string fileName);
    }
}
