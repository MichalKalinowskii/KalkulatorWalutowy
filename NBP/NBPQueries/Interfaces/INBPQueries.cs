using Microsoft.AspNetCore.Mvc;
using NBP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBP.NBPQueries.Interfaces
{
    public interface INBPQueries
    {
        Task<IActionResult> GetNBPTodayDataAsync();
        Task<IActionResult> GetNBPDataByGivenDate(DateTime date);
        Task<IActionResult> GetNBPRatesInGivenRange(int range);
    }
}
