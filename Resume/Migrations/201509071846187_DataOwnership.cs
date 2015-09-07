namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataOwnership : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "OwnerIdentity", c => c.String(nullable: false, defaultValue: "Josh"));
            AddColumn("dbo.Skills", "OwnerIdentity", c => c.String(nullable: false, defaultValue: "Josh"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "OwnerIdentity");
            DropColumn("dbo.Positions", "OwnerIdentity");
        }
    }
}
