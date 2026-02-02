using DigitalHomeLibrary.TrackingBooks.DTO;
using DigitalHomeLibrary.TrackingBooks.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalHomeLibrary.TrackingBooks.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books-info")]
    public class BooksController(IBooksService booksService) : Controller
    {
        readonly IBooksService _booksService = booksService;

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks([FromQuery] int page, [FromQuery] int size)
        {
            var resp = (await _booksService.GetBooks()).Skip((page - 1) * size).Take(size);

            return Ok(new PaginationResponse<BookInfo>(page, size, resp.Count(), resp.Select(BookInfo.FromEntity)));
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors([FromQuery] int page, [FromQuery] int size)
        {
            var resp = (await _booksService.GetAuthors()).Skip((page - 1) * size).Take(size);

            return Ok(new PaginationResponse<AuthorInfo>(page, size, resp.Count(), resp.Select(AuthorInfo.Fro)));
        }

        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _booksService.GetBook(id);

            return book is not null ? Ok(BookInfo.FromEntity(book)) : NotFound();
        }

        [HttpGet("authors/{id}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var author = await _booksService.GetAuthor(id);

            return author is not null ? Ok(AuthorInfo.FromEntity(author)) : NotFound();
        }

        [HttpPost("books")]
        public async Task<IActionResult> AddBook([FromBody] BookCreateRequest request)
        {
            try
            {
                var book = BookInfo.ToEntity(request.BookInfo);
                var authors = request.BookAuthorsInfo.Select(AuthorInfo.ToEntity);

                await _booksService.AddBook(book, authors);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _booksService.DeleteBook(id);
            return NoContent();
        }

        [HttpPatch("books/{id}/set-reading")]
        public async Task<IActionResult> SetBookToReading(Guid id)
        {
            try
            {
                await _booksService.SetBookStateReading(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("books/{id}/set-read")]
        public async Task<IActionResult> SetBookToRead(Guid id)
        {
            try
            {
                await _booksService.SetBookStateRead(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
