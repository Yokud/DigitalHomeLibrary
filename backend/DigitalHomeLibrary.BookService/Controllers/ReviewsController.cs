using DigitalHomeLibrary.BookService.Application.DTO.Info;
using DigitalHomeLibrary.BookService.Application.DTO.Requests;
using DigitalHomeLibrary.BookService.Application.DTO.Responses;
using DigitalHomeLibrary.BookService.Application.Services;
using DigitalHomeLibrary.BookService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books/{bookId}/reviews")]
    public class ReviewsController(ReviewService bookReviewsService) : Controller
    {
        readonly ReviewService _bookReviewsService = bookReviewsService;

        [HttpGet]
        public async Task<IActionResult> GetBookReviews([FromRoute] Guid bookId, [FromQuery] int page, [FromQuery] int size)
        {
            var resp = (await _bookReviewsService.GetBookReviews(bookId, new(page, size)));

            return Ok(new PaginationResponse<ReviewInfo>(page, size, resp.Count(), resp.Select(ReviewInfo.FromDomainEntity)));
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            await _bookReviewsService.DeleteReview(reviewId);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddReviewToBook([FromBody] CreateReviewRequest request)
        {
            var review = new Review(request.BookId, new(request.Score), request.Note);

            var res = await _bookReviewsService.AddReviewToBook(review);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}
