using Amazon.DynamoDBv2.Model;
using System.Threading.Tasks;

namespace BookRank.Libs.Repositories
{
    public interface IBookRankRepository
    {
        Task<ScanResponse> GetAllBooks();
    }
}
