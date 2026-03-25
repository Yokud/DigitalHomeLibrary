namespace DigitalHomeLibrary.ContentService.Domain.Services
{
    public interface IBookContentStorageService
    {
        Task<string> UploadFile(IFormFile file, string keyName, Action<int>? progressCallback = null);
        Task<Stream> DownloadFileAsync(string keyName);
        Task DeleteFileAsync(string keyName);
    }
}
