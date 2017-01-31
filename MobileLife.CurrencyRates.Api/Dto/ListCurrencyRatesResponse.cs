using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    public class ListCurrencyRatesResponse : CurrencyRatesBaseResponse
    {
        [DataMember]
        public IEnumerable<CurrencyRate> CurrencyRates;
    }
}