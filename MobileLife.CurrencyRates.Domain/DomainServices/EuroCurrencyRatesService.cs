using MobileLife.CurrencyRates.Domain.DomainObjects;
using MobileLife.CurrencyRates.Domain.PersistenceServices;
using MobileLife.CurrencyRates.Domain.ServiceAgents;
using Polly;
using System;

namespace MobileLife.CurrencyRates.Domain.DomainServices
{
    public class EuroCurrencyRatesService : IEuroCurrencyRatesService
    {
        private const string BaseCurrency = "EUR";
        private readonly ICurrencyRatesPersistenceService _currencyRatesPersistenceService;
        private readonly ICurrencyRatesServiceAgent _currencyRatesServiceAgent;

        public EuroCurrencyRatesService(ICurrencyRatesPersistenceService currencyRatesPersistenceService,
            ICurrencyRatesServiceAgent currencyRatesServiceAgent)
        {
            _currencyRatesPersistenceService = currencyRatesPersistenceService;
            _currencyRatesServiceAgent = currencyRatesServiceAgent;
        }

        public CurrencyRate GetEuroCurrencyRate(DateTime day, string currency)
        {
            if (day > DateTime.Today)
                return null;

            var dbCurrencyRate = _currencyRatesPersistenceService.GetCurrencyRate(day, BaseCurrency, currency);

            if (dbCurrencyRate != null)
                return dbCurrencyRate;

            var policy = Policy
                .Handle<Exception>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.Error.WriteLine($"Timeout while calling currency rates service. Attempt number: {retryCount}.");
                });

            var currencyRate =
                policy.Execute(() => _currencyRatesServiceAgent.FetchCurrencyRate(day, BaseCurrency, currency));

            if (currencyRate != null)
                _currencyRatesPersistenceService.SaveCurrencyRate(currencyRate);

            return currencyRate;
        }
    }
}