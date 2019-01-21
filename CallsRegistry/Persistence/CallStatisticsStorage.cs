using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CallsRegistry.Model;
using Microsoft.EntityFrameworkCore;

namespace CallsRegistry.Persistence
{
    public interface ICallsSummaryStorage
    {
        Task<Summary<CallsReport>> GetTop5MsisdnByTotalDuration(DateTimeOffset from, DateTimeOffset to);
        Task<Summary<SmsMessagesReport>> GetTop5MsisdnBySmsCount(DateTimeOffset from, DateTimeOffset to);
    }


    public class EntityFrameworkCallsSummaryStorage : ICallsSummaryStorage
    {
        private readonly CallsRegistryContext _db;

        public EntityFrameworkCallsSummaryStorage(CallsRegistryContext db)
        {
            _db = db;
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

        public async Task<Summary<CallsReport>> GetTop5MsisdnByTotalDuration(DateTimeOffset from, DateTimeOffset to)
        {
            var top5Task = _db.PhoneCalls
                .Where(x => x.Date >= from && x.Date < to)
                .GroupBy(x => x.MSISDN)
                .Select(x => new CallsReport()
                {
                    MSISDN = x.Key,
                    Duration = x.Sum(z => z.Duration)
                })
                .OrderByDescending(x => x.Duration)
                .ThenBy(x => x.MSISDN)
                .Take(5)
                .ToListAsync();

            var totalTask = _db.PhoneCalls
                .Where(x => x.Date >= from && x.Date < to).CountAsync();

            await Task.WhenAll(top5Task, totalTask);

            return new Summary<CallsReport>(totalTask.Result, top5Task.Result);
        }

        public async Task<Summary<SmsMessagesReport>> GetTop5MsisdnBySmsCount(DateTimeOffset @from, DateTimeOffset to)
        {
            var top5Task = _db.SmsMessages
                .Where(x => x.Date >= from && x.Date < to)
                .GroupBy(x => x.MSISDN)
                .Select(x => new SmsMessagesReport()
                {
                    MSISDN = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.MSISDN)
                .Take(5)
                .ToListAsync();

            var totalTask = _db.SmsMessages
                .Where(x => x.Date >= from && x.Date < to).CountAsync();

            await Task.WhenAll(top5Task, totalTask);

            return new Summary<SmsMessagesReport>(totalTask.Result, top5Task.Result);
        }
    }
}