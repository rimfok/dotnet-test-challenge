using MobileLife.CurrencyRates.Api.Enums;
using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    [KnownType(typeof(GetCurrencyRateResponse))]
    [KnownType(typeof(ListCurrencyRatesResponse))]
    public abstract class CurrencyRatesBaseResponse
    {
        [DataMember]
        public ResponseCode ResponseCode { get; set; }
    }
}