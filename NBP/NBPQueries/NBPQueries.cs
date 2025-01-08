using Database;
using Database.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
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

            return await GetNBPLatestRates(DateTime.Today);
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

        private async Task<IActionResult> GetNBPLatestRates(DateTime date)
        {
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
