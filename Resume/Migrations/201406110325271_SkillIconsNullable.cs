namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillIconsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "SmallIconId" });
            DropIndex("dbo.Skills", new[] { "MediumIconId" });
            DropIndex("dbo.Skills", new[] { "LargeIconId" });
            AlterColumn("dbo.Skills", "SmallIconId", c => c.Int());
            AlterColumn("dbo.Skills", "MediumIconId", c => c.Int());
            AlterColumn("dbo.Skills", "LargeIconId", c => c.Int());
            CreateIndex("dbo.Skills", "SmallIconId");
            CreateIndex("dbo.Skills", "MediumIconId");
            CreateIndex("dbo.Skills", "LargeIconId");
            AddForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles", "UserFileId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "LargeIconId" });
            DropIndex("dbo.Skills", new[] { "MediumIconId" });
            DropIndex("dbo.Skills", new[] { "SmallIconId" });
            AlterColumn("dbo.Skills", "LargeIconId", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "MediumIconId", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "SmallIconId", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "LargeIconId");
            CreateIndex("dbo.Skills", "MediumIconId");
            CreateIndex("dbo.Skills", "SmallIconId");
            AddForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: true);
            AddForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles", "UserFileId", cascadeDelete: true);
        }
    }
}
