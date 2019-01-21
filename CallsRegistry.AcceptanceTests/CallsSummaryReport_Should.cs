using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NHamcrest;
using Xunit;
using Xunit.Abstractions;

namespace CallsRegistry.AcceptanceTests
{
    [Collection("WebApplication")]
    public class CallsSummaryReport_Should : IClassFixture<CallsReportFixture>
    {
        private readonly ITestOutputHelper _output;
        private CallsRegistryApiClient _api;

        public CallsSummaryReport_Should(CallsRegistryHost host, CallsReportFixture testData, ITestOutputHelper output)
        {
            _output = output;
            _api = new CallsRegistryApiClient("http://localhost:1234", () => new HttpLoggingHandler(new HttpClientHandler(), output));

            testData.EnsureInitialized(host.Configuration.GetConnectionString("CallsRegistry"));
        }

        public static IEnumerable<object[]> PhoneCalls
        {
            get
            {
                yield return new object[] { DateTimeOffset.Parse("2019-01-10"), DateTimeOffset.Parse("2019-01-11"), 2, new []
                {
                    new CallSummary("3706230001", 20)
                } };
                yield return new object[] { DateTimeOffset.Parse("2019-01-10"), DateTimeOffset.Parse("2019-01-12"), 7, new []
                {
                    new CallSummary("3706230002", 173),
                    new CallSummary("3706230003", 156),
                    new CallSummary("3706230001", 50),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-01-10"), DateTimeOffset.Parse("2019-01-12"), 28, new []
                {
                    new CallSummary("3706230003", 948),
                    new CallSummary("3706230001", 639),
                    new CallSummary("3706230002", 623),
                    new CallSummary("3706230005", 302),
                    new CallSummary("3706230004", 286),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-09-15"), DateTimeOffset.Parse("2018-09-16"), 5, new []
                {
                    new CallSummary("3706230003", 250),
                    new CallSummary("3706230002", 157),
                    new CallSummary("3706230001", 136),
                    new CallSummary("3706230005", 83),
                    new CallSummary("3706230004", 55),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-10-15"), DateTimeOffset.Parse("2018-10-16"), 5, new []
                {
                    new CallSummary("3706230001", 136),
                    new CallSummary("3706230002", 136),
                    new CallSummary("3706230003", 136),
                    new CallSummary("3706230004", 136),
                    new CallSummary("3706230005", 136),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-02"), 2, new []
                {
                    new CallSummary("3706230006", 13),
                    new CallSummary("3706230007", 11)
                } };

                yield return new object[] { DateTimeOffset.Parse("2017-01-01"), DateTimeOffset.Parse("2017-01-02"), 0, new object[0] };

                //new Call("3706230009", 19, DateTimeOffset.Parse("2018-01-04 15:00+12:00")),

                //yield return new object[] { DateTimeOffset.Parse("2018-01-04"), DateTimeOffset.Parse("2018-01-05"), 0, new object[0] };
                //yield return new object[] { DateTimeOffset.Parse("2018-01-05"), DateTimeOffset.Parse("2018-01-06"), 1, new []
                //{
                //    new CallSummary("3706230009", 19)
                //} };
            }
        }

        [Theory]
        [MemberData(nameof(PhoneCalls))]
        public async Task Return_top_5_calls_in_given_period(DateTimeOffset startDate, DateTimeOffset endDate, int expectedTotal, CallSummary[] calls)
        {
            var response = await _api.GetCallersTop5ByTotalCallsDuration(startDate, endDate);
            response.EnsureSuccessStatusCode();

            await response.AssertContentMatches(Is.StructurallyEqualTo(new
            {
                items = calls,
                total = expectedTotal
            }));
        }

        public class CallSummary
        {
            public string msisdn { get; set; }
            public int duration { get; set; }

            public CallSummary(string msisdn, int duration)
            {
                this.msisdn = msisdn;
                this.duration = duration;
            }
        }
    }
}