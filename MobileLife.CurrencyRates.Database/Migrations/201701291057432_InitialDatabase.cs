namespace MobileLife.CurrencyRates.Database.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrencyRates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Day = c.DateTime(nullable: false, storeType: "date"),
                    BaseCurrency = c.String(nullable: false, maxLength: 3),
                    TargetCurrency = c.String(nullable: false, maxLength: 3),
                    Rate = c.Decimal(nullable: false, precision: 12, scale: 6),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.CurrencyRates");
        }
    }
}