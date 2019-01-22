using System.Threading.Tasks;
using CallsRegistry.Persistence;

namespace CallsRegistry.DemoData
{
    public class NopDataGenerator : IDataGenerator
    {
        public Task Prefill(CallsRegistryContext dbContext)
        {
            return Task.CompletedTask;
        }
    }
}