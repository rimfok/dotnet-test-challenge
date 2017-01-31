using MobileLife.CurrencyRates.Domain.LietuvosBankas.CurrencyRates;
using MobileLife.CurrencyRates.Domain.ServiceAgents;
using Moq;
using NUnit.Framework;
using System;
using System.Xml;

namespace MobileLife.CurrencyRates.UnitTest
{
    [TestFixture]
    public class LBankCurrencyRates
    {
        [Test]
        public void FetchCurrencyRate_Queried_GetRate()
        {
            var date = DateTime.Parse("2017-01-27");
            var doc = new XmlDocument();
            doc.LoadXml($@"<FxRate xmlns=""http://www.lb.lt/WebServices/FxRates""><Tp>EU</Tp><Dt>{date:yyyy-MM-dd}</Dt><CcyAmt><Ccy>EUR</Ccy><Amt>1</Amt></CcyAmt><CcyAmt><Ccy>GBP</Ccy><Amt>0.8517</Amt></CcyAmt></FxRate>");
            var mock = new Mock<FxRatesSoap>();
            mock.Setup(_ => _.getFxRatesForCurrency(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(doc.DocumentElement);

            var agent = new LBankCurrencyRatesServiceAgent(mock.Object);
            var result = agent.FetchCurrencyRate(date, "EUR", "GBP");

            Assert.AreEqual(date, result.Day);
        }
    }
}