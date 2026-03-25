using DigitalHomeLibrary.ContentService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.ContentService.Application.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books-files")]
    public class DigitalBooksStorageController(BookContentService bookContentService) : Controller
    {
        readonly BookContentService _bookContentService = bookContentService;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDigitalBook([FromQuery] Guid bookId, [FromBody] IFormFile file, [FromQuery] string path = "")
        {
            var res = await _bookContentService.UploadDigitalBook(bookId, file, path);

            return Ok(res);
        }

        [HttpGet("download/{bookId}")]
        public async Task<IActionResult> DownloadDigitalBook([FromRoute] Guid bookId, [FromQuery] string path = "")
        {
            var contentData = await _bookContentService.GetBookContentData(bookId);
            var fs = await _bookContentService.DownloadDigitalBook(bookId);
            return File(fs, "application/octet-stream", contentData.ContentUri);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteDigitalBook([FromRoute] Guid bookId, [FromQuery] string path = "")
        {
            await _bookContentService.DeleteDigitalBook(bookId);
            return NoContent();
        }
    }
}
