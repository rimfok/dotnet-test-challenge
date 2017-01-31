using System;
using System.Linq;
using System.ServiceModel;
using MobileLife.CurrencyRates.Api.ApiServices;
using MobileLife.CurrencyRates.Api.Dto;
using MobileLife.CurrencyRates.Api.Enums;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MobileLife.CurrencyRates.IntegrationTest.Specs
{
    [Binding]
    public class SurfingSteps
    {
        private static ChannelFactory<ICurrencyRatesApiService> _factory;
        readonly ListStoredCurrencyRatesRequest _listRequest = new ListStoredCurrencyRatesRequest();
        ListCurrencyRatesResponse _listResponse;
        readonly GetCurrencyRateRequest _getRequest = new GetCurrencyRateRequest();
        private GetCurrencyRateResponse _getResponse;
        private static ICurrencyRatesApiService _channel;

        [BeforeFeature]
        public static void Setup()
        {
            _factory = new ChannelFactory<ICurrencyRatesApiService>("BasicHttpBinding_ICurrencyRatesApiService");
            _channel = _factory.CreateChannel();
        }

        [Given(@"I have set initial id to (.*)")]
        public void GivenIHaveSetInitialIdTo(int id)
        {
            _listRequest.StartId = id;
        }

        [Given(@"I have set initial day to ""(.*)""")]
        public void GivenIHaveSetInitialDayTo(DateTime rateDay)
        {
            _getRequest.Day = rateDay;
        }

        [Given(@"Currency is to ""(.*)""")]
        public void GivenCurrencyIsTo(string targetCurrency)
        {
            _getRequest.Currency = targetCurrency;
        }

        [When(@"I invoke WS method ListStoredCurrencyRates")]
        public void WhenIInvokeWSMethodListStoredCurrencyRates()
        {
            _listResponse = _channel.ListStoredCurrencyRates(_listRequest);
        }

        [When(@"I invoke WS method GetEuroCurrencyRate")]
        public void WhenIInvokeWSMethodGetEuroCurrencyRate()
        {
            _getResponse = _channel.GetEuroCurrencyRate(_getRequest);
        }

        [Then(@"I should get successful list response")]
        public void ThenIShouldGetSuccessfulListResponse()
        {
            Assert.AreEqual(ResponseCode.Success, _listResponse.ResponseCode);
        }

        [Then(@"I should get successful get response")]
        public void ThenIShouldGetSuccessfulGetResponse()
        {
            Assert.AreEqual(ResponseCode.Success, _getResponse.ResponseCode);
        }

        [Then(@"I should get collection of rates up to (.*)")]
        public void ThenIShouldGetCollectionOfRatesUpTo(int maxItems)
        {
            Assert.LessOrEqual(_listResponse.CurrencyRates?.Count(), maxItems);
        }

        [Then(@"I should get rate equal to (.*)")]
        public void ThenIShouldGetRateEqualTo(Decimal expectedRate)
        {
            Assert.AreEqual(expectedRate, _getResponse.Rate);
        }

        [AfterFeature]
        public static void Cleanup()
        {
            _factory?.Close();
        }
    }
}
