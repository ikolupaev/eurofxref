using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateProvider
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }
    }
}
