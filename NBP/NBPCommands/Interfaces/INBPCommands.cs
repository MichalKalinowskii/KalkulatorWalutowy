using Database.Models;
using Microsoft.AspNetCore.Mvc;
using NBP.Models;

namespace NBP.NBPCommands.Interfaces
{
    public interface INBPCommands
    {
        Task<IActionResult> SaveRates(SaveRates saveRates);
    }
}