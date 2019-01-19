using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using BookRank.Contracts;

namespace BookRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<BookResponse> ToBookContract(ScanResponse response)
        {
            return response.Items.Select(ToBookContract);
        }

        public IEnumerable<BookResponse> ToBookContract(QueryResponse response)
        {
            return response.Items.Select(ToBookContract);
        }

        public BookResponse ToBookContract(GetItemResponse response)
        {
            return new BookResponse
            {
                BookName = response.Item["BookName"].S,
                Description = response.Item["Description"].S,
                Genres = response.Item["Genres"].SS,
                Ranking = Convert.ToInt32(response.Item["Ranking"].N),
                TimeRanked = response.Item["RankedDateTime"].S
            };
        }

        private BookResponse ToBookContract(Dictionary<string, AttributeValue> item)
        {
            return new BookResponse
            {
                BookName = item["BookName"].S,
                Description = item["Description"].S,
                Genres = item["Genres"].SS,
                Ranking = Convert.ToInt32(item["Ranking"].N),
                TimeRanked = item["RankedDateTime"].S
            };
        }
    }
}
