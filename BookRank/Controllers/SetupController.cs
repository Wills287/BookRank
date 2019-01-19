using BookRank.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookRank.Controllers
{
    [Route("setup")]
    public class SetupController : Controller
    {
        private readonly ISetupService _setupService;

        public SetupController(ISetupService setupService)
        {
            _setupService = setupService;
        }

        [HttpPost]
        [Route("create/{tableName}")]
        public async Task<IActionResult> CreateDynamoDbTable(string tableName)
        {
            await _setupService.CreateDynamoDbTable(tableName);

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{tableName}")]
        public async Task<IActionResult> DeleteDynamoDbTable(string tableName)
        {
            await _setupService.DeleteDynamoDbTable(tableName);

            return Ok();
        }
    }
}
