using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MobileLife.CurrencyRates.SelfHost.Integration;
using Topshelf;

namespace MobileLife.CurrencyRates.SelfHost
{
    internal class Program
    {
        private static void Main()
        {
            const string serviceName = "MobileLifeCurrencyRatesService";
            const string serviceDisplayName = "MobileLife Currency Rates Service";
            const string serviceDescription = "Service providing currency rate data";

            var container = new WindsorContainer().AddFacility<WcfFacility>().Install(FromAssembly.This());

            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<ICurrencyRatesSelfHostWrapper>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(
                        name => container.Resolve<ICurrencyRatesSelfHostWrapper>(new { serviceName = serviceDisplayName }));
                    serviceConfigurator.WhenStarted(service => service.Start());
                    serviceConfigurator.WhenStopped(service => { service.Stop(); });
                });

                hostConfigurator.SetServiceName(serviceName);
                hostConfigurator.SetDisplayName(serviceDisplayName);
                hostConfigurator.SetDescription(serviceDescription);
            });
        }
    }
}