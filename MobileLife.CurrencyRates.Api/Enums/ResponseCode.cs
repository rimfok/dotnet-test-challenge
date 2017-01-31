using System.Runtime.Serialization;

namespace MobileLife.CurrencyRates.Api.Enums
{
    [DataContract]
    public enum ResponseCode
    {
        [EnumMember(Value = "200")]
        Success,

        [EnumMember(Value = "404")]
        CurrencyRateNotFound,

        [EnumMember(Value = "500")]
        InternalError
    }
}