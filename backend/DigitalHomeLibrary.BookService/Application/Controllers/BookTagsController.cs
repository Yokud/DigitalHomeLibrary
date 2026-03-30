using DigitalHomeLibrary.BookService.Application.DTO.Requests;
using DigitalHomeLibrary.BookService.Application.DTO.Responses;
using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BookTagsController(BookTagsService bookTagsService) : Controller
    {
        readonly BookTagsService _bookTagsService = bookTagsService;

        [HttpPost("books/{bookId}/tags")]
        public async Task<IActionResult> AddTagToBook([FromRoute] Guid bookId, [FromBody] CreateTagRequest request)
        {
            var tag = new BookTag(request.Name, request.Description ?? string.Empty);

            var res = await _bookTagsService.AddTagToBook(bookId, tag);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpGet("books/{bookId}/tags")]
        public async Task<IActionResult> GetBookTags([FromRoute] Guid bookId)
        {
            var resp = await _bookTagsService.GetBookTags(bookId);

            return Ok(resp.Select(e => new TagsResponse(e.Id, e.Name, e.Description)));
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
            var tag = new BookTag(tagId, request.Name, request.Description ?? string.Empty);

            await _bookTagsService.UpdateTag(tag);
            return Ok();
        }
    }
}
