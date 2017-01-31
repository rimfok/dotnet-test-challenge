using System;
using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    public class GetCurrencyRateRequest
    {
        [DataMember]
        public DateTime Day { get; set; }

        [DataMember]
        public string Currency { get; set; }
    }
}