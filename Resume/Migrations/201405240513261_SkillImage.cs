namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFiles",
                c => new
                    {
                        UserFileId = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.UserFileId);
            
            AddColumn("dbo.Skills", "LargeIcon_UserFileId", c => c.Int());
            AddColumn("dbo.Skills", "MediumIcon_UserFileId", c => c.Int());
            AddColumn("dbo.Skills", "SmallIcon_UserFileId", c => c.Int());
            CreateIndex("dbo.Skills", "LargeIcon_UserFileId");
            CreateIndex("dbo.Skills", "MediumIcon_UserFileId");
            CreateIndex("dbo.Skills", "SmallIcon_UserFileId");
            AddForeignKey("dbo.Skills", "LargeIcon_UserFileId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "MediumIcon_UserFileId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "SmallIcon_UserFileId", "dbo.UserFiles", "UserFileId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "SmallIcon_UserFileId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIcon_UserFileId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "LargeIcon_UserFileId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "SmallIcon_UserFileId" });
            DropIndex("dbo.Skills", new[] { "MediumIcon_UserFileId" });
            DropIndex("dbo.Skills", new[] { "LargeIcon_UserFileId" });
            DropColumn("dbo.Skills", "SmallIcon_UserFileId");
            DropColumn("dbo.Skills", "MediumIcon_UserFileId");
            DropColumn("dbo.Skills", "LargeIcon_UserFileId");
            DropTable("dbo.UserFiles");
        }
    }
}
