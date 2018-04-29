namespace DrugStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nonrequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmailLog", "EmailCC", c => c.String(maxLength: 250));
            AlterColumn("dbo.User", "MiddleName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "MiddleName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.EmailLog", "EmailCC", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
