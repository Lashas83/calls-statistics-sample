using System.Threading.Tasks;
using CallsRegistry.Persistence;

namespace CallsRegistry.DemoData
{
    public interface IDataGenerator
    {
        Task Prefill(CallsRegistryContext dbContext);
    }
}