using System;
using Xunit;
using System.Threading.Tasks;

namespace WebApplication.xunit
{
    public class UnitTest1 : IClassFixture<WebApplicationRestApiDriver>
    {
        private readonly WebApplicationRestApiDriver _driver;

        public UnitTest1(WebApplicationRestApiDriver driver)
        {
            _driver = driver;
        }

        [Fact]
        public async Task Test1()
        {
            // TODO: insert some data into db through driver
            // TODO: query data from restapi (make http call)
            // ASSERT: it should match expected data

            // 

            // TODO: making http call should return 
        }
    }

    public class WebApplicationRestApiDriver
    {
        private readonly string _baseUrl;

        public WebApplicationRestApiDriver(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

    }
}
