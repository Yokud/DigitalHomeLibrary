using CSharpFunctionalExtensions;
using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Application.DTO.Requests;
using DigitalHomeLibrary.BookService.Application.DTO.Responses;
using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("authors")]
    public class AuthorsController(AuthorService authorService) : Controller
    {
        private readonly AuthorService _authorService = authorService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var res = await _authorService.GetAuthorById(id);

            return res.IsSuccess ? Ok(AuthorInfo.FromDomainEntity(res.Value)) : NotFound(res.Error);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPageOfAuthors([FromQuery] int page, [FromQuery] int size)
        {
            var paginationInfo = new PaginationInfo(page, size);
            var resp = await _authorService.GetAllAuthors(paginationInfo);

            return Ok(new PaginationResponse<AuthorInfo>(paginationInfo.PageNum, paginationInfo.PageSize, resp.Count, resp.Select(AuthorInfo.FromDomainEntity)));
        }

        [HttpGet("\"{firstname} {lastname} {middlename}\"")]
        public async Task<IActionResult> FindAuthorByFullName(string firstname, string lastname, string? middlename)
        {
            var res = await _authorService.FindAuthorByFullName(new(firstname, lastname, middlename));

            return res.IsSuccess ? Ok(AuthorInfo.FromDomainEntity(res.Value)) : BadRequest(res.Error);
        }

        [HttpGet("books/{bookId}")]
        public async Task<IActionResult> GetBookAuthors(Guid bookId)
        {
            var res = await _authorService.GetBookAuthors(bookId);

            return res.IsSuccess ? Ok(res.Value.Select(AuthorInfo.FromDomainEntity)) : BadRequest(res.Error);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest request)
        {
            var authorRes = await _authorService.GetAuthorById(request.Id);

            if (authorRes.IsFailure)
                return NotFound(authorRes.Error);

            var updatedAuthor = new Author(request.Id,
                new(request.FirstName ?? authorRes.Value.FullName.FirstName,
                    request.LastName ?? authorRes.Value.FullName.LastName,
                    request.MiddleName ?? authorRes.Value.FullName.MiddleName),
                request.BirthDate ?? authorRes.Value.BirthDate,
                request.CountryName is not null ? new(request.CountryName) : authorRes.Value.Country,
                request.DeathDate ?? authorRes.Value.DeathDate,
                request.LifeStory ?? authorRes.Value.LifeStory);
            var res = await _authorService.UpdateAuthor(updatedAuthor);

            return res.IsSuccess ? Ok() : BadRequest(res.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorRequest request)
        {
            var author = new Author(new(request.FirstName, request.LastName, request.MiddleName), request.BirthDate, new(request.CountryName), request.DeathDate, request.LifeStory);
            var res = await _authorService.AddAuthor(author);

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
