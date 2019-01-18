using BookRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRank.Services
{
    public interface IBookRankService
    {
        Task<IEnumerable<BookResponse>> GetAllBooks();

        Task<BookResponse> GetBook(int userId, string bookName);

        Task<IEnumerable<BookResponse>> GetUsersRankedBooksByTitle(int userId, string bookName);

        Task AddBook(int userId, BookRankRequest request);

        Task UpdateBook(int userId, BookUpdateRequest request);
    }
}
