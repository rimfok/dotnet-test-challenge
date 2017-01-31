using MobileLife.CurrencyRates.Database.Context;
using MobileLife.CurrencyRates.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileLife.CurrencyRates.Database.DataAgents
{
    public class CurrencyRatesDataAgent : ICurrencyRatesDataAgent
    {
        private readonly ICurrencyRatesContext _context;

        public CurrencyRatesDataAgent(ICurrencyRatesContext context)
        {
            _context = context;
        }

        public IEnumerable<CurrencyRate> ListCurrencyRates(int start, int limit)
        {
            return _context.CurrencyRates.Where(c => c.Id > start).Take(limit).ToList();
        }

        public CurrencyRate GetCurrencyRate(DateTime date, string baseCurrency, string targetCurrency)
        {
            return
                _context.CurrencyRates
                    .FirstOrDefault(c =>
                        string.Equals(c.BaseCurrency, baseCurrency) &&
                        string.Equals(c.TargetCurrency, targetCurrency) &&
                        DateTime.Equals(c.Day, date));
        }

        public bool SaveCurrencyRate(CurrencyRate currencyRate)
        {
            _context.CurrencyRates.Add(currencyRate);
            return _context.SaveChanges() > 0;
        }
    }
}