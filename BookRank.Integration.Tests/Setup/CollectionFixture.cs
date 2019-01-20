using Xunit;

namespace BookRank.Integration.Tests.Setup
{
    [CollectionDefinition("api")]
    public class CollectionFixture : ICollectionFixture<TestContext>, ICollectionFixture<TestDataSetup>
    {
    }
}
