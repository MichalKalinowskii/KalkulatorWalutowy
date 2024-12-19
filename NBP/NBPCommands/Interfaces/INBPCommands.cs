using Database.Models;

namespace NBP.NBPCommands.Interfaces
{
    public interface INBPCommands
    {
        Task SaveRates(DateTime date);
    }
}