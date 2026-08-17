using DigitalHomeLibrary.BookService.Application.Requests;
using DigitalHomeLibrary.BookService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    public class BookTagsController(BookTagsService bookTagsService) : Controller
    {
        readonly BookTagsService _bookTagsService = bookTagsService;

        [HttpPost("books/{bookId}/tags")]
        public async Task<IActionResult> AddTagToBook([FromRoute] Guid bookId, [FromBody] CreateTagRequest request)
        {
            var res = await _bookTagsService.AddTagToBook(bookId, request.Name, request.Description);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpGet("books/{bookId}/tags")]
        public async Task<IActionResult> GetBookTags([FromRoute] Guid bookId)
        {
            var resp = await _bookTagsService.GetBookTags(bookId);

            return Ok(resp);
        }

        [HttpDelete("tags/{tagId}")]
        public async Task<IActionResult> DeleteTag(Guid tagId)
        {
            var res = await _bookTagsService.DeleteTag(tagId);
            return res.IsSuccess ? NoContent() : BadRequest(res.Error);
        }

        [HttpPut("tags/{tagId}")]
        public async Task<IActionResult> UpdateTag(Guid tagId, [FromBody] UpdateTagRequest request)
        {
            await _bookTagsService.UpdateTag(tagId, request.Name, request.Description);
            return Ok();
        }
    }
}
