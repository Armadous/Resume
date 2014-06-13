namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillIconForeignKeyAnnotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "LargeIcon_UserFileId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIcon_UserFileId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "SmallIcon_UserFileId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "LargeIcon_UserFileId" });
            DropIndex("dbo.Skills", new[] { "MediumIcon_UserFileId" });
            DropIndex("dbo.Skills", new[] { "SmallIcon_UserFileId" });
            DropColumn("dbo.Skills", "LargeIconId");
            DropColumn("dbo.Skills", "MediumIconId");
            DropColumn("dbo.Skills", "SmallIconId");
            RenameColumn(table: "dbo.Skills", name: "LargeIcon_UserFileId", newName: "LargeIconId");
            RenameColumn(table: "dbo.Skills", name: "MediumIcon_UserFileId", newName: "MediumIconId");
            RenameColumn(table: "dbo.Skills", name: "SmallIcon_UserFileId", newName: "SmallIconId");
            AlterColumn("dbo.Skills", "LargeIconId", c => c.Int(nullable: true));
            AlterColumn("dbo.Skills", "MediumIconId", c => c.Int(nullable: true));
            AlterColumn("dbo.Skills", "SmallIconId", c => c.Int(nullable: true));
            CreateIndex("dbo.Skills", "SmallIconId");
            CreateIndex("dbo.Skills", "MediumIconId");
            CreateIndex("dbo.Skills", "LargeIconId");
            AddForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: false);
            AddForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: false);
            AddForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "LargeIconId" });
            DropIndex("dbo.Skills", new[] { "MediumIconId" });
            DropIndex("dbo.Skills", new[] { "SmallIconId" });
            AlterColumn("dbo.Skills", "SmallIconId", c => c.Int());
            AlterColumn("dbo.Skills", "MediumIconId", c => c.Int());
            AlterColumn("dbo.Skills", "LargeIconId", c => c.Int());
            RenameColumn(table: "dbo.Skills", name: "SmallIconId", newName: "SmallIcon_UserFileId");
            RenameColumn(table: "dbo.Skills", name: "MediumIconId", newName: "MediumIcon_UserFileId");
            RenameColumn(table: "dbo.Skills", name: "LargeIconId", newName: "LargeIcon_UserFileId");
            AddColumn("dbo.Skills", "SmallIconId", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "MediumIconId", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "LargeIconId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "SmallIcon_UserFileId");
            CreateIndex("dbo.Skills", "MediumIcon_UserFileId");
            CreateIndex("dbo.Skills", "LargeIcon_UserFileId");
            AddForeignKey("dbo.Skills", "SmallIcon_UserFileId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "MediumIcon_UserFileId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "LargeIcon_UserFileId", "dbo.UserFiles", "UserFileId");
        }
    }
}
