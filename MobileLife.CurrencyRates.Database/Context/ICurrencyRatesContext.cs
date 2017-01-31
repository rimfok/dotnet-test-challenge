using MobileLife.CurrencyRates.Database.Models;
using System.Data.Entity;

namespace MobileLife.CurrencyRates.Database.Context
{
    public interface ICurrencyRatesContext
    {
        DbSet<CurrencyRate> CurrencyRates { get; set; }

        int SaveChanges();
    }
}