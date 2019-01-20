using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookRank.Integration.Tests.Setup
{
    public class TestDataSetup
    {
        private static readonly IAmazonDynamoDB DynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
        {
            ServiceURL = "http://localhost:8000"
        });

        public TestDataSetup()
        {
            Task.Run(CreateTable);
        }

        public async Task CreateTable()
        {
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName = "UserId",
                        AttributeType = ScalarAttributeType.N
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "BookName",
                        AttributeType = ScalarAttributeType.S
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "UserId",
                        KeyType = KeyType.HASH
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "BookName",
                        KeyType = KeyType.RANGE
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                },
                TableName = "BookRank",
                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
                {
                    new GlobalSecondaryIndex
                    {
                        IndexName = "BookName-index",
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement
                            {
                                AttributeName = "BookName",
                                KeyType = KeyType.HASH
                            }
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 1,
                            WriteCapacityUnits = 1
                        },
                        Projection = new Projection
                        {
                            ProjectionType = ProjectionType.ALL
                        }
                    }
                }
            };
            await DynamoDBClient.CreateTableAsync(request);
        }

        public static void TearDownStore(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            DynamoDBClient.DeleteTableAsync(tableName);
        }

        private static async Task WaitUntilTableReady(string tableName)
        {
            TableStatus status = null;
            do
            {
                Thread.Sleep(1000);
                try
                {
                    status = await GetTableStatus(tableName);
                }
                catch (ResourceNotFoundException e)
                {
                }
            } while (status != TableStatus.ACTIVE);
        }

        private static async Task<TableStatus> GetTableStatus(string tableName)
        {
            var response = await DynamoDBClient.DescribeTableAsync(new DescribeTableRequest
            {
                TableName = tableName
            });

            return response.Table.TableStatus;
        }
    }
}
