using CallsRegistry.Model;
using Microsoft.EntityFrameworkCore;

namespace CallsRegistry.Persistence
{
    public class CallsRegistryContext : DbContext
    {
        public DbSet<SmsMessage> SmsMessages { get; set; }
        public DbSet<Call> PhoneCalls { get; set; }

        public CallsRegistryContext(DbContextOptions<CallsRegistryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SmsMessage>(e => e.HasKey(x => new { x.MSISDN, x.Date }));
            modelBuilder.Entity<Call>(e => e.HasKey(x => new { x.MSISDN, x.Date }));
        }
    }
}