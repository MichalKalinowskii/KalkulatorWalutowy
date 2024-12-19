using Database;
using Database.Interfaces;
using Database.Models;
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

        public NBPQueries(IKalkulatorContext db)
        {
            this.db = db;
            httpClient = new HttpClient();
        }

        public async Task<NBPResponse> GetNBPTodayDataAsync()
        {
            NBPResponse result = new();

            var response = await httpClient.GetAsync("https://api.nbp.pl/api/exchangerates/tables/a/today/");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content).First();
            }

            return result;
        }

        public async Task<NBPResponse> GetNBPDataByGivenDate(DateTime date)
        {
            NBPResponse result = new();

            var response = await httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/{date.ToString("yyyy-MM-dd")}");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content).First();
            }

            return result;
        }

        public async Task<List<NBPResponse>> GetNBPRatesInGivenRange(int range)
        {
            HttpClient client = new HttpClient();
            List<NBPResponse> result = new();

            var response = await httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/last/{range}/");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content);
            }

            return result;
        }
    }
}
