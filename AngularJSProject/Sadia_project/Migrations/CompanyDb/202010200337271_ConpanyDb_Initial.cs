namespace Sadia_Project.Migrations.CompanyDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConpanyDb_Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 40),
                        Eshtablished = c.DateTime(nullable: false, storeType: "date"),
                        Address = c.String(maxLength: 200),
                        ContactEmail = c.String(nullable: false, maxLength: 50),
                        Web = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 40),
                        Type = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false, maxLength: 40),
                        IsContactual = c.Boolean(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Services", new[] { "CompanyId" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropTable("dbo.Services");
            DropTable("dbo.Products");
            DropTable("dbo.Companies");
        }
    }
}
