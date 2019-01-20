using BookRank.Contracts;
using BookRank.Integration.Tests.Setup;
using BookRank.Libs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookRank.Integration.Tests.Scenarios
{
    [Collection("api")]
    public class BookTests
    {
        private readonly TestContext _sut;

        public BookTests(TestContext sut)
        {
            _sut = sut;
        }

        [Fact]
        public async Task AddBookRankReturnsOkStatus()
        {
            const int userId = 1;

            var response = await AddBookRank(userId);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllBooksReturnsNotNullBookResponse()
        {
            const int userId = 2;

            await AddBookRank(userId);

            var response = await _sut.Client.GetAsync("books");

            IList<BookResponse> result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<IList<BookResponse>>(await content);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBookReturnsExpectedBookName()
        {
            const int userId = 3;
            const string bookName = "test-GetBookBack";

            await AddBookRank(userId, bookName);

            var response = await _sut.Client.GetAsync($"books/{userId}/{bookName}");

            BookResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<BookResponse>(await content);
            }

            Assert.Equal(bookName, result.BookName);
        }

        [Fact]
        public async Task UpdateBookReturnsUpdatedRank()
        {
            const int userId = 4;
            const string bookName = "test-UpdateBook";
            const int ranking = 10;

            await AddBookRank(userId, bookName);

            var updateBook = new BookUpdateRequest
            {
                BookName = bookName,
                Ranking = ranking
            };

            var json = JsonConvert.SerializeObject(updateBook);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            await _sut.Client.PatchAsync($"books/{userId}", stringContent);

            var response = await _sut.Client.GetAsync($"movies/{userId}/{bookName}");

            BookResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<BookResponse>(await content);
            }

            Assert.Equal(ranking, result.Ranking);
        }

        [Fact]
        public async Task GetBookRankReturnsOverallRank()
        {
            const int userId = 5;
            const string bookName = "test-GetBookOverallRank";

            await AddBookRank(userId, bookName);

            var response = await _sut.Client.GetAsync($"books/{bookName}/ranking");

            BookRankResponse result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<BookRankResponse>(await content);
            }

            Assert.NotNull(result);
        }

        private async Task<HttpResponseMessage> AddBookRank(int userId, string bookName = "test-BookName")
        {
            var bookDbData = new BookDb
            {
                UserId = userId,
                BookName = bookName,
                Description = "test-Description",
                Genres = new List<string>
                {
                    "test-Genre1",
                    "test-Genre2"
                },
                Ranking = 6,
                RankedDateTime = "20/01/2019 18:15:00 PM"
            };

            var json = JsonConvert.SerializeObject(bookDbData);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _sut.Client.PostAsync($"books/{userId}", stringContent);
        }
    }
}
