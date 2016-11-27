namespace Tee.PerfectChannel.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            RenameColumn(table: "dbo.BasketItems", name: "BasketId", newName: "Basket_Id");
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BasketItems", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Baskets", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.BasketItems", "Basket_Id", c => c.Int());
            CreateIndex("dbo.BasketItems", "Basket_Id");
            AddForeignKey("dbo.BasketItems", "Basket_Id", "dbo.Baskets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "Basket_Id", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "Basket_Id" });
            AlterColumn("dbo.BasketItems", "Basket_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Baskets", "UserId");
            DropColumn("dbo.BasketItems", "UserId");
            DropTable("dbo.Users");
            RenameColumn(table: "dbo.BasketItems", name: "Basket_Id", newName: "BasketId");
            CreateIndex("dbo.BasketItems", "BasketId");
            AddForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets", "Id", cascadeDelete: true);
        }
    }
}
