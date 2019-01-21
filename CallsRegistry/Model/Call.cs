using System;

namespace CallsRegistry.Model
{
    public class Call: IPhoneUse
    {
        public string MSISDN { get; private set; }
        public int Duration { get; private set; }
        public DateTimeOffset Date { get; private set; }

        private Call()
        {

        }

        public Call(string msisdn, int duration, DateTimeOffset date)
        {
            MSISDN = msisdn;
            Duration = duration;
            Date = date;
        }
    }
}
