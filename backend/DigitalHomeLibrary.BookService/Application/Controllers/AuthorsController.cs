using DigitalHomeLibrary.BookService.Application.Requests;
using DigitalHomeLibrary.BookService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorsController(AuthorService authorService) : Controller
    {
        private readonly AuthorService _authorService = authorService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var res = await _authorService.GetAuthorById(id);

            return res.IsSuccess ? Ok(res.Value) : NotFound(res.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetPageOfAuthors([FromQuery] int page, [FromQuery] int size)
        {
            var resp = await _authorService.GetAllAuthors(page, size);

            return Ok(resp);
        }

        [HttpGet("\"{firstname} {lastname} {middlename}\"")]
        public async Task<IActionResult> FindAuthorByFullName(string firstname, string lastname, string? middlename)
        {
            var res = await _authorService.FindAuthorByFullName(new(firstname, lastname, middlename));

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
        {
            var res = await _authorService.UpdateAuthor(request.Id, request.FirstName, request.MiddleName, request.LastName, request.BirthDate, request.DeathDate, request.LifeStory, request.CountryName);

            return res.IsSuccess ? Ok() : BadRequest(res.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest request)
        {
            var res = await _authorService.AddAuthor(request.FirstName, request.MiddleName, request.LastName, request.BirthDate, request.DeathDate, request.LifeStory, request.CountryName);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _authorService.DeleteAuthor(id);

            return NoContent();
        }
    }
}
