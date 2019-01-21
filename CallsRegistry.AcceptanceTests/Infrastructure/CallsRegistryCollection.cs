using Xunit;

namespace CallsRegistry.AcceptanceTests
{
    [CollectionDefinition("WebApplication")]
    public class CallsRegistryCollection : ICollectionFixture<CallsRegistryHost>
    {
    }
}
