using BookRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRank.Services
{
    public interface IBookRankService
    {
        Task<IEnumerable<BookResponse>> GetAllBooks()
    }
}
