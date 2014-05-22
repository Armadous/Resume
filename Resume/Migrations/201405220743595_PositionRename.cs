namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PositionRename : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "EndDate", c => c.DateTime());
            RenameColumn(table: "dbo.Positions", name: "Name", newName: "Title");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Positions", "EndDate", c => c.DateTime(nullable: false));
            RenameColumn(table: "dbo.Positions", name: "Title", newName: "Name");
        }
    }
}
