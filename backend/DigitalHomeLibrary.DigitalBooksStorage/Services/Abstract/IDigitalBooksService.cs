namespace DigitalHomeLibrary.DigitalBooksStorage.Services.Abstract
{
    public interface IDigitalBooksService
    {
        Task<string> UploadFile(IFormFile file, string keyName, Action<int>? progressCallback = null);
        Task<Stream> DownloadFileAsync(string keyName);
        Task DeleteFileAsync(string keyName);
    }
}
