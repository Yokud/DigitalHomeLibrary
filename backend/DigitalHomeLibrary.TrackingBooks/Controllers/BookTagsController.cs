using DigitalHomeLibrary.BookService.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Domain.Services;
using DigitalHomeLibrary.BookService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books-info/{bookId}/tags")]
    public class BookTagsController(IBookTagService bookTagsService) : Controller
    {
        readonly IBookTagService _bookTagsService = bookTagsService;

        [HttpPost]
        public async Task<IActionResult> AddTagToBook([FromRoute] Guid bookId, [FromBody] TagCreateRequest request)
        {
            var tag = new TagEntity()
            {
                Name = request.Name,
                Description = request.Description ?? string.Empty
            };

            try
            {
                var tagId = await _bookTagsService.AddTagToBook(bookId, tag);
                return Ok(tagId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookTags([FromRoute] Guid bookId)
        {
            var resp = await _bookTagsService.GetBookTags(bookId);

            return Ok(resp.Select(e => new TagsResponse(e.Id, e.Name, e.Description, e.TaggedBooks.Select(BookInfo.FromEntity))));
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(Guid tagId)
        {
            await _bookTagsService.DeleteTag(tagId);
            return NoContent();
        }

        [HttpPut("{tagId}")]
        public async Task<IActionResult> UpdateTag(Guid tagId, [FromBody] TagUpdateRequest request)
        {
            var tag = new TagEntity()
            {
                Id = tagId,
                Name = request.Name,
                Description = request.Description ?? string.Empty
            };

            await _bookTagsService.UpdateTag(tag);
            return Ok();
        }
    }
}
