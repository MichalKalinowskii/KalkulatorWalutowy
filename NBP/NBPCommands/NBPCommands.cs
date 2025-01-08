using Database.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
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

        public NBPCommands(IKalkulatorContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> SaveRates(NBPResponse npbData)
        {
            try
            {
                var entityToSave = MapToNbp(npbData);

                db.Set<Nbp>().Add(entityToSave);

                await db.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        private Nbp MapToNbp(NBPResponse respone)
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
