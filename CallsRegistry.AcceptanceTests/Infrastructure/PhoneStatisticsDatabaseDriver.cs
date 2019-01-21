using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CallsRegistry.Model;
using CallsRegistry.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CallsRegistry.AcceptanceTests
{
    public class PhoneStatisticsDatabaseDriver : IDisposable
    {
        private readonly CallsRegistryContext _db;

        public PhoneStatisticsDatabaseDriver(string connectionString)
        {
            var opt = new DbContextOptionsBuilder<CallsRegistryContext>()
                .UseSqlServer(connectionString);

            _db = new CallsRegistryContext(opt.Options);
        }

        public Task Add(IEnumerable<SmsMessage> messages, CancellationToken token = default(CancellationToken))
        {
            foreach (var message in messages)
                _db.SmsMessages.Add(message);

            return _db.SaveChangesAsync(token);
        }

        public Task Add(IEnumerable<Call> calls, CancellationToken token = default(CancellationToken))
        {
            foreach (var call in calls)
                _db.PhoneCalls.Add(call);

            return _db.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public void Truncate()
        {
            _db.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE dbo.PhoneCalls"));
            _db.Database.ExecuteSqlCommand(new RawSqlString("TRUNCATE TABLE dbo.SmsMessages"));
        }
    }
}