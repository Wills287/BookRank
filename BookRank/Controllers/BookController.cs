using System.Collections.Generic;
using System.Threading.Tasks;
using BookRank.Contracts;
using BookRank.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRank.Controllers
{
    [Route("books")]
    public class BookController : Controller
    {
        private readonly IBookRankService _bookRankService;

        public BookController(IBookRankService bookRankService)
        {
            _bookRankService = bookRankService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            var results = await _bookRankService.GetAllBooks();

            return results;
        }

        [HttpGet]
        [Route("{userId}/{bookName}")]
        public async Task<BookResponse> GetBook(int userId, string bookName)
        {
            var result = await _bookRankService.GetBook(userId, bookName);

            return result;
        }

        [HttpGet]
        [Route("user/{userId}/rankedBooks/{bookName}")]
        public async Task<IEnumerable<BookResponse>> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var results = await _bookRankService.GetUsersRankedBooksByTitle(userId, bookName);

            return results;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> AddBook(int userId, [FromBody] BookRankRequest request)
        {
            await _bookRankService.AddBook(userId, request);

            return Ok();
        }
    }
}
