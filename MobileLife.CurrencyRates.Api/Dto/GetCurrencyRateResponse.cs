using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    public class GetCurrencyRateResponse : CurrencyRatesBaseResponse
    {
        [DataMember]
        public decimal? Rate { get; set; }
    }
}