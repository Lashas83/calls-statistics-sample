using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace CallsRegistry.AcceptanceTests
{
    public class CallsRegistryApiClient : IDisposable
    {
        //private readonly string _baseUrl;
        private readonly HttpClient _client;

        public CallsRegistryApiClient(string baseUrl, Func<HttpMessageHandler> handlerFactory = null)
        {
            if (handlerFactory == null)
                handlerFactory = () => new HttpClientHandler();

            _client = new HttpClient(handlerFactory())
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<HttpResponseMessage> GetCallersTop5ByTotalCallsDuration(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var uri = QueryHelpers.AddQueryString("api/calls/top5-by-total-duration", new Dictionary<string, string>()
            {
                { "from", startDate.ToString("s")},
                { "to", endDate.ToString("s")}
            });

            var response = await _client.GetAsync(uri);

            return response;
        }

        public async Task<HttpResponseMessage> GetSendersTop5ByAmountOfMessages(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var uri = QueryHelpers.AddQueryString("api/sms/top5-by-amount", new Dictionary<string, string>()
            {
                { "from", startDate.ToString("s")},
                { "to", endDate.ToString("s")}
            });

            var response = await _client.GetAsync(uri);

            return response;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}