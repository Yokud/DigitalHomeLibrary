using DigitalHomeLibrary.ContentService.Domain.Entities;
using DigitalHomeLibrary.ContentService.Domain.Repositories;
using DigitalHomeLibrary.ContentService.Domain.Services;

namespace DigitalHomeLibrary.ContentService.Application.Services
{
    public class BookContentService(IBookContentDataRepository bookContentDataRepository, IBookContentStorageService bookContentStorageService)
    {
        readonly IBookContentDataRepository _bookContentDataRepository = bookContentDataRepository;
        readonly IBookContentStorageService _bookContentStorageService = bookContentStorageService;

        public async Task<BookContentData> GetBookContentData(Guid bookId) => await _bookContentDataRepository.GetBookContentData(bookId);

        public async Task<BookContentData> UploadDigitalBook(Guid bookId, IFormFile file, string path = "")
        {
            var keyName = GetKeyName(path, file.FileName);

            var result = await _bookContentStorageService.UploadFile(file, keyName, progress =>
            {
                Console.WriteLine($"Current progress: {progress}%");
            });

            var bookContentData = new BookContentData(bookId, result);

            await _bookContentDataRepository.AddBookContentData(bookContentData);

            return bookContentData;
        }

        public async Task<Stream> DownloadDigitalBook(Guid bookId)
        {
            var bookContentData = await _bookContentDataRepository.GetBookContentData(bookId);

            return await _bookContentStorageService.DownloadFileAsync(bookContentData.ContentUri);
        }

        public async Task DeleteDigitalBook(Guid bookId)
        {
            var bookContentData = await _bookContentDataRepository.GetBookContentData(bookId);
            var keyName = bookContentData.ContentUri;

            await _bookContentDataRepository.DeleteBookContent(bookId);
            await _bookContentStorageService.DeleteFileAsync(keyName);
        }

        private static string GetKeyName(string path, string filename) => string.IsNullOrEmpty(path)
                ? $"{Ulid.NewUlid()}{Path.GetExtension(filename)}"
                : $"{path.TrimEnd('/')}/{Ulid.NewUlid()}{Path.GetExtension(filename)}";

        private static string GetFullPath(string path, string keyName) => string.IsNullOrEmpty(path)
                ? $"{keyName}"
                : $"{path.TrimEnd('/')}/{keyName}";
    }
}
