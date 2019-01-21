using System;
using CallsRegistry.Model;

namespace CallsRegistry.AcceptanceTests
{
    public class CallsReportFixture
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

                    dbDriver.Add(new Call[]
                    {
                        new Call("3706230001", 10, DateTimeOffset.Parse("2019-01-10 15:37")),
                        new Call("3706230001", 10, DateTimeOffset.Parse("2019-01-10 15:48")),
                        new Call("3706230001", 10, DateTimeOffset.Parse("2019-01-11 15:48")),
                        new Call("3706230001", 10, DateTimeOffset.Parse("2019-01-11 15:57")),
                        new Call("3706230001", 10, DateTimeOffset.Parse("2019-01-11 20:48")),

                        new Call("3706230002", 173, DateTimeOffset.Parse("2019-01-11 20:48")),

                        new Call("3706230003", 156, DateTimeOffset.Parse("2019-01-11 20:48")),
                        new Call("3706230003", 156, DateTimeOffset.Parse("2018-02-15 11:35")),

                        new Call("3706230001", 136, DateTimeOffset.Parse("2018-10-15 11:35")),
                        new Call("3706230002", 136, DateTimeOffset.Parse("2018-10-15 11:35")),
                        new Call("3706230003", 136, DateTimeOffset.Parse("2018-10-15 11:35")),
                        new Call("3706230004", 136, DateTimeOffset.Parse("2018-10-15 11:35")),
                        new Call("3706230005", 136, DateTimeOffset.Parse("2018-10-15 11:35")),


                        new Call("3706230001", 136, DateTimeOffset.Parse("2018-09-15 11:35")),
                        new Call("3706230002", 157, DateTimeOffset.Parse("2018-09-15 11:35")),
                        new Call("3706230003", 250, DateTimeOffset.Parse("2018-09-15 11:35")),
                        new Call("3706230004", 55, DateTimeOffset.Parse("2018-09-15 11:35")),
                        new Call("3706230005", 83, DateTimeOffset.Parse("2018-09-15 11:35")),

                        new Call("3706230001", 136, DateTimeOffset.Parse("2018-09-14 11:35")),
                        new Call("3706230001",  58, DateTimeOffset.Parse("2018-09-14 12:35")),
                        new Call("3706230001", 123, DateTimeOffset.Parse("2018-09-14 13:35")),
                        new Call("3706230002", 157, DateTimeOffset.Parse("2018-09-14 11:35")),
                        new Call("3706230003", 250, DateTimeOffset.Parse("2018-09-14 11:35")),
                        new Call("3706230004",  55, DateTimeOffset.Parse("2018-09-14 11:35")),
                        new Call("3706230004",  40, DateTimeOffset.Parse("2018-09-14 13:35")),
                        new Call("3706230005",  83, DateTimeOffset.Parse("2018-09-14 11:35")),
                        new Call("3706230006", 127, DateTimeOffset.Parse("2018-09-14 12:35")),
                        new Call("3706230006",  11, DateTimeOffset.Parse("2018-09-14 13:35")),

                        new Call("3706230006",   7, DateTimeOffset.Parse("2017-12-31 23:59:59")),
                        new Call("3706230006",  13, DateTimeOffset.Parse("2018-01-01 00:00")),
                        new Call("3706230007",  11, DateTimeOffset.Parse("2018-01-01 00:00:01")),
                        new Call("3706230006",  17, DateTimeOffset.Parse("2018-01-02 00:00")),

                        new Call("3706230009",  19, DateTimeOffset.Parse("2018-01-04 15:00+12:00")),
                    });

                    _initialized = true;
                }
            }
        }
    }
}