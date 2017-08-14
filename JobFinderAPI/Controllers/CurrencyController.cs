using JobFinderAPI.CurrencyConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobFinderAPI.Controllers
{
    [RoutePrefix("api/soap")]
    public class CurrencyController : ApiController
    {
        [Route("convert")]
        [HttpPost]
        public decimal convert(String fromCurrency, String toCurrency, decimal amount)
        {
            ConverterSoapClient converter = new ConverterSoapClient();
            if (fromCurrency == "EUR")
            {
                return (converter.GetCurrencyRate(toCurrency, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))) * amount;
            }
            else
                return (converter.GetCurrencyRate(fromCurrency, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))) / amount;
        }

    }
}
