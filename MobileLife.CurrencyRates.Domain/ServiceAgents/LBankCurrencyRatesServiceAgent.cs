using MobileLife.CurrencyRates.Domain.DomainObjects;
using MobileLife.CurrencyRates.Domain.LietuvosBankas.CurrencyRates;
using System;
using System.Globalization;

namespace MobileLife.CurrencyRates.Domain.ServiceAgents
{
    public class LBankCurrencyRatesServiceAgent : ICurrencyRatesServiceAgent
    {
        private readonly FxRatesSoap _fxRatesSoap;
        private const string RateType = "EU";
        private const string DatePattern = "yyyy-MM-dd";

        public LBankCurrencyRatesServiceAgent(FxRatesSoap fxRatesSoap)
        {
            _fxRatesSoap = fxRatesSoap;
        }

        public CurrencyRate FetchCurrencyRate(DateTime day, string baseCurrency, string targetCurrency)
        {
            var date = day.ToString(DatePattern, CultureInfo.InvariantCulture);

            var response = _fxRatesSoap.getFxRatesForCurrency(RateType, targetCurrency, date, date);

            var rate = response?.SelectNodes("//*[local-name()='Amt']")?[1]?.InnerText;

            if (rate == null)
                return null;

            decimal currencyRate;
            if (!decimal.TryParse(rate, NumberStyles.Any, new NumberFormatInfo { NumberDecimalSeparator = "." }, out currencyRate))
                return null;

            return new CurrencyRate
            {
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                Day = day,
                Rate = currencyRate
            };
        }
    }
}