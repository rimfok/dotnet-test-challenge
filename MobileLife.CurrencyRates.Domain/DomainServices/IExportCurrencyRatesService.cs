using MobileLife.CurrencyRates.Domain.DomainObjects;
using System.Collections.Generic;

namespace MobileLife.CurrencyRates.Domain.DomainServices
{
    public interface IExportCurrencyRatesService
    {
        IEnumerable<CurrencyRate> ListCurrencyRates(int startId);
    }
}