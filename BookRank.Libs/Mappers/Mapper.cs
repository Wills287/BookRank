using Amazon.DynamoDBv2.DocumentModel;
using BookRank.Contracts;
using BookRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRank.Libs.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<BookResponse> ToBookContract(IEnumerable<Document> items)
        {
            return items.Select(ToBookContract);
        }

        public BookResponse ToBookContract(Document item)
        {
            return new BookResponse
            {
                BookName = item["BookName"],
                Description = item["Description"],
                Genres = item["Genres"].AsListOfString(),
                Ranking = item["Ranking"].AsInt(),
                TimeRanked = item["RankedDateTime"]
            };
        }

        public Document ToDocumentModel(int userId, BookRankRequest request)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["BookName"] = request.BookName,
                ["Description"] = request.Description,
                ["Genres"] = request.Genres,
                ["Ranking"] = request.Ranking,
                ["RankedDateTime"] = DateTime.UtcNow.ToString()
            };
        }

        public Document ToDocumentModel(int userId, BookResponse response, BookUpdateRequest request)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["BookName"] = response.BookName,
                ["Description"] = response.Description,
                ["Genres"] = response.Genres,
                ["Ranking"] = request.Ranking,
                ["RankedDateTime"] = DateTime.UtcNow.ToString()
            };
        }
    }
}
