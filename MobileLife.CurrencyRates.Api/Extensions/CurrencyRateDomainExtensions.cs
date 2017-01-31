using MobileLife.CurrencyRates.Api.Dto;

namespace MobileLife.CurrencyRates.Api.Extensions
{
    internal static class CurrencyRateDomainExtensions
    {
        public static CurrencyRate ToDto(this Domain.DomainObjects.CurrencyRate currencyRate)
        {
            return new CurrencyRate
            {
                Id = currencyRate.Id,
                Day = currencyRate.Day.ToString("yyy-MM-dd"),
                BaseCurrency = currencyRate.BaseCurrency,
                TargetCurrency = currencyRate.TargetCurrency,
                Rate = currencyRate.Rate
            };
        }
    }
}