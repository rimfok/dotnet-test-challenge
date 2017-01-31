using MobileLife.CurrencyRates.Database.DataAgents;
using MobileLife.CurrencyRates.Database.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileLife.CurrencyRates.UnitTest
{
    [TestFixture]
    public class DatabaseCurrencyRatePersistenceService
    {
        [Test]
        public void GetCurrencyRate_Queried_ItShouldReturnRate()
        {
            var expected = new Database.Models.CurrencyRate { Id = 1, Day = DateTime.Today, BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m };

            var ratesAgent = new Mock<ICurrencyRatesDataAgent>();
            ratesAgent.Setup(_ => _.GetCurrencyRate(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);

            var service = new Domain.PersistenceServices.DatabaseCurrencyRatePersistenceService(ratesAgent.Object);
            var actual = service.GetCurrencyRate(DateTime.Today, "EUR", "GBP");

            Assert.AreEqual(expected.Id, actual.Id);
        }

        [Test]
        public void ListCurrencyRates_Queried_ItShouldReturnRates()
        {
            var expected = new List<Database.Models.CurrencyRate>
            {
                new Database.Models.CurrencyRate { Id = 1, Day = DateTime.Today.AddDays(-10), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 2, Day = DateTime.Today.AddDays(-9), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 3, Day = DateTime.Today.AddDays(-8), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 4, Day = DateTime.Today.AddDays(-7), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 5, Day = DateTime.Today.AddDays(-6), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 6, Day = DateTime.Today.AddDays(-5), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 7, Day = DateTime.Today.AddDays(-4), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 8, Day = DateTime.Today.AddDays(-3), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 9, Day = DateTime.Today.AddDays(-2), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
                new Database.Models.CurrencyRate { Id = 10, Day = DateTime.Today.AddDays(-1), BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m },
            };

            var ratesAgent = new Mock<ICurrencyRatesDataAgent>();
            ratesAgent.Setup(_ => _.ListCurrencyRates(It.IsAny<int>(), It.IsAny<int>())).Returns(expected);

            var service = new Domain.PersistenceServices.DatabaseCurrencyRatePersistenceService(ratesAgent.Object);
            var actual = service.ListCurrencyRates(0, 10);

            Assert.AreEqual(expected.Count, actual.Count());
        }

        [Test]
        public void SaveCurrencyRate_IsNotNull_ItShouldSave()
        {
            var expected = new Domain.DomainObjects.CurrencyRate { Id = 1, Day = DateTime.Today, BaseCurrency = "EUR", TargetCurrency = "GBP", Rate = 0.85m };

            var ratesAgent = new Mock<ICurrencyRatesDataAgent>();
            ratesAgent.Setup(_ => _.SaveCurrencyRate(It.IsAny<CurrencyRate>())).Returns(true);

            var service = new Domain.PersistenceServices.DatabaseCurrencyRatePersistenceService(ratesAgent.Object);
            var result = service.SaveCurrencyRate(expected);

            Assert.IsTrue(result);
        }
    }
}