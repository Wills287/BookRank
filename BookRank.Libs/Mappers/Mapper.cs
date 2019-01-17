using BookRank.Contracts;
using BookRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<BookResponse> ToBookContract(IEnumerable<BookDb> items)
        {
            return items.Select(ToBookContract);
        }

        public BookResponse ToBookContract(BookDb book)
        {
            return new BookResponse
            {
                BookName = book.BookName,
                Description = book.Description,
                Genres = book.Genres,
                Ranking = book.Ranking,
                TimeRanked = book.RankedDateTime
            };
        }

        public BookDb ToBookDbModel(int userId, BookRankRequest request)
        {
            return new BookDb
            {
                UserId = userId,
                BookName = request.BookName,
                Description = request.Description,
                Genres = request.Genres,
                Ranking = request.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }
    }
}
