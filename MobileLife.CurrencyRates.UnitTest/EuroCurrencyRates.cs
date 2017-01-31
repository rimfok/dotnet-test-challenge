using System;
using MobileLife.CurrencyRates.Domain.DomainObjects;
using MobileLife.CurrencyRates.Domain.DomainServices;
using MobileLife.CurrencyRates.Domain.PersistenceServices;
using MobileLife.CurrencyRates.Domain.ServiceAgents;
using Moq;
using NUnit.Framework;

namespace MobileLife.CurrencyRates.UnitTest
{
    [TestFixture]
    public class EuroCurrencyRates
    {
        [Test]
        public void GetEuroCurrencyRate_RateIsNotFetchedYet_ItShouldFetchFromOutsideWS()
        {
            var expectedRate = new CurrencyRate(1, DateTime.Today, "EUR", "GBP", 0.85m);

            var factory = new MockRepository(MockBehavior.Loose);
            var persistanceService = factory.Create<ICurrencyRatesPersistenceService>();
            persistanceService.Setup(_ => _.GetCurrencyRate(DateTime.Today, "EUR", "GBP")).Returns(default(CurrencyRate));

            var ratesServiceAgent = factory.Create<ICurrencyRatesServiceAgent>();
            ratesServiceAgent.Setup(_ => _.FetchCurrencyRate(DateTime.Today, "EUR", "GBP")).Returns(expectedRate);

            IEuroCurrencyRatesService euroCurrencyRatesService = new EuroCurrencyRatesService(persistanceService.Object, ratesServiceAgent.Object);

            var actualRate = euroCurrencyRatesService.GetEuroCurrencyRate(DateTime.Today, "GBP");

            persistanceService.Verify(_ => _.GetCurrencyRate(DateTime.Today, "EUR", "GBP"), Times.Once());
            ratesServiceAgent.Verify(_ => _.FetchCurrencyRate(DateTime.Today, "EUR", "GBP"), Times.Once());
            persistanceService.Verify(_ => _.SaveCurrencyRate(It.IsAny<CurrencyRate>()), Times.Once());
            Assert.AreEqual(expectedRate.Id, actualRate.Id);
        }

        [Test]
        public void GetEuroCurrencyRate_RateIsAlreadyFetched_ItShouldFetchFromDb()
        {
            var expectedRate = new CurrencyRate(1, DateTime.Today, "EUR", "GBP", 0.85m);

            var factory = new MockRepository(MockBehavior.Loose);
            var persistanceService = factory.Create<ICurrencyRatesPersistenceService>();
            persistanceService.Setup(_ => _.GetCurrencyRate(DateTime.Today, "EUR", "GBP")).Returns(expectedRate);

            var ratesServiceAgent = factory.Create<ICurrencyRatesServiceAgent>();
            ratesServiceAgent.Setup(_ => _.FetchCurrencyRate(DateTime.Today, "EUR", "GBP")).Returns(new CurrencyRate(2, DateTime.Today, "EUR", "GBP", 0.85m));

            IEuroCurrencyRatesService euroCurrencyRatesService = new EuroCurrencyRatesService(persistanceService.Object, ratesServiceAgent.Object);

            var actualRate = euroCurrencyRatesService.GetEuroCurrencyRate(DateTime.Today, "GBP");

            persistanceService.Verify(_ => _.GetCurrencyRate(DateTime.Today, "EUR", "GBP"), Times.Once());
            ratesServiceAgent.Verify(_ => _.FetchCurrencyRate(DateTime.Today, "EUR", "GBP"), Times.Never());
            persistanceService.Verify(_ => _.SaveCurrencyRate(It.IsAny<CurrencyRate>()), Times.Never());
            Assert.AreEqual(expectedRate.Id, actualRate.Id);
        }
    }
}