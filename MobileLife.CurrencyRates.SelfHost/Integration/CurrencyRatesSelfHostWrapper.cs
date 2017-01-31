using Castle.Facilities.WcfIntegration;
using MobileLife.CurrencyRates.Api.ApiServices;
using System;
using System.ServiceModel;

namespace MobileLife.CurrencyRates.SelfHost.Integration
{
    internal class CurrencyRatesSelfHostWrapper : ICurrencyRatesSelfHostWrapper
    {
        private readonly string _serviceName;
        private ServiceHostBase _serviceHost;

        public CurrencyRatesSelfHostWrapper(string serviceName)
        {
            _serviceName = serviceName;
        }

        public CurrencyRatesSelfHostWrapper()
        {
        }

        public void Start()
        {
            Console.WriteLine(_serviceName + " starting...");
            var openSucceeded = false;

            try
            {
                _serviceHost?.Close();

                // ReSharper disable once AssignNullToNotNullAttribute
                _serviceHost =
                    new DefaultServiceHostFactory().CreateServiceHost(
                        typeof(ICurrencyRatesApiService).AssemblyQualifiedName, new Uri[] { });
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Caught exception while creating " + _serviceName + ": " + e);
                return;
            }

            try
            {
                _serviceHost?.Open();
                openSucceeded = true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Caught exception while starting " + _serviceName + ": " + ex);
            }
            finally
            {
                if (!openSucceeded)
                    _serviceHost?.Abort();
            }

            if (_serviceHost != null && _serviceHost.State == CommunicationState.Opened)
            {
                Console.WriteLine(_serviceName + " started");
            }
            else
            {
                Console.WriteLine(_serviceName + " failed to open");
                var closeSucceeded = false;
                try
                {
                    _serviceHost?.Close();
                    closeSucceeded = true;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("{0} failed to close: {1}", _serviceName, ex);
                }
                finally
                {
                    if (!closeSucceeded)
                        _serviceHost?.Abort();
                }
            }
        }

        public void Stop()
        {
            Console.WriteLine(_serviceName + " stopping...");
            try
            {
                if (_serviceHost == null) return;
                _serviceHost.Close();
                _serviceHost = null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Caught exception while stopping " + _serviceName + ": " + ex);
            }
            finally
            {
                Console.WriteLine(_serviceName + " stopped...");
            }
        }
    }
}