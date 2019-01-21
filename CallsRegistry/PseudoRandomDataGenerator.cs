using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallsRegistry.Model;
using CallsRegistry.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CallsRegistry
{
    public class PseudoRandomDataGenerator : IDataGenerator
    {
        private readonly Random _rand;

        public PseudoRandomDataGenerator(Random rand)
        {
            _rand = rand;
        }

        public async Task Prefill(CallsRegistryContext dbContext)
        {
            if (await dbContext.PhoneCalls.AnyAsync())
                return;

            var msisdn = GenerateMSISDN().ToList();

            await dbContext.PhoneCalls.AddRangeAsync(GenerateCalls(msisdn));
            await dbContext.SmsMessages.AddRangeAsync(GenerateSmsMessages(msisdn));

            await dbContext.SaveChangesAsync();
        }

        private IEnumerable<Call> GenerateCalls(List<string> msisdn)
        {
            for (var i = 0; i < 5000; i++)
            {
                yield return new Call(msisdn[_rand.Next(msisdn.Count)], _rand.Next(15, 300),
                    DateTimeOffset.UtcNow.AddMinutes(-_rand.Next(5, 1752) * 20).AddSeconds(_rand.Next(60)));
            }
        }

        private IEnumerable<SmsMessage> GenerateSmsMessages(List<string> msisdn)
        {
            for (var i = 0; i < 5000; i++)
            {
                yield return new SmsMessage(msisdn[_rand.Next(msisdn.Count)],
                    DateTimeOffset.UtcNow.AddMinutes(-_rand.Next(5, 1752) * 20).AddSeconds(_rand.Next(60)));
            }
        }

        private static IEnumerable<string> GenerateMSISDN()
        {
            for (var i = 0; i < 1000; i++)
            {
                int phoneNo = 0;
                unchecked
                {
                    phoneNo = ((9349 + i * 7) * 53) ^ i;
                    phoneNo = phoneNo % 100000;
                }

                yield return $"370623{phoneNo:00000}";
            }
        }
    }
}