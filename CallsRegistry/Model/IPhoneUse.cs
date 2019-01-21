using System;

namespace CallsRegistry.Model
{
    public interface IPhoneUse
    {
        string MSISDN { get; }
        DateTimeOffset Date { get; }
    }
}