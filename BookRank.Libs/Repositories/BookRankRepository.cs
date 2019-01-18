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
        private readonly Table _table;

        private const string TableName = "BookRank";

        public BookRankRepository(IAmazonDynamoDB dynamoDBClient)
        {
            _table = Table.LoadTable(dynamoDBClient, TableName);
        }

        public async Task<IEnumerable<Document>> GetAllBooks()
        {
            var config = new ScanOperationConfig();

            return await _table.Scan(config).GetRemainingAsync();
        }

        public async Task<Document> GetBook(int userId, string bookName)
        {
            return await _table.GetItemAsync(userId, bookName);
        }

        public async Task<IEnumerable<Document>> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var filter = new QueryFilter("UserId", QueryOperator.Equal, userId);
            filter.AddCondition("BookName", QueryOperator.BeginsWith, bookName);

            return await _table.Query(filter).GetRemainingAsync();
        }

        public async Task AddBook(Document document)
        {
            await _table.PutItemAsync(document);
        }

        public async Task UpdateBook(Document document)
        {
            await _table.UpdateItemAsync(document);
        }

        public async Task<IEnumerable<Document>> GetBookRank(string bookName)
        {
            var filter = new QueryFilter("BookName", QueryOperator.Equal, bookName);

            var config = new QueryOperationConfig
            {
                IndexName = "BookName-index",
                Filter = filter
            };

            return await _table.Query(config).GetRemainingAsync();
        }
    }
}
