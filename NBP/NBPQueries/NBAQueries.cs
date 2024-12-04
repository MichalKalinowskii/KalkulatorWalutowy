using Database.Models;
using NBP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NBP.NBPQueries
{
    public class NBAQueries
    {
        HttpClient HttpClient { get; set; }
        KalkulatorContext db;

        public NBAQueries(KalkulatorContext db)
        {
            this.db = db;
            HttpClient = new HttpClient();
        }

        public async Task<List<NBPResponse>> GetNBPTodayDataAsync()
        {
            List<NBPResponse> result = new();

            var response = await HttpClient.GetAsync("https://api.nbp.pl/api/exchangerates/tables/a/today/");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content);
            }

            DateOnly.TryParse(result.First().EffectiveDate, out DateOnly date);

            List<Nbprate> rates = new();

            result.First().Rates.ForEach(x => rates.Add(new Nbprate
            {
                Currency = x.Currency,
                Code = x.Code,
                Mid = x.Mid
            }));

            var entity = new Nbp
            {
                TableType = result.First().Table,
                No = result.First().No,
                EffectiveDate = date,
                Nbprates = rates
            };

            db.Nbps.Add(entity);

            await db.SaveChangesAsync();

            return result;
        }

        public async Task<List<NBPResponse>> GetNBPDataInGivenDate(DateTime date)
        {
            HttpClient client = new HttpClient();
            List<NBPResponse> result = new();

            var response = await HttpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/{date.ToString("yyyy-MM-dd")}");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content);
            }

            return result;
        }

        public async Task<List<NBPResponse>> GetNBPRatesInGivenRange(int range)
        {
            HttpClient client = new HttpClient();
            List<NBPResponse> result = new();

            var response = await HttpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/tables/a/last/{range}/");
            if (response?.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<NBPResponse>>(content);
            }

            return result;
        }
    }
}
