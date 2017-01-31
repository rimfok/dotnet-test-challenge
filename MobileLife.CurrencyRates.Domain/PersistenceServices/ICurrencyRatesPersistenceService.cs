using MobileLife.CurrencyRates.Domain.DomainObjects;
using System;
using System.Collections.Generic;

namespace MobileLife.CurrencyRates.Domain.PersistenceServices
{
    public interface ICurrencyRatesPersistenceService
    {
        CurrencyRate GetCurrencyRate(DateTime day, string baseCurrency, string targetCurrency);

        IEnumerable<CurrencyRate> ListCurrencyRates(int startId, int limit);

        bool SaveCurrencyRate(CurrencyRate currencyRate);
    }
}