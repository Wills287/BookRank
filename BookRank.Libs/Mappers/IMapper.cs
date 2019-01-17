using System.Collections.Generic;
using BookRank.Contracts;
using BookRank.Libs.Models;

namespace BookRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<BookResponse> ToBookContract(IEnumerable<BookDb> items);

        BookResponse ToBookContract(BookDb book);

        BookDb ToBookDbModel(int userId, BookRankRequest request);
    }
}
