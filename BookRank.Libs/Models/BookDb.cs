using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace BookRank.Libs.Models
{
    [DynamoDBTable("BookRank")]
    public class BookDb
    {
        [DynamoDBHashKey]
        public int UserId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string BookName { get; set; }

        public string Description { get; set; }

        public List<string> Genres { get; set; }

        public int Ranking { get; set; }

        public string RankedDateTime { get; set; }
    }
}
