using BookRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public interface IBookRankRepository
    {
        Task<IEnumerable<BookDb>> GetAllBooks();

        Task<BookDb> GetBook(int userId, string bookName);

        Task<IEnumerable<BookDb>> GetUsersRankedBooksByTitle(int userId, string bookName);

        Task AddBook(BookDb bookDb);
    }
}
