namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserFiles", "ContentType", c => c.String());
            AddColumn("dbo.UserFiles", "LocalFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserFiles", "LocalFileName");
            DropColumn("dbo.UserFiles", "ContentType");
        }
    }
}
