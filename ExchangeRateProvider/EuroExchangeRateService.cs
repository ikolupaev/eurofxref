using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExchangeRateProvider
{
    public class EuroExchangeRateService
    {
        XmlDocument DataDocument;
        private XmlNamespaceManager nsmgr;

        public EuroExchangeRateService(string xml)
        {
            DataDocument = new XmlDocument();
            DataDocument.LoadXml(xml);

            nsmgr = new XmlNamespaceManager(DataDocument.NameTable);
            nsmgr.AddNamespace("n", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");
        }

        public IEnumerable<string> GetAvailableCurrencies()
        {
            var currencies = from x in DataDocument.SelectNodes(@"/*/n:Cube/n:Cube[1]/n:Cube/@currency", nsmgr).Cast<XmlAttribute>()
                             select x.Value;

            return currencies;
        }

        public IEnumerable<ExchangeRate> GetHistoryData()
        {
            var rates = DataDocument.SelectNodes(@"/*/n:Cube/n:Cube/n:Cube", nsmgr).Cast<XmlElement>();
            return rates.Select(CreateExchangeRate);
        }

        ExchangeRate CreateExchangeRate(XmlElement xmlElement)
        {
            var date = DateTime.ParseExact( xmlElement.ParentNode.Attributes["time"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return new ExchangeRate
            {
                Currency = xmlElement.Attributes["currency"].Value,
                Date = date,
                Rate = double.Parse(xmlElement.Attributes["rate"].Value, CultureInfo.InvariantCulture)
            };
        }
    }
}
