namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSkills : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Tags", "Skill_SkillId", "dbo.Skills");
            DropIndex("dbo.Tags", new[] { "Skill_SkillId" });
            DropIndex("dbo.Skills", new[] { "SmallIconId" });
            DropIndex("dbo.Skills", new[] { "MediumIconId" });
            DropIndex("dbo.Skills", new[] { "LargeIconId" });
            DropColumn("dbo.Tags", "Skill_SkillId");
            DropTable("dbo.Skills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Level = c.Single(nullable: false),
                        SmallIconId = c.Int(),
                        MediumIconId = c.Int(),
                        LargeIconId = c.Int(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            AddColumn("dbo.Tags", "Skill_SkillId", c => c.Int());
            CreateIndex("dbo.Skills", "LargeIconId");
            CreateIndex("dbo.Skills", "MediumIconId");
            CreateIndex("dbo.Skills", "SmallIconId");
            CreateIndex("dbo.Tags", "Skill_SkillId");
            AddForeignKey("dbo.Tags", "Skill_SkillId", "dbo.Skills", "SkillId");
            AddForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles", "UserFileId");
            AddForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles", "UserFileId");
        }
    }
}
