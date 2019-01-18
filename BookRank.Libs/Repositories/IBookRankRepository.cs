using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public interface IBookRankRepository
    {
        Task<IEnumerable<Document>> GetAllBooks();

        Task<Document> GetBook(int userId, string bookName);

        Task<IEnumerable<Document>> GetUsersRankedBooksByTitle(int userId, string bookName);

        Task AddBook(Document document);

        Task UpdateBook(Document document);

        Task<IEnumerable<Document>> GetBookRank(string bookName);
    }
}
