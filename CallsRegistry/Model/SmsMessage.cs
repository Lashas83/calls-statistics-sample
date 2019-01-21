using System;

namespace CallsRegistry.Model
{
    public class SmsMessage : IPhoneUse
    {
        public string MSISDN { get; private set; }
        public DateTimeOffset Date { get; private set; }

        public SmsMessage()
        {

        }

        public SmsMessage(string msisdn, DateTimeOffset date)
        {
            MSISDN = msisdn;
            Date = date;
        }
    }
}