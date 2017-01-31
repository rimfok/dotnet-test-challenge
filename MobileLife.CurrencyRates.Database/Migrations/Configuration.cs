using MobileLife.CurrencyRates.Database.Context;
using MobileLife.CurrencyRates.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MobileLife.CurrencyRates.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CurrencyRatesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CurrencyRatesContext context)
        {
            context.CurrencyRates.AddOrUpdate(
                c => new { c.Day, c.BaseCurrency, c.TargetCurrency }, InitialSeedBuilder().ToArray()
            );
        }

        private static IEnumerable<CurrencyRate> InitialSeedBuilder()
        {
            return new List<CurrencyRate>
            {
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 1),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.856180m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 2),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.851400m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 3),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.845650m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 4),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.849450m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 5),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.854400m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 6),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.856480m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 7),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.856480m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 8),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.856480m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 9),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.866600m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 10),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.869400m
                },
                new CurrencyRate
                {
                    Day = new DateTime(2017, 1, 11),
                    BaseCurrency = "EUR",
                    TargetCurrency = "GBP",
                    Rate = 0.867250m
                }
            };
        }
    }
}