using System.Threading.Tasks;
using CallsRegistry.Persistence;

namespace CallsRegistry
{
    public interface IDataGenerator
    {
        Task Prefill(CallsRegistryContext dbContext);
    }
}