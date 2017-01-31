using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MobileLife.CurrencyRates.Api.ApiServices;
using MobileLife.CurrencyRates.Database.Context;
using MobileLife.CurrencyRates.Database.DataAgents;
using MobileLife.CurrencyRates.Domain.DomainServices;
using MobileLife.CurrencyRates.Domain.LietuvosBankas.CurrencyRates;
using MobileLife.CurrencyRates.Domain.PersistenceServices;
using MobileLife.CurrencyRates.Domain.ServiceAgents;
using MobileLife.CurrencyRates.SelfHost.Integration;

namespace MobileLife.CurrencyRates.SelfHost.Ioc
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InitializeDomain(container);
            container.Register(Component.For<IWindsorContainer>().Instance(container).LifestyleSingleton());
        }

        private void InitializeDomain(IWindsorContainer container)
        {
            // Self Host Service
            RegisterDependency<ICurrencyRatesSelfHostWrapper, CurrencyRatesSelfHostWrapper>(container);

            // Database
            RegisterDependency<ICurrencyRatesContext, CurrencyRatesContext>(container);
            RegisterDependency<ICurrencyRatesDataAgent, CurrencyRatesDataAgent>(container);

            // Domain Persistence
            RegisterDependency<ICurrencyRatesPersistenceService, DatabaseCurrencyRatePersistenceService>(container);

            // Domain SOAP Services
            RegisterDependency<FxRatesSoap, FxRatesSoapClient>(container);

            // Domain Service Agents
            RegisterDependency<ICurrencyRatesServiceAgent, LBankCurrencyRatesServiceAgent>(container);

            // Domain Services
            RegisterDependency<IEuroCurrencyRatesService, EuroCurrencyRatesService>(container);
            RegisterDependency<IExportCurrencyRatesService, ExportCurrencyRatesService>(container);

            // API Services
            RegisterDependency<ICurrencyRatesApiService, CurrencyRatesApiService>(container);
        }

        private void RegisterDependency<T, TU>(IWindsorContainer container)
            where T : class
            where TU : T
        {
            container.Register(Component.For<T>().ImplementedBy<TU>().LifestyleTransient());
        }
    }
}