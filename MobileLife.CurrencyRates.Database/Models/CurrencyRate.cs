using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileLife.CurrencyRates.Database.Models
{
    public class CurrencyRate
    {
        public CurrencyRate()
        {
        }

        public CurrencyRate(int id, DateTime day, string baseCurrency, string targetCurrency, decimal rate)
        {
            Id = id;
            Day = day;
            BaseCurrency = baseCurrency;
            TargetCurrency = targetCurrency;
            Rate = rate;
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        [Required]
        public DateTime Day { get; set; }

        [Required]
        [StringLength(3)]
        public string BaseCurrency { get; set; }

        [Required]
        [StringLength(3)]
        public string TargetCurrency { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}