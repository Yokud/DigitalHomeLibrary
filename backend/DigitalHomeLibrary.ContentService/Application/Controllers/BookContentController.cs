using DigitalHomeLibrary.ContentService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.ContentService.Application.Controllers
{
    [ApiController]
    [Route("content-books")]
    public class BookContentController(BookContentService bookContentService) : Controller
    {
        readonly BookContentService _bookContentService = bookContentService;

        [HttpPost]
        public async Task<IActionResult> UploadDigitalBook([FromQuery] Guid bookId, [FromForm] IFormFile file, [FromQuery] string path = "")
        {
            var res = await _bookContentService.UploadDigitalBook(bookId, file, path);

            return Ok(res);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> DownloadDigitalBook([FromRoute] Guid bookId)
        {
            var contentData = await _bookContentService.GetBookContentData(bookId);
            var fs = await _bookContentService.DownloadDigitalBook(bookId);
            return File(fs, "application/octet-stream", contentData.ContentUri);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteDigitalBook([FromRoute] Guid bookId)
        {
            await _bookContentService.DeleteDigitalBook(bookId);
            return NoContent();
        }
    }
}
