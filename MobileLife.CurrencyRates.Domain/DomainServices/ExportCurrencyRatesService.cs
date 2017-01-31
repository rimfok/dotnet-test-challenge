using MobileLife.CurrencyRates.Domain.DomainObjects;
using MobileLife.CurrencyRates.Domain.PersistenceServices;
using System.Collections.Generic;

namespace MobileLife.CurrencyRates.Domain.DomainServices
{
    public class ExportCurrencyRatesService : IExportCurrencyRatesService
    {
        private readonly ICurrencyRatesPersistenceService _currencyRatesPersistenceService;

        private const int Limit = 10;

        public ExportCurrencyRatesService(ICurrencyRatesPersistenceService currencyRatesPersistenceService)
        {
            _currencyRatesPersistenceService = currencyRatesPersistenceService;
        }

        public IEnumerable<CurrencyRate> ListCurrencyRates(int startId)
        {
            return _currencyRatesPersistenceService.ListCurrencyRates(startId, Limit);
        }
    }
}