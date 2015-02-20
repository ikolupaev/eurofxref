using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateProvider
{
    public class ExchangeRateService
    {
        IEuroExchangeRateService euroExchangeRateService;

        public ExchangeRateService(IEuroExchangeRateService euroExchangeRateService)
        {
            this.euroExchangeRateService = euroExchangeRateService;
        }

        public decimal GetExchnageRate(string fromCurrency, string toCurrency)
        {
            return euroExchangeRateService.GetEuroRate(toCurrency) / euroExchangeRateService.GetEuroRate(fromCurrency);
        }
    }
}
