using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BookRank.Contracts;
using System;
using System.Collections.Generic;
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

        public async Task<GetItemResponse> GetBook(int userId, string bookName)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "UserId", new AttributeValue { N = userId.ToString() } },
                    { "BookName", new AttributeValue { S = bookName } }
                }
            };

            return await _dynamoDbClient.GetItemAsync(request);
        }

        public async Task<QueryResponse> GetUsersRankedBooksByTitle(int userId, string bookName)
        {
            var request = new QueryRequest
            {
                TableName = TableName,
                KeyConditionExpression = "UserId = :userId and begins_with (BookName, :bookName)",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":userId", new AttributeValue { N = userId.ToString() } },
                    { ":bookName", new AttributeValue { S = bookName } }
                }
            };

            return await _dynamoDbClient.QueryAsync(request);
        }

        public async Task AddBook(int userId, BookRankRequest request)
        {
            var putRequest = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "UserId", new AttributeValue { N = userId.ToString() } },
                    { "BookName", new AttributeValue { S = request.BookName } },
                    { "Description", new AttributeValue { S = request.Description } },
                    { "Genres", new AttributeValue { SS = request.Genres } },
                    { "Ranking", new AttributeValue { N = request.Ranking.ToString() } },
                    { "RankedDateTime", new AttributeValue { S = DateTime.UtcNow.ToString() } }
                }
            };

            await _dynamoDbClient.PutItemAsync(putRequest);
        }

        public async Task UpdateBook(int userId, BookUpdateRequest request)
        {
            var updateRequest = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "UserId", new AttributeValue { N = userId.ToString() } },
                    { "BookName", new AttributeValue { S = request.BookName } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    {
                        "Ranking",
                        new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue { N = request.Ranking.ToString() }
                        }
                    },
                    {
                        "RankedDateTime",
                        new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue { S = DateTime.UtcNow.ToString() }
                        }
                    }
                }
            };

            await _dynamoDbClient.UpdateItemAsync(updateRequest);
        }

        public async Task<QueryResponse> GetBookRank(string bookName)
        {
            var request = new QueryRequest
            {
                TableName = TableName,
                IndexName = "BookName-index",
                KeyConditionExpression = "BookName = :bookName",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":bookName", new AttributeValue { S = bookName } }
                }
            };

            return await _dynamoDbClient.QueryAsync(request);
        }

        public async Task CreateDynamoDbTable(string tableName)
        {
            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };

            await _dynamoDbClient.CreateTableAsync(request);
        }

        public async Task DeleteDynamoDbTable(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            await _dynamoDbClient.DeleteTableAsync(request);
        }
    }
}
