using System;

namespace MobileLife.CurrencyRates.Domain.DomainObjects
{
    public class CurrencyRate
    {
        public CurrencyRate()
        {
        }

        public CurrencyRate(int id, DateTime day, string baseCurrency, string targetCurrency, decimal rate)
        {
            Id = id;
            Day = day;
            BaseCurrency = baseCurrency;
            TargetCurrency = targetCurrency;
            Rate = rate;
        }

        public int Id { get; set; }

        public DateTime Day { get; set; }

        public string BaseCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public decimal Rate { get; set; }
    }
}