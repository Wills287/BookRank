using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public class BookRankRepository : IBookRankRepository
    {
        private const string TableName = "BookRank";

        private readonly IAmazonDynamoDB _dynamoDbClient;

        public BookRankRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<ScanResponse> GetAllBooks()
        {
            var scanRequest = new ScanRequest(TableName);

            return await _dynamoDbClient.ScanAsync(scanRequest);
        }
    }
}
