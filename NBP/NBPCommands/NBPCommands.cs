using Database.Interfaces;
using Database.Models;
using NBP.Models;
using NBP.NBPCommands.Interfaces;
using NBP.NBPQueries.Interfaces;
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
        private readonly INBPQueries nbpQueries;

        public NBPCommands(IKalkulatorContext db, INBPQueries nbpQueries)
        {
            this.db = db;
            this.nbpQueries = nbpQueries;
        }

        public async Task SaveRates(DateTime date)
        {
            var respone = await nbpQueries.GetNBPDataByGivenDate(date);
            var entityToSave = MapToNbp(respone);

            db.Set<Nbp>().Add(entityToSave);

            await db.SaveChangesAsync();
        }

        public Nbp MapToNbp(NBPResponse respone)
        {
            List<Nbprate> rates = new();

            respone.Rates.ForEach(x => rates.Add(new Nbprate
            {
                Currency = x.Currency,
                Code = x.Code,
                Mid = x.Mid
            }));

            var nbpMappedObject = new Nbp
            {
                TableType = respone.Table,
                No = respone.No,
                EffectiveDate = respone.EffectiveDate,
                Nbprates = rates
            };

            return nbpMappedObject;
        }
    }
}
