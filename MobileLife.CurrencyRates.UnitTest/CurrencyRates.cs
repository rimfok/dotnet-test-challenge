using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MobileLife.CurrencyRates.Database.Context;
using MobileLife.CurrencyRates.Database.DataAgents;
using Moq;
using NUnit.Framework;

namespace MobileLife.CurrencyRates.UnitTest
{
    [TestFixture]
    public class CurrencyRates
    {
        [Test]
        public void ListCurrencyRates_RatesIsInDb_ShouldReturnSpecificNumberRates()
        {
            var expected = new List<Database.Models.CurrencyRate>
            {
                new Database.Models.CurrencyRate { Id = 1, Day = DateTime.Today.AddDays(-2), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 2, Day = DateTime.Today.AddDays(-1), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 3, Day = DateTime.Today, BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m }
            };

            var context = new Mock<ICurrencyRatesContext>();
            context.Setup(_ => _.CurrencyRates).Returns(DbSetMocking.CreateMockSet(expected.AsQueryable()).Object);

            var agent = new CurrencyRatesDataAgent(context.Object);
            var items = agent.ListCurrencyRates(0, 3);

            Assert.AreEqual(expected.Count, items.Count());
        }

        [Test]
        public void GetCurrencyRate_RatesIsInDb_ShouldReturnSpecificRate()
        {
            var expected = new List<Database.Models.CurrencyRate>
            {
                new Database.Models.CurrencyRate { Id = 1, Day = DateTime.Today.AddDays(-2), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 2, Day = DateTime.Today.AddDays(-1), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 3, Day = DateTime.Today, BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m }
            };

            var context = new Mock<ICurrencyRatesContext>();
            context.Setup(_ => _.CurrencyRates).Returns(DbSetMocking.CreateMockSet(expected.AsQueryable()).Object);

            var agent = new CurrencyRatesDataAgent(context.Object);
            var rate = agent.GetCurrencyRate(DateTime.Today.AddDays(-2), "EUR", "GBP");

            Assert.AreEqual(expected[0].Id, rate.Id);
        }

        [Test]
        public void SaveCurrencyRate_RateIsNotNull_NewRateIsSaved()
        {
            var expected = new Database.Models.CurrencyRate { Id = 4, Day = DateTime.Today, BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m };

            var initialData = new List<Database.Models.CurrencyRate>
            {
                new Database.Models.CurrencyRate { Id = 1, Day = DateTime.Today.AddDays(-3), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 2, Day = DateTime.Today.AddDays(-2), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 3, Day = DateTime.Today.AddDays(-1), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m }
            };

            var context = new Mock<ICurrencyRatesContext>();
            context.Setup(_ => _.CurrencyRates).Returns(DbSetMocking.CreateMockSet(initialData.AsQueryable()).Object);
            context.Setup(_ => _.SaveChanges()).Returns(1);

            var agent = new CurrencyRatesDataAgent(context.Object);
            var result = agent.SaveCurrencyRate(expected);

            Assert.True(result);
        }
    }

    public static class DbSetMocking
    {
        public static Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data)
            where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                .Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression)
                .Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType)
                .Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
                .Returns(queryableData.GetEnumerator());
            return mockSet;
        }
    }
}