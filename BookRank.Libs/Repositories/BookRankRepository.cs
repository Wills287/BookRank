using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BookRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public class BookRankRepository : IBookRankRepository
    {
        private readonly DynamoDBContext _context;

        public BookRankRepository(IAmazonDynamoDB dynamoDBClient)
        {
            _context = new DynamoDBContext(dynamoDBClient);
        }

        public async Task<IEnumerable<BookDb>> GetAllBooks()
        {
            return await _context.ScanAsync<BookDb>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<BookDb> GetBook(int userId, string bookName)
        {
            return await _context.LoadAsync<BookDb>(userId, bookName);
        }

        public async Task<IEnumerable<BookDb>> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var config = new DynamoDBOperationConfig
            {
                QueryFilter = new List<ScanCondition>
                {
                    new ScanCondition("BookName", ScanOperator.BeginsWith, bookName)
                }
            };

            return await _context.QueryAsync<BookDb>(userId, config).GetRemainingAsync();
        }

        public async Task AddBook(BookDb bookDb)
        {
            await _context.SaveAsync(bookDb);
        }

        public async Task UpdateBook(BookDb bookDb)
        {
            await _context.SaveAsync(bookDb);
        }
    }
}
