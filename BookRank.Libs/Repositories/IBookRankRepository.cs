using Amazon.DynamoDBv2.Model;
using BookRank.Contracts;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public interface IBookRankRepository
    {
        Task<ScanResponse> GetAllBooks();

        Task<GetItemResponse> GetBook(int userId, string bookName);

        Task<QueryResponse> GetUsersRankedBooksByTitle(int userId, string bookName);

        Task AddBook(int userId, BookRankRequest request);

        Task UpdateBook(int userId, BookUpdateRequest request);
        
        Task<QueryResponse> GetBookRank(string bookName);

        Task CreateDynamoDbTable(string tableName);

        Task DeleteDynamoDbTable(string tableName);
    }
}
