using eurofxref.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExchangeRateProviderTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void HistoryData_Should_Return_Json()
        {
            var controller = new HomeController();
            Assert.IsInstanceOfType(controller.HistoryData("USD"), typeof(JsonResult));
        }

        [TestMethod]
        public void HistoryData_Should_Return_Content()
        {
            var controller = new HomeController();
            Assert.IsInstanceOfType(controller.ExchangeRate("EUR","USD"), typeof(ContentResult));
        }
    }
}
