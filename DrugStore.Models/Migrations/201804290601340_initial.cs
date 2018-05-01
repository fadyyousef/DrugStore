namespace DrugStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drug",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        FormID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        MarketName = c.String(nullable: false, maxLength: 250),
                        ActiveIngredients = c.String(nullable: false, maxLength: 1000),
                        DoseAmount = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableAmount = c.Int(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DrugCategory", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.DrugCompany", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.DrugForm", t => t.FormID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.CategoryID)
                .Index(t => t.FormID);
            
            CreateTable(
                "dbo.DrugCategory",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.DrugCompany",
                c => new
                    {
                        CompanyID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Address = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(maxLength: 25),
                        isLocal = c.Boolean(nullable: false),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.DrugForm",
                c => new
                    {
                        FormID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FormID);
            
            CreateTable(
                "dbo.EmailLog",
                c => new
                    {
                        EmailLogID = c.Int(nullable: false, identity: true),
                        EmailTo = c.String(nullable: false, maxLength: 100),
                        EmailFrom = c.String(nullable: false, maxLength: 100),
                        EmailCC = c.String(nullable: false, maxLength: 250),
                        EmailSubject = c.String(nullable: false, maxLength: 250),
                        EmailBody = c.String(nullable: false),
                        isHTML = c.Boolean(nullable: false),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmailLogID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserRoleID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 25),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserRole", t => t.UserRoleID, cascadeDelete: true)
                .Index(t => t.UserRoleID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(nullable: false, maxLength: 100),
                        UpdatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UserRoleID", "dbo.UserRole");
            DropForeignKey("dbo.Drug", "FormID", "dbo.DrugForm");
            DropForeignKey("dbo.Drug", "CompanyID", "dbo.DrugCompany");
            DropForeignKey("dbo.Drug", "CategoryID", "dbo.DrugCategory");
            DropIndex("dbo.User", new[] { "UserRoleID" });
            DropIndex("dbo.Drug", new[] { "FormID" });
            DropIndex("dbo.Drug", new[] { "CategoryID" });
            DropIndex("dbo.Drug", new[] { "CompanyID" });
            DropTable("dbo.UserRole");
            DropTable("dbo.User");
            DropTable("dbo.EmailLog");
            DropTable("dbo.DrugForm");
            DropTable("dbo.DrugCompany");
            DropTable("dbo.DrugCategory");
            DropTable("dbo.Drug");
        }
    }
}
