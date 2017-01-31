using MobileLife.CurrencyRates.Api.Dto;
using MobileLife.CurrencyRates.Api.Enums;
using MobileLife.CurrencyRates.Api.Extensions;
using MobileLife.CurrencyRates.Domain.DomainServices;
using System;
using System.Linq;

namespace MobileLife.CurrencyRates.Api.ApiServices
{
    public class CurrencyRatesApiService : ICurrencyRatesApiService
    {
        private readonly IEuroCurrencyRatesService _euroCurrencyRatesService;
        private readonly IExportCurrencyRatesService _exportCurrencyRatesService;

        public CurrencyRatesApiService(IEuroCurrencyRatesService euroCurrencyRatesService,
            IExportCurrencyRatesService exportCurrencyRatesService)
        {
            _euroCurrencyRatesService = euroCurrencyRatesService;
            _exportCurrencyRatesService = exportCurrencyRatesService;
        }

        public GetCurrencyRateResponse GetEuroCurrencyRate(GetCurrencyRateRequest currencyRateRequest)
        {
            try
            {
                var currencyRate = _euroCurrencyRatesService.GetEuroCurrencyRate(currencyRateRequest.Day,
                    currencyRateRequest.Currency.ToUpper())?.Rate;

                return new GetCurrencyRateResponse
                {
                    ResponseCode = currencyRate != null ? ResponseCode.Success : ResponseCode.CurrencyRateNotFound,
                    Rate = currencyRate
                };
            }
            catch (Exception ex)
            {
                return HandleExceptionResponse<GetCurrencyRateResponse>(ex);
            }
        }

        public ListCurrencyRatesResponse ListStoredCurrencyRates(ListStoredCurrencyRatesRequest currencyRatesRequest)
        {
            try
            {
                var currencyRatesList = _exportCurrencyRatesService.ListCurrencyRates(currencyRatesRequest.StartId);

                return new ListCurrencyRatesResponse
                {
                    ResponseCode = ResponseCode.Success,
                    CurrencyRates = currencyRatesList.Select(c => c.ToDto()).ToList()
                };
            }
            catch (Exception ex)
            {
                return HandleExceptionResponse<ListCurrencyRatesResponse>(ex);
            }
        }

        private T HandleExceptionResponse<T>(Exception ex) where T : CurrencyRatesBaseResponse, new()
        {
            Console.Error.WriteLine(ex.Message);
            return new T
            {
                ResponseCode = ResponseCode.InternalError
            };
        }
    }
}