using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExchangeRateProvider;

namespace ExchangeRateProviderTests
{
    [TestClass]
    public class EuroExchangeRateProviderTests
    {
        EuroExchangeRateService euroProvider;
        
        [TestInitialize]
        public void Init()
        {
            euroProvider = new EuroExchangeRateService(Properties.Resources.eurofxref_hist_90d);
        }

        [TestMethod]
        public void GetAvailableCurrencies_Should_Return_Currencies()
        {
            Assert.IsTrue(euroProvider.GetAvailableCurrencies().ToList().Any());
        }

        [TestMethod]
        public void GetHistoryData_Should_Return_Correct_Latest_USD_Rate()
        {
            Assert.AreEqual(1.1372, euroProvider.GetHistoryData().Where(x => x.Currency == "USD").OrderByDescending(x => x.Date).First().Rate);
        }

        [TestMethod]
        public void GetHistoryData_Should_Return_Correct_Oldest_USD_Rate()
        {
            Assert.AreEqual(1.2422, euroProvider.GetHistoryData().Where(x => x.Currency == "USD").OrderBy(x => x.Date).First().Rate);
        }

        [TestMethod]
        public void GetHistoryData_Should_Not_Throw()
        {
            euroProvider.GetHistoryData().Any();
        }
    }
}
