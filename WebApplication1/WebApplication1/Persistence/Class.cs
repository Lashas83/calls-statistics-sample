using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Persistence
{
    public class PhoneCommunicationContext : DbContext
    {
        public DbSet<SmsMessage> SmsMessages { get; set; }
        public DbSet<Call> PhoneCalls { get; set; }

        public PhoneCommunicationContext(DbContextOptions<PhoneCommunicationContext> options) : base(options)
        {

        }
    }

    public class SmsMessage
    {
        public string msisdn { get; private set; }
        public DateTimeOffset Date { get; private set; }
    }

    public class Call
    {
        public string msisdn { get; private set; }
        public int Duration { get; private set; }
        public DateTimeOffset Date { get; private set; }
    }
}
