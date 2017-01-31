using MobileLife.CurrencyRates.Domain.DomainObjects;

namespace MobileLife.CurrencyRates.Domain.Mappers
{
    public static class CurrencyRateMapper
    {
        public static CurrencyRate ToDomainObject(this Database.Models.CurrencyRate currencyRate)
        {
            return new CurrencyRate
            {
                Id = currencyRate.Id,
                BaseCurrency = currencyRate.BaseCurrency,
                TargetCurrency = currencyRate.TargetCurrency,
                Day = currencyRate.Day,
                Rate = currencyRate.Rate
            };
        }

        public static Database.Models.CurrencyRate ToDatabaseModel(this CurrencyRate currencyRate)
        {
            return new Database.Models.CurrencyRate
            {
                Id = currencyRate.Id,
                BaseCurrency = currencyRate.BaseCurrency,
                TargetCurrency = currencyRate.TargetCurrency,
                Day = currencyRate.Day,
                Rate = currencyRate.Rate
            };
        }
    }
}