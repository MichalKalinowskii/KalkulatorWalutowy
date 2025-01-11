using Database;
using Database.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBP.Models;
using NBP.NBPQueries.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NBP.NBPQueries
{
    public class NBPQueries : INBPQueries
    {
        private readonly HttpClient httpClient;
        private readonly IKalkulatorContext db;
        const int hardRequestLimit = 30;
        const int daysMaxRange = 93;
        const int daysMinRange = 1;

        public NBPQueries(IKalkulatorContext db)
        {
            this.db = db;
            httpClient = new HttpClient();
        }

        public async Task<IActionResult> GetNBPTodayDataAsync()
        {
            return await GetNBPLatestRates(DateTime.Today);
        }

        public async Task<IActionResult> GetNBPDataByGivenDate(DateTime date)
        {
            if (date == default)
            {
                return new BadRequestResult();
            }

            return await GetNBPLatestRates(date);
        }

        public async Task<IActionResult> GetNBPRatesInGivenRange(int range)
        {
            if (range < daysMinRange || range > daysMaxRange)
            {
                return new BadRequestResult();
            }
            
            HttpClient client = new HttpClient();
            List<NBPResponse> result = new();

            var response = await httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/last/{range}/");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content);
                return new OkObjectResult(result);
            }

            return new StatusCodeResult((int)response.StatusCode);
        }

        public async Task<IActionResult> SavedRates(string username)
        {
            try
            {
                var user = db.Set<User>()
                    .Where(x => x.Username == username)
                    .FirstOrDefault();

                if (user == null) 
                {
                    return new BadRequestObjectResult("User doesnt exists!");
                }

                var rates = db.Set<UsersNbp>()
                    .Include(x => x.Nbp)
                        .ThenInclude(x => x.Nbprates)
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.Nbp)
                    .ToList();

                return new OkObjectResult(rates);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        private async Task<IActionResult> GetNBPLatestRates(DateTime date)
        {
            if (date.Date > DateTime.Today.Date)
            {
                return new BadRequestObjectResult("Invalid date");
            }

            NBPResponse result = new();
            HttpResponseMessage response;
            int daysToSubstract = 0;

            //API w weekendy nie genruje danych
            do
            {
                response = await httpClient
                    .GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/{date.AddDays(-daysToSubstract).ToString("yyyy-MM-dd")}");
                daysToSubstract++;
            } while (response.StatusCode == HttpStatusCode.NotFound && daysToSubstract < hardRequestLimit);

            if (hardRequestLimit < daysToSubstract)
            {
                return new NotFoundResult();
            }

            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content).First();
                return new OkObjectResult(result);
            }

            return new StatusCodeResult((int)response.StatusCode);
        }
    }
}
