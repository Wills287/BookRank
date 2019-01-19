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
    }
}
