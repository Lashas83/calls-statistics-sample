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
    public class SmsMessagesSummaryReport_Should : IClassFixture<SmsMessagesReportFixture>
    {
        private readonly CallsRegistryApiClient _api;

        public SmsMessagesSummaryReport_Should(CallsRegistryHost host, SmsMessagesReportFixture testData, ITestOutputHelper output)
        {
            _api = new CallsRegistryApiClient("http://localhost:1234", () => new HttpLoggingHandler(new HttpClientHandler(), output));

            testData.EnsureInitialized(host.Configuration.GetConnectionString("CallsRegistry"));
        }

        public static IEnumerable<object[]> SmsMessagesSummaries
        {
            get
            {
                yield return new object[] { DateTimeOffset.Parse("2019-01-10"), DateTimeOffset.Parse("2019-01-11"), 2, new []
                {
                    new SmsSummary("3706230001", 2)
                } };
                yield return new object[] { DateTimeOffset.Parse("2019-01-10"), DateTimeOffset.Parse("2019-01-12"), 7, new []
                {
                    new SmsSummary("3706230001", 5),
                    new SmsSummary("3706230002", 1),
                    new SmsSummary("3706230003", 1)
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-01-10"), DateTimeOffset.Parse("2019-01-12"), 29, new []
                {
                    new SmsSummary("3706230001", 10),
                    new SmsSummary("3706230003", 5),
                    new SmsSummary("3706230002", 4),
                    new SmsSummary("3706230004", 4),
                    new SmsSummary("3706230005", 3),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-09-15"), DateTimeOffset.Parse("2018-09-16"), 5, new []
                {
                    new SmsSummary("3706230001", 1),
                    new SmsSummary("3706230002", 1),
                    new SmsSummary("3706230003", 1),
                    new SmsSummary("3706230004", 1),
                    new SmsSummary("3706230005", 1),
                } };

                yield return new object[] { DateTimeOffset.Parse("2018-09-14"), DateTimeOffset.Parse("2018-09-15"), 10, new []
                {
                    new SmsSummary("3706230001", 3),
                    new SmsSummary("3706230004", 2),
                    new SmsSummary("3706230006", 2),
                    new SmsSummary("3706230002", 1),
                    new SmsSummary("3706230003", 1)

                } };

                yield return new object[] { DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-02"), 2, new []
                {
                    new SmsSummary("3706230006", 1),
                    new SmsSummary("3706230007", 1)
                } };

                yield return new object[] { DateTimeOffset.Parse("2017-01-01"), DateTimeOffset.Parse("2017-01-02"), 0, new object[0] };

                //                new Call("3706230009", 19, DateTimeOffset.Parse("2018-01-04 15:00+12:00")),

                //yield return new object[] { DateTimeOffset.Parse("2018-01-04"), DateTimeOffset.Parse("2018-01-05"), 0, new object[0] };
                //yield return new object[] { DateTimeOffset.Parse("2018-01-05"), DateTimeOffset.Parse("2018-01-06"), 1, new []
                //{
                //    new SmsSummary("3706230009", 19)
                //} };
            }
        }

        [Theory]
        [MemberData(nameof(SmsMessagesSummaries))]
        public async Task Return_top_5_senders_in_given_period(DateTimeOffset startDate, DateTimeOffset endDate, int expectedTotal, SmsSummary[] smsMessages)
        {
            var response = await _api.GetSendersTop5ByAmountOfMessages(startDate, endDate);
            response.EnsureSuccessStatusCode();

            await response.AssertContentMatches(Is.StructurallyEqualTo(new
            {
                items = smsMessages,
                total = expectedTotal
            }));
        }

        public class SmsSummary
        {
            public string msisdn { get; set; }
            public int count { get; set; }

            public SmsSummary(string msisdn, int count)
            {
                this.msisdn = msisdn;
                this.count = count;
            }
        }
    }
}