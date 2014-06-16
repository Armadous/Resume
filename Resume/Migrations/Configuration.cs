namespace Resume.Migrations
{
    using Resume.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Resume.Models.ResumeDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Resume.Models.ResumeDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Add sample data if none exists
            if (!context.Positions.Any())
            {
                // Position One
                var samplePositionOne = new Position()
                {
                    Title = "CEO",
                    Company = "Circut City",
                    Description = "Played golf and drank Mojitos.",
                    StartDate = DateTime.Now.AddYears(-10),
                    EndDate = DateTime.Now.AddYears(-5),
                };
                context.Positions.AddOrUpdate(samplePositionOne);

                var boardMeetings = new Responsibility()
                {
                    Name = "Board Meetings",
                    Description = "Command executives in epic rap battle.",
                    Percentage = .75
                };
                samplePositionOne.Responsibilities.Add(boardMeetings);

                var makinMoney = new Responsibility()
                {
                    Name = "Profit",
                    Description = "Get all the dollars",
                    Percentage = .25,
                };
                samplePositionOne.Responsibilities.Add(makinMoney);

                // Position Two
                var samplePositionTwo = new Position()
                {
                    Title = "Fry Cook",
                    Company = "Joes Fry Shack",
                    Description = "Cookied lots of fries",
                    StartDate = DateTime.Now.AddYears(-1),
                    EndDate = DateTime.Now
                };
                context.Positions.Add(samplePositionTwo);

                var grillResponsibility = new Responsibility()
                {
                    Name = "Grill Safety",
                    Description = "Turn off the grill at the end of the day so the shack doesn't burn down, agian.",
                    Percentage = 1
                };
                samplePositionTwo.Responsibilities.Add(grillResponsibility);

                ////Some skills
                var cocktailSkill = new Skill()
                {
                    Name = "Cocktail Mixer",
                    Description = "I make a mean Old-fashioned",
                    Level = 8
                };
                context.Skills.Add(cocktailSkill);

                var retailSkill = new Skill()
                {
                    Name = "Retail",
                    Description = "I sell like a boss",
                    Level = 5
                };
                context.Skills.Add(retailSkill);

                var schmoozing = new Skill()
                {
                    Name = "Schmoozing",
                    Description = "I wine and dine with the best of em",
                    Level = 5
                };
                context.Skills.Add(schmoozing);

                var grilling = new Skill()
                {
                    Name = "Grilling",
                    Description = "The key is a low flame",
                    Level = 2
                };
                context.Skills.Add(grilling);

                //and then some experiences for those skills
                var nineToFiveGrill = new Models.Experience()
                {
                    Value = 30,
                    Responsibility = grillResponsibility,
                    Skill = grilling,
                };
                context.Experiences.Add(nineToFiveGrill);

                var sellingWidgets = new Models.Experience()
                {
                    Value = 50,
                    Responsibility = makinMoney,
                    Skill = retailSkill
                };
                context.Experiences.Add(sellingWidgets);

                var companyBBQ = new Models.Experience()
                {
                    Value = 20,
                    Responsibility = makinMoney,
                    Skill = grilling
                };
                context.Experiences.Add(companyBBQ);

                var winingAndDining = new Models.Experience()
                {
                    Value = 30,
                    Responsibility = makinMoney,
                    Skill = schmoozing
                };
                context.Experiences.Add(winingAndDining);
            }
        }
    }
}
