using Amazon.DynamoDBv2.DocumentModel;
using BookRank.Contracts;
using System.Collections.Generic;

namespace BookRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<BookResponse> ToBookContract(IEnumerable<Document> items);

        BookResponse ToBookContract(Document book);

        Document ToDocumentModel(int userId, BookRankRequest request);

        Document ToDocumentModel(int userId, BookResponse response, BookUpdateRequest request);
    }
}
