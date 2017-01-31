using MobileLife.CurrencyRates.Database.Models;
using System;
using System.Collections.Generic;

namespace MobileLife.CurrencyRates.Database.DataAgents
{
    public interface ICurrencyRatesDataAgent
    {
        IEnumerable<CurrencyRate> ListCurrencyRates(int start, int limit);

        CurrencyRate GetCurrencyRate(DateTime date, string baseCurrency, string targetCurrency);

        bool SaveCurrencyRate(CurrencyRate currencyRate);
    }
}