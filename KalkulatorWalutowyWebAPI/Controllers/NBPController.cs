using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using NBP.Models;
using NBP.NBPQueries;
using NBP.NBPQueries.Interfaces;

namespace KalkulatorWalutowyWebAPI.Controllers
{
    [Route("NBP")]
    public class NBPController : ApiBaseController
    {
        private readonly INBPQueries nbp;

        NBPController(INBPQueries nbp)
        {
            this.nbp = this.nbp;
        }

        [HttpGet("Today")]
        public async Task<List<NBPResponse>> GetNBPTodayRates()
        {
            return await nbp.GetNBPTodayDataAsync();
        }

        [HttpGet("Date")]
        public async Task<List<NBPResponse>> GetNBPRatesInGivenDate(DateTime date)
        {
            return await nbp.GetNBPDataInGivenDate(date);
        }

        [HttpGet("Range")]
        public async Task<List<NBPResponse>> GetNBPRatesInGivenRange(int range)
        {
            return await nbp.GetNBPRatesInGivenRange(range);
        }
    }
}
