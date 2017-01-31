using MobileLife.CurrencyRates.Database.DataAgents;
using MobileLife.CurrencyRates.Domain.DomainObjects;
using MobileLife.CurrencyRates.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileLife.CurrencyRates.Domain.PersistenceServices
{
    public class DatabaseCurrencyRatePersistenceService : ICurrencyRatesPersistenceService
    {
        private readonly ICurrencyRatesDataAgent _currencyRatesDataAgent;

        public DatabaseCurrencyRatePersistenceService(ICurrencyRatesDataAgent currencyRatesDataAgent)
        {
            _currencyRatesDataAgent = currencyRatesDataAgent;
        }

        public CurrencyRate GetCurrencyRate(DateTime day, string baseCurrency, string targetCurrency)
        {
            return _currencyRatesDataAgent.GetCurrencyRate(day, baseCurrency, targetCurrency)?.ToDomainObject();
        }

        public IEnumerable<CurrencyRate> ListCurrencyRates(int startId, int limit)
        {
            return _currencyRatesDataAgent.ListCurrencyRates(startId, limit).Select(c => c.ToDomainObject());
        }

        public bool SaveCurrencyRate(CurrencyRate currencyRate)
        {
            return _currencyRatesDataAgent.SaveCurrencyRate(currencyRate?.ToDatabaseModel());
        }
    }
}