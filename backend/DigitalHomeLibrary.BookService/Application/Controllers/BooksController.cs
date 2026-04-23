using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Application.DTO.Responses;
using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController(BooksService booksService) : Controller
    {
        readonly BooksService _booksService = booksService;

        [HttpGet]
        public async Task<IActionResult> GetPageOfBooks([FromQuery] int page, [FromQuery] int size)
        {
            var paginationInfo = new PaginationInfo(page, size);
            var resp = await _booksService.GetAllBooks(paginationInfo);

            return Ok(new PaginationResponse<BookInfo>(paginationInfo.PageNum, paginationInfo.PageSize, resp.Count(), resp.Select(BookInfo.FromDomainEntity)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var res = await _booksService.GetBookById(id);

            return res.IsSuccess ? Ok(res.Value) : NotFound(res.Error);
        }


        [HttpGet("{id}/authors")]
        public async Task<IActionResult> GetBookAuthors(Guid id)
        {
            var res = await _booksService.GetBookAuthors(id);

            return res.IsSuccess ? Ok(res.Value.Select(AuthorInfo.FromDomainEntity)) : BadRequest(res.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookRequest request)
        {
            var details = new BookDetails(request.Title, request.Description, request.AuthorIds, request.ReleaseYear, request.Publisher, new(request.ISBN), request.Genre, request.Language);

            var id = await _booksService.AddBook(details);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _booksService.DeleteBook(id);
            return NoContent();
        }

        [HttpPatch("{id}/set-reading")]
        public async Task<IActionResult> SetBookToReading(Guid id)
        {
            var res = await _booksService.SetBookStateReading(id);
            return res.IsSuccess ? Ok() : BadRequest(res.Error);
        }

        [HttpPatch("{id}/set-read")]
        public async Task<IActionResult> SetBookToRead(Guid id)
        {
            var res = await _booksService.SetBookStateRead(id);
            return res.IsSuccess ? Ok() : BadRequest(res.Error);
        }
    }
}
