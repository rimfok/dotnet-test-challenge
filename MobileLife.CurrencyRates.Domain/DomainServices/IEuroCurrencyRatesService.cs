using MobileLife.CurrencyRates.Domain.DomainObjects;
using System;

namespace MobileLife.CurrencyRates.Domain.DomainServices
{
    public interface IEuroCurrencyRatesService
    {
        CurrencyRate GetEuroCurrencyRate(DateTime day, string currency);
    }
}