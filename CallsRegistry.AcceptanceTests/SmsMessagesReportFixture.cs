using System;
using CallsRegistry.Model;

namespace CallsRegistry.AcceptanceTests
{
    public class SmsMessagesReportFixture
    {
        private readonly object _syncObj = new object();
        private bool _initialized = false;

        public void EnsureInitialized(string connectionString)
        {
            if (!_initialized)
            {
                lock (_syncObj)
                {
                    if (_initialized)
                        return;

                    var dbDriver = new PhoneStatisticsDatabaseDriver(connectionString);
                    dbDriver.Truncate();

                    dbDriver.Add(new SmsMessage[]
                    {
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2019-01-10 15:37")), 
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2019-01-10 15:48")),
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2019-01-11 15:48")),
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2019-01-11 15:57")),
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2019-01-11 20:48")),

                        new SmsMessage("3706230002", DateTimeOffset.Parse("2019-01-11 20:48")),

                        new SmsMessage("3706230003", DateTimeOffset.Parse("2019-01-11 20:48")),
                        new SmsMessage("3706230003", DateTimeOffset.Parse("2018-02-15 11:35")),

                        new SmsMessage("3706230001", DateTimeOffset.Parse("2018-10-15 11:35")),
                        new SmsMessage("3706230002", DateTimeOffset.Parse("2018-10-15 11:35")),
                        new SmsMessage("3706230003", DateTimeOffset.Parse("2018-10-15 11:35")),
                        new SmsMessage("3706230004", DateTimeOffset.Parse("2018-10-15 11:35")),
                        new SmsMessage("3706230005", DateTimeOffset.Parse("2018-10-15 11:35")),
                        new SmsMessage("3706230006", DateTimeOffset.Parse("2018-10-15 11:35")),


                        new SmsMessage("3706230001", DateTimeOffset.Parse("2018-09-15 11:35")),
                        new SmsMessage("3706230002", DateTimeOffset.Parse("2018-09-15 11:35")),
                        new SmsMessage("3706230003", DateTimeOffset.Parse("2018-09-15 11:35")),
                        new SmsMessage("3706230004", DateTimeOffset.Parse("2018-09-15 11:35")),
                        new SmsMessage("3706230005", DateTimeOffset.Parse("2018-09-15 11:35")),

                        new SmsMessage("3706230001", DateTimeOffset.Parse("2018-09-14 11:35")),
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2018-09-14 12:35")),
                        new SmsMessage("3706230001", DateTimeOffset.Parse("2018-09-14 13:35")),
                        new SmsMessage("3706230002", DateTimeOffset.Parse("2018-09-14 11:35")),
                        new SmsMessage("3706230003", DateTimeOffset.Parse("2018-09-14 11:35")),
                        new SmsMessage("3706230004", DateTimeOffset.Parse("2018-09-14 11:35")),
                        new SmsMessage("3706230004", DateTimeOffset.Parse("2018-09-14 13:35")),
                        new SmsMessage("3706230005", DateTimeOffset.Parse("2018-09-14 11:35")),
                        new SmsMessage("3706230006", DateTimeOffset.Parse("2018-09-14 12:35")),
                        new SmsMessage("3706230006", DateTimeOffset.Parse("2018-09-14 13:35")),

                        new SmsMessage("3706230006", DateTimeOffset.Parse("2017-12-31 23:59:59")),
                        new SmsMessage("3706230006", DateTimeOffset.Parse("2018-01-01 00:00")),
                        new SmsMessage("3706230007", DateTimeOffset.Parse("2018-01-01 00:00:01")),
                        new SmsMessage("3706230006", DateTimeOffset.Parse("2018-01-02 00:00")),

                        new SmsMessage("3706230009", DateTimeOffset.Parse("2018-01-04 15:00+12:00")),
                    });

                    _initialized = true;
                }
            }
        }
    }
}