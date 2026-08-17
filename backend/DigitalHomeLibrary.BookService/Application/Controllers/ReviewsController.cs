using DigitalHomeLibrary.BookService.Application.Requests;
using DigitalHomeLibrary.BookService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.BookService.Application.Controllers
{
    [ApiController]
    public class ReviewsController(ReviewService bookReviewsService) : Controller
    {
        readonly ReviewService _bookReviewsService = bookReviewsService;

        [HttpGet("books/{bookId}/reviews")]
        public async Task<IActionResult> GetBookReviews([FromRoute] Guid bookId, [FromQuery] int page, [FromQuery] int size)
        {
            var resp = await _bookReviewsService.GetBookReviews(bookId, page, size);

            return Ok(resp);
        }

        [HttpDelete("reviews/{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            await _bookReviewsService.DeleteReview(reviewId);

            return NoContent();
        }

        [HttpPost("reviews")]
        public async Task<IActionResult> AddReviewToBook([FromBody] CreateReviewRequest request)
        {
            var res = await _bookReviewsService.AddReviewToBook(request.BookId, request.Score, request.Note);

            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}
