using Microsoft.AspNetCore.Mvc;
using NBP.Models;
using NBP.NBPQueries;

namespace KalkulatorWalutowyWebAPI.Controllers
{
    [Route("NBP")]
    public class NBPController : ApiBaseController
    {
        [HttpGet("Today")]
        public async Task<List<NBPResponse>> GetNBPTodayRates()
        {
            var nbp = new NBAQueries();
            return await nbp.GetNBPTodayDataAsync();
        }

        [HttpGet("Date")]
        public async Task<List<NBPResponse>> GetNBPRatesInGivenDate(DateTime date)
        {
            var nbp = new NBAQueries();
            return await nbp.GetNBPDataInGivenDate(date);
        }

        [HttpGet("Range")]
        public async Task<List<NBPResponse>> GetNBPRatesInGivenRange(int range)
        {
            var nbp = new NBAQueries();
            return await nbp.GetNBPRatesInGivenRange(range);
        }
    }
}
