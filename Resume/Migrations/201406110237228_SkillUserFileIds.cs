namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkillUserFileIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Skills", "SmallIconId", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "MediumIconId", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "LargeIconId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Skills", "LargeIconId");
            DropColumn("dbo.Skills", "MediumIconId");
            DropColumn("dbo.Skills", "SmallIconId");
        }
    }
}
