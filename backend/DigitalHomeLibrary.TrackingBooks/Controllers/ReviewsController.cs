using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.DTO;
using DigitalHomeLibrary.TrackingBooks.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHomeLibrary.TrackingBooks.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("books-info/reviews")]
    public class ReviewsController(IBookReviewsService bookReviewsService) : Controller
    {
        readonly IBookReviewsService _bookReviewsService = bookReviewsService;

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookReviews(Guid bookId, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var resp = (await _bookReviewsService.GetBookReviews(bookId)).Skip((page - 1) * size).Take(size);

                return Ok(new PaginationResponse<ReviewInfo>(page, size, resp.Count(), resp.Select(ReviewInfo.FromEntity)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            try
            {
                await _bookReviewsService.DeleteReview(reviewId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReviewToBook([FromBody] ReviewCreateRequest request)
        {
            var review = new Review()
            {
                BookId = request.BookId,
                Score = request.Score,
                Note = request.Note,
            };

            var reviewId = await _bookReviewsService.AddReviewToBook(review);
            return Ok(reviewId);
        }
    }
}
