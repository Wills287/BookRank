using BookRank.Contracts;
using BookRank.Libs.Mappers;
using BookRank.Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRank.Services
{
    public class BookRankService : IBookRankService
    {
        private readonly IBookRankRepository _bookRankRepository;

        private readonly IMapper _mapper;

        public BookRankService(IBookRankRepository bookRankRepository, IMapper mapper)
        {
            _bookRankRepository = bookRankRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooks()
        {
            var response = await _bookRankRepository.GetAllBooks();

            return _mapper.ToBookContract(response);
        }

        public async Task<BookResponse> GetBook(int userId, string bookName)
        {
            var response = await _bookRankRepository.GetBook(userId, bookName);

            return _mapper.ToBookContract(response);
        }

        public async Task<IEnumerable<BookResponse>> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var response = await _bookRankRepository.GetUsersRankedBooksByTitle(userId, bookName);

            return _mapper.ToBookContract(response);
        }

        public async Task AddBook(int userId, BookRankRequest request)
        {
            var bookDb = _mapper.ToDocumentModel(userId, request);

            await _bookRankRepository.AddBook(bookDb);
        }

        public async Task UpdateBook(int userId, BookUpdateRequest request)
        {
            var response = await GetBook(userId, request.BookName);

            var bookDb = _mapper.ToDocumentModel(userId, response, request);

            await _bookRankRepository.UpdateBook(bookDb);
        }

        public async Task<BookRankResponse> GetBookRank(string bookName)
        {
            var response = await _bookRankRepository.GetBookRank(bookName);

            var overallBookRanking = Math.Round(response.Select(x => x["Ranking"].AsInt()).Average());

            return new BookRankResponse
            {
                BookName = bookName,
                OverallRanking = overallBookRanking
            };
        }
    }
}
