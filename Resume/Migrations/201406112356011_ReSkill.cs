namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReSkill : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.UserFiles", t => t.LargeIconId)
                .ForeignKey("dbo.UserFiles", t => t.MediumIconId)
                .ForeignKey("dbo.UserFiles", t => t.SmallIconId)
                .Index(t => t.SmallIconId)
                .Index(t => t.MediumIconId)
                .Index(t => t.LargeIconId);
            
            AddColumn("dbo.Tags", "Skill_SkillId", c => c.Int());
            CreateIndex("dbo.Tags", "Skill_SkillId");
            AddForeignKey("dbo.Tags", "Skill_SkillId", "dbo.Skills", "SkillId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Skill_SkillId", "dbo.Skills");
            DropForeignKey("dbo.Skills", "SmallIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "MediumIconId", "dbo.UserFiles");
            DropForeignKey("dbo.Skills", "LargeIconId", "dbo.UserFiles");
            DropIndex("dbo.Skills", new[] { "LargeIconId" });
            DropIndex("dbo.Skills", new[] { "MediumIconId" });
            DropIndex("dbo.Skills", new[] { "SmallIconId" });
            DropIndex("dbo.Tags", new[] { "Skill_SkillId" });
            DropColumn("dbo.Tags", "Skill_SkillId");
            DropTable("dbo.Skills");
        }
    }
}
