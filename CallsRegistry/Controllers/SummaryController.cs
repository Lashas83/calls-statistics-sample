using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CallsRegistry.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CallsRegistry.Controllers
{
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ICallsSummaryStorage _storage;

        public SummaryController(ICallsSummaryStorage storage)
        {
            _storage = storage;
        }

        [Route("api/calls/top5-by-total-duration")]
        public async Task<ActionResult<object>> GetCallsTop5ByTotalDuration(DateTimeOffset from, DateTimeOffset to)
        {
            var result = await _storage.GetTop5MsisdnByTotalDuration(from, to);
            return result;
        }

        [Route("api/sms/top5-by-amount")]
        public async Task<ActionResult<object>> GetSmsTop5ByAmount(DateTimeOffset from, DateTimeOffset to)
        {
            var result = await _storage.GetTop5MsisdnBySmsCount(from, to);
            return result;
        }
    }
}
