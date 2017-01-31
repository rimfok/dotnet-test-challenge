using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Dto
{
    [DataContract]
    public class CurrencyRate
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Day { get; set; }

        [DataMember]
        public string BaseCurrency { get; set; }

        [DataMember]
        public string TargetCurrency { get; set; }

        [DataMember]
        public decimal Rate { get; set; }
    }
}