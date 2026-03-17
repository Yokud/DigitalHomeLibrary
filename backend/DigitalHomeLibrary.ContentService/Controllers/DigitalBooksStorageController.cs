using DigitalHomeLibrary.DigitalBooksStorage.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.DigitalBooksStorage.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books-files")]
    public class DigitalBooksStorageController(IDigitalBooksService digitalBooksService) : Controller
    {
        readonly IDigitalBooksService _digitalBooksService = digitalBooksService;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDigitalBook([FromBody] IFormFile file, [FromQuery] string path = "")
        {
            var keyName = string.IsNullOrEmpty(path)
                ? $"{Ulid.NewUlid()}{Path.GetExtension(file.FileName)}"
                : $"{path.TrimEnd('/')}/{Ulid.NewUlid()}{Path.GetExtension(file.FileName)}";

            var result = await _digitalBooksService.UploadFile(file, keyName, progress =>
            {
                Console.WriteLine($"Current progress: {progress}%");
            });
            return Ok(new { KeyName = result });
        }

        [HttpGet("download/{keyName}")]
        public async Task<IActionResult> DownloadDigitalBook([FromRoute] string keyName, [FromQuery] string path = "")
        {
            var fullPath = string.IsNullOrEmpty(path)
                ? $"{keyName}"
                : $"{path.TrimEnd('/')}/{keyName}";

            var stream = await _digitalBooksService.DownloadFileAsync($"{fullPath}");
            return File(stream, "application/octet-stream", keyName);
        }

        [HttpDelete("{keyName}")]
        public async Task<IActionResult> DeleteDigitalBook([FromRoute] string keyName, [FromQuery] string path = "")
        {
            var fullPath = string.IsNullOrEmpty(path)
                ? $"{keyName}"
                : $"{path.TrimEnd('/')}/{keyName}";

            await _digitalBooksService.DeleteFileAsync($"{fullPath}");
            return NoContent();
        }
    }
}
