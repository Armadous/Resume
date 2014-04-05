namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PocoRelationIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responsibilities", "Position_PositionId", "dbo.Positions");
            DropIndex("dbo.Responsibilities", new[] { "Position_PositionId" });
            RenameColumn(table: "dbo.Responsibilities", name: "Position_PositionId", newName: "PositionId");
            AlterColumn("dbo.Responsibilities", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Responsibilities", "PositionId");
            AddForeignKey("dbo.Responsibilities", "PositionId", "dbo.Positions", "PositionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responsibilities", "PositionId", "dbo.Positions");
            DropIndex("dbo.Responsibilities", new[] { "PositionId" });
            AlterColumn("dbo.Responsibilities", "PositionId", c => c.Int());
            RenameColumn(table: "dbo.Responsibilities", name: "PositionId", newName: "Position_PositionId");
            CreateIndex("dbo.Responsibilities", "Position_PositionId");
            AddForeignKey("dbo.Responsibilities", "Position_PositionId", "dbo.Positions", "PositionId");
        }
    }
}
