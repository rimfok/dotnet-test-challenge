using MobileLife.CurrencyRates.Domain.DomainObjects;
using System;

namespace MobileLife.CurrencyRates.Domain.ServiceAgents
{
    public interface ICurrencyRatesServiceAgent
    {
        CurrencyRate FetchCurrencyRate(DateTime day, string baseCurrency, string targetCurrency);
    }
}