using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBP.Models
{
    public class NBPResponse
    {
        public string Table { get; set; }
        public string No { get; set; }
        public string TradingDate { get; set; }
        public string EffectiveDate { get; set; }
        public List<Rates> Rates { get; set; }
    }
}
