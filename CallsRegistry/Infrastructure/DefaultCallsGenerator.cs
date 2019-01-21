using System;
using System.Collections.Generic;
using CallsRegistry.Model;
using CallsRegistry.Persistence;

namespace CallsRegistry.Infrastructure
{
    public class DefaultCallsGenerator
    {
        private readonly Random _random;

        public DefaultCallsGenerator()
        {
            _random = new Random();
        }

        public IEnumerable<Call> GenerateCalls(string msisdn, int count)
        {
            var current = DateTimeOffset.UtcNow;

            while (count > 0)
            {
                yield return new Call(msisdn, _random.Next(10, 300), current);
                current = current.AddMinutes(-_random.Next(1, 5760));
                count--;
            }
        }
    }
}