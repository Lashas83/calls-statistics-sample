using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CallsRegistry.AcceptanceTests
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ITestOutputHelper _outputHelper;

        public HttpLoggingHandler(HttpMessageHandler innerHandler, ITestOutputHelper outputHelper)
            : base(innerHandler)
        {
            _outputHelper = outputHelper;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _outputHelper.WriteLine("Request:");
            _outputHelper.WriteLine(request.ToString());
            if (request.Content != null)
            {
                _outputHelper.WriteLine(await request.Content.ReadAsStringAsync());
            }
            _outputHelper.WriteLine("");

            var response = await base.SendAsync(request, cancellationToken);

            _outputHelper.WriteLine("Response:");
            _outputHelper.WriteLine(response.ToString());
            if (response.Content != null)
            {
                _outputHelper.WriteLine(await response.Content.ReadAsStringAsync());
            }
            _outputHelper.WriteLine("");

            return response;
        }
    }
}