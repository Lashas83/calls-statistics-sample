using System.Threading.Tasks;
using CallsRegistry.Persistence;

namespace CallsRegistry
{
    public class NopDataGenerator : IDataGenerator
    {
        public Task Prefill(CallsRegistryContext dbContext)
        {
            return Task.CompletedTask;
        }
    }
}