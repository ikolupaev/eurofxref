using System;
using System.Collections.Generic;
namespace ExchangeRateProvider
{
    public interface IEuroExchangeRateService
    {
        IEnumerable<string> GetAvailableCurrencies();
        decimal GetEuroRate(string currency);
        decimal GetEuroRate(string currency, DateTime date);
    }
}
