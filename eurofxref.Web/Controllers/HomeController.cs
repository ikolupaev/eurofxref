using ExchangeRateProvider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eurofxref.Web.Controllers
{
    public class HomeController : Controller
    {
        static DateTime epoc = new DateTime(1970, 1, 1);

        // GET: Home
        public ActionResult Index()
        {
            var service = GetExchangeService();
            var items = service.GetAvailableCurrencies().Select(x => new SelectListItem { Text = x }).ToList();

            return View(items);
        }

        public ActionResult HistoryData(string currency)
        {
            var service = GetExchangeService();

            var data = from item in service.GetHistoryData()
                       where item.Currency == currency
                       orderby item.Date
                       select new
                       {
                           x = (long)(item.Date - epoc).TotalSeconds * 1000,
                           y = item.Rate
                       };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExchangeRate(string currency, string to)
        {
            var service = GetExchangeService();

            var data = service.GetHistoryData().OrderByDescending(x => x.Date).ToList();

            var fromX = GetRate(currency, data);
            var toX = GetRate(to, data);
            var rate = toX.Rate / fromX.Rate;

            return Content(string.Format("{0:0.0000}, date: {1:d}", rate, fromX.Date));
        }

        private static ExchangeRate GetRate(string currency, List<ExchangeRateProvider.ExchangeRate> data)
        {
            if (currency == "EUR")
            {
                return new ExchangeRate { Rate = 1, Date = DateTime.Today };
            }

            return data.First(x => x.Currency == currency);
        }

        private static EuroExchangeRateService GetExchangeService()
        {
            var client = new WebClient();
            var xml = client.DownloadString("http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml");
            var service = new EuroExchangeRateService(xml);
            return service;
        }
    }
}