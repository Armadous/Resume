namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstgo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Company = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PositionId);
            
            CreateTable(
                "dbo.Responsibilities",
                c => new
                    {
                        ResponsibilityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Percentage = c.Double(nullable: false),
                        Position_PositionId = c.Int(),
                    })
                .PrimaryKey(t => t.ResponsibilityId)
                .ForeignKey("dbo.Positions", t => t.Position_PositionId)
                .Index(t => t.Position_PositionId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Responsibility_ResponsibilityId = c.Int(),
                        Position_PositionId = c.Int(),
                        Skill_SkillId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Responsibilities", t => t.Responsibility_ResponsibilityId)
                .ForeignKey("dbo.Positions", t => t.Position_PositionId)
                .ForeignKey("dbo.Skills", t => t.Skill_SkillId)
                .Index(t => t.Responsibility_ResponsibilityId)
                .Index(t => t.Position_PositionId)
                .Index(t => t.Skill_SkillId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Level = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Skill_SkillId", "dbo.Skills");
            DropForeignKey("dbo.Tags", "Position_PositionId", "dbo.Positions");
            DropForeignKey("dbo.Tags", "Responsibility_ResponsibilityId", "dbo.Responsibilities");
            DropForeignKey("dbo.Responsibilities", "Position_PositionId", "dbo.Positions");
            DropIndex("dbo.Tags", new[] { "Skill_SkillId" });
            DropIndex("dbo.Tags", new[] { "Position_PositionId" });
            DropIndex("dbo.Tags", new[] { "Responsibility_ResponsibilityId" });
            DropIndex("dbo.Responsibilities", new[] { "Position_PositionId" });
            DropTable("dbo.Skills");
            DropTable("dbo.Tags");
            DropTable("dbo.Responsibilities");
            DropTable("dbo.Positions");
        }
    }
}
