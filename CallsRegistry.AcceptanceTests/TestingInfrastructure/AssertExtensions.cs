using System;
using System.Net.Http;
using System.Threading.Tasks;
using NHamcrest;

namespace CallsRegistry.AcceptanceTests
{
    public static class AssertExtensions
    {
        public static async Task<T> AssertEventually<T>(Func<Task<T>> requestFunction, IMatcher<T> matcher)
        {
            var remaining = 30000;
            var interval = 250;

            while (true)
            {
                var result = await requestFunction();

                if (matcher.Matches(result)) return result;
                if (remaining < 0)
                {
                    Assert.That(result, matcher);
                }

                await Task.Delay(interval);
                remaining -= interval;
            }
        }

        public static async Task AssertContentMatches<T>(this Task<HttpResponseMessage> response, IMatcher<T> matcher)
        {
            await (await response).AssertContentMatches(matcher);
        }

        public static async Task AssertContentMatches<T>(this HttpResponseMessage response, IMatcher<T> matcher)
        {
            var content = await response.Content.ReadAsAsync<T>();

            Assert.That(content, matcher);
        }

        public static async Task<HttpContent> AssertResponseEventually(this HttpClient client, Func<HttpClient, Task<HttpResponseMessage>> requestFunction, IMatcher<HttpResponseMessage> matcher)
        {
            return (await AssertEventually(() => requestFunction(client), matcher)).Content;
        }
    }
}