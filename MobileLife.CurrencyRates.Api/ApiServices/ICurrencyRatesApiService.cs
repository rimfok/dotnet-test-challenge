using MobileLife.CurrencyRates.Api.Dto;
using System.ServiceModel;

namespace MobileLife.CurrencyRates.Api.ApiServices
{
    [ServiceContract]
    public interface ICurrencyRatesApiService
    {
        [OperationContract]
        GetCurrencyRateResponse GetEuroCurrencyRate(GetCurrencyRateRequest currencyRateRequest);

        [OperationContract]
        ListCurrencyRatesResponse ListStoredCurrencyRates(ListStoredCurrencyRatesRequest currencyRatesRequest);
    }
}