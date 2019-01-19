using System.Threading.Tasks;

namespace BookRank.Services
{
    public interface ISetupService
    {
        Task CreateDynamoDbTable(string tableName);

        Task DeleteDynamoDbTable(string tableName);
    }
}
