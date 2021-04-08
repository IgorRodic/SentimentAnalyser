using NUnit.Framework;
using System.Threading.Tasks;

namespace SentimentAnalyser.Application.IntegrationTests
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await TestingFixture.ResetState();
        }
    }
}
