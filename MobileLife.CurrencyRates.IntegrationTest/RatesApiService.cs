using System;
using System.ServiceModel;
using MobileLife.CurrencyRates.Api.ApiServices;
using MobileLife.CurrencyRates.Api.Dto;
using NUnit.Framework;

namespace MobileLife.CurrencyRates.IntegrationTest
{
    [TestFixture]
    public class RatesApiService
    {
        [Test]
        public void GetCurrencyRate_IsQueried_ReturnsConcreteDateRate()
        {
            using (var factory = new ChannelFactory<ICurrencyRatesApiService>("BasicHttpBinding_ICurrencyRatesApiService"))
            {
                var channel = factory.CreateChannel();
                var response = channel.GetEuroCurrencyRate(new GetCurrencyRateRequest {Day = DateTime.Today, Currency = "GBP"});

                Assert.NotNull(response.Rate);
            }
        }

        [Test]
        public void ListCurrencyRates_IsQueriedById_ReturnsCollectionOfRates()
        {
            using (var factory = new ChannelFactory<ICurrencyRatesApiService>("BasicHttpBinding_ICurrencyRatesApiService"))
            {
                var channel = factory.CreateChannel();
                var response = channel.ListStoredCurrencyRates(new ListStoredCurrencyRatesRequest { StartId = 0});

                Assert.IsNotEmpty(response.CurrencyRates);
            }
        }
    }
}