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

        public async Task<IActionResult> SaveRates(SaveRates saveRates)
        {
            try
            {
                var user = db.Set<User>()
                    .Where(x => x.Username == saveRates.userName)
                    .FirstOrDefault();

                var nbp = db.Set<Nbp>()
                    .Where(x => x.EffectiveDate == saveRates.nbpData.EffectiveDate)
                    .FirstOrDefault();

                if (nbp is not null)
                {
                    var nbpUser = db.Set<UsersNbp>()
                    .Where(x => x.Nbpid == nbp.Id)
                    .Where(x => x.UserId == user.Id)
                    .FirstOrDefault();

                    if (nbpUser is not null)
                    {
                        return new OkResult();
                    }

                    var userNbpToSave = new UsersNbp
                    {
                        UserId = user.Id,
                        Nbpid = nbp.Id
                    };

                    db.Set<UsersNbp>().Add(userNbpToSave);
                    await db.SaveChangesAsync();
                    return new OkResult();
                }

                var entityToSave = MapToNbp(saveRates.nbpData);

                var userNbp = new UsersNbp
                {
                    UserId = user.Id,
                    Nbp = entityToSave
                };

                db.Set<UsersNbp>().Add(userNbp);

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
