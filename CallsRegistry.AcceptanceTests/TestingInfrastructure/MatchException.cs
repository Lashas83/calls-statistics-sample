using Xunit.Sdk;

namespace CallsRegistry.AcceptanceTests
{
    public class MatchException : AssertActualExpectedException
    {
        public MatchException(object expected, object actual, string userMessage) : base(expected, actual, userMessage)
        {
        }
    }
}