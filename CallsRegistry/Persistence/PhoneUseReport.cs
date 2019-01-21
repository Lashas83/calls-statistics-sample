using System.Collections.Generic;
using CallsRegistry.Model;

namespace CallsRegistry.Persistence
{
    public class Summary<TItem>
    {
        public int Total { get; private set; }
        public List<TItem> Items { get; private set; }

        public Summary(int total, List<TItem> items)
        {
            Total = total;
            Items = items;
        }
    }

    public class CallsReport
    {
        public string MSISDN { get; set; }
        public int Duration { get; set; }
    }

    public class SmsMessagesReport
    {
        public string MSISDN { get; set; }
        public int Count { get; set; }
    }

}