namespace Tee.PerfectChannel.WebApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InvoiceModelAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "Basket_Id", "dbo.Baskets");
            DropForeignKey("dbo.BasketItems", "ItemId", "dbo.Items");
            DropIndex("dbo.BasketItems", new[] { "ItemId" });
            DropIndex("dbo.BasketItems", new[] { "Basket_Id" });
            CreateTable(
                "dbo.InvoiceItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ItemId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    PricePerItem = c.Double(nullable: false),
                    InvoiceId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);

            CreateTable(
                "dbo.Invoices",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            DropColumn("dbo.BasketItems", "Basket_Id");
            AddColumn("dbo.BasketItems", "BasketId", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AddColumn("dbo.BasketItems", "Basket_Id", c => c.Int());
            DropForeignKey("dbo.InvoiceItems", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceItems", new[] { "InvoiceId" });
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceItems");
            CreateIndex("dbo.BasketItems", "Basket_Id");
            CreateIndex("dbo.BasketItems", "ItemId");
            AddForeignKey("dbo.BasketItems", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BasketItems", "Basket_Id", "dbo.Baskets", "Id");
        }
    }
}