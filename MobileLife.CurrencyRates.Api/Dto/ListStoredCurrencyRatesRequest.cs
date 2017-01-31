using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    public class ListStoredCurrencyRatesRequest
    {
        [DataMember]
        public int StartId { get; set; }
    }
}