using Database;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBP.Models;
using NBP.NBPCommands.Interfaces;
using NBP.NBPQueries;
using NBP.NBPQueries.Interfaces;
using System.Net;

namespace KalkulatorWalutowyWebAPI.Controllers
{
    [Route("nbp")]
    public class NBPController : ApiBaseController
    {
        private readonly INBPQueries nbpQuries;
        private readonly INBPCommands nbpCommands;

        public NBPController(INBPQueries nbp, INBPCommands nbpCommands)
        {
            this.nbpQuries = nbp;
            this.nbpCommands = nbpCommands;
        }


        [HttpGet("today")]
        public async Task<IActionResult> GetNBPTodayRates()
        {
            return await nbpQuries.GetNBPTodayDataAsync();
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetNBPRatesInGivenDate(DateTime date)
        {
            return await nbpQuries.GetNBPDataByGivenDate(date);
        }

        [HttpGet("range")]
        public async Task<IActionResult> GetNBPRatesInGivenRange(int range)
        {
            return await nbpQuries.GetNBPRatesInGivenRange(range);
        }

        //[Authorize]
        [HttpPost("save")]
        public async Task<IActionResult> SaveRates([FromBody] NBPResponse nbpData)
        {
            return await nbpCommands.SaveRates(nbpData);
        }
    }
}
