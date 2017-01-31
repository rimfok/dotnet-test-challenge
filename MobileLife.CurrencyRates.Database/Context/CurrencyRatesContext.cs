using MobileLife.CurrencyRates.Database.Migrations;
using MobileLife.CurrencyRates.Database.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace MobileLife.CurrencyRates.Database.Context
{
    public class CurrencyRatesContext : DbContext, ICurrencyRatesContext
    {
        static CurrencyRatesContext()
        {
            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<CurrencyRatesContext, Configuration>());

            var type = typeof(SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");
        }

        public CurrencyRatesContext()
            : base("name=CurrencyRatesData_ConnectionString")
        {
        }

        public DbSet<CurrencyRate> CurrencyRates { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>()
                .Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<CurrencyRate>().Property(c => c.Rate).HasPrecision(12, 6);

            base.OnModelCreating(modelBuilder);
        }
    }
}