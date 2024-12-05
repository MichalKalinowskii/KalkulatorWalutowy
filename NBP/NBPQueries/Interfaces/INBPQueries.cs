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
        Task<List<NBPResponse>> GetNBPTodayDataAsync();
        Task<List<NBPResponse>> GetNBPDataInGivenDate(DateTime date);
        Task<List<NBPResponse>> GetNBPRatesInGivenRange(int range);
    }
}
