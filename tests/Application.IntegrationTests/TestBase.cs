using System.Threading.Tasks;
using NUnit.Framework;

namespace EcosferaBlazor.Auth.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
