using Database.Interfaces;
using Database.Models;
using NBP.Models;
using NBP.NBPCommands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NBP.NBPCommands
{
    public class NBPCommands : INBPCommands
    {
        private readonly IKalkulatorContext db;

        public NBPCommands(IKalkulatorContext db)
        {
            this.db = db;
        }

        public async Task SaveRates(Nbp entity)
        {
            db.Set<Nbp>().Add(entity);

            await db.SaveChangesAsync();
        }

        //DateOnly.TryParse(result.First().EffectiveDate, out DateOnly date);

        //List<Nbprate> rates = new();

        //result.First().Rates.ForEach(x => rates.Add(new Nbprate
        //    {
        //        Currency = x.Currency,
        //        Code = x.Code,
        //        Mid = x.Mid
        //}));

        //        var entity = new Nbp
        //        {
        //            TableType = result.First().Table,
        //            No = result.First().No,
        //            EffectiveDate = date,
        //            Nbprates = rates
        //        };

        //db.Set<Nbp>().Add(entity);

        //await db.SaveChangesAsync();
    }
}
