namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Experience : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ExperienceId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        ResponsibilityId = c.Int(nullable: false),
                        SkillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExperienceId)
                .ForeignKey("dbo.Responsibilities", t => t.ResponsibilityId, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.SkillId, cascadeDelete: true)
                .Index(t => t.ResponsibilityId)
                .Index(t => t.SkillId);
            
            AddColumn("dbo.Tags", "Experience_ExperienceId", c => c.Int());
            CreateIndex("dbo.Tags", "Experience_ExperienceId");
            AddForeignKey("dbo.Tags", "Experience_ExperienceId", "dbo.Experiences", "ExperienceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Experience_ExperienceId", "dbo.Experiences");
            DropForeignKey("dbo.Experiences", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Experiences", "ResponsibilityId", "dbo.Responsibilities");
            DropIndex("dbo.Tags", new[] { "Experience_ExperienceId" });
            DropIndex("dbo.Experiences", new[] { "SkillId" });
            DropIndex("dbo.Experiences", new[] { "ResponsibilityId" });
            DropColumn("dbo.Tags", "Experience_ExperienceId");
            DropTable("dbo.Experiences");
        }
    }
}
