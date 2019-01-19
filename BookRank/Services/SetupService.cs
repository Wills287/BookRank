using BookRank.Libs.Repositories;
using System.Threading.Tasks;

namespace BookRank.Services
{
    public class SetupService : ISetupService
    {
        private readonly IBookRankRepository _bookRankRepository;

        public SetupService(IBookRankRepository bookRankRepository)
        {
            _bookRankRepository = bookRankRepository;
        }

        public async Task CreateDynamoDbTable(string tableName)
        {
            await _bookRankRepository.CreateDynamoDbTable(tableName);
        }

        public async Task DeleteDynamoDbTable(string tableName)
        {
            await _bookRankRepository.DeleteDynamoDbTable(tableName);
        }
    }
}
