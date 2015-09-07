using System;
using Resume.Migrations;
using Resume.IdentityMigrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;

namespace Resume.Tests
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void Migrate()
        {
            var migrateConifg = new Resume.Migrations.Configuration();
            var migrator = new DbMigrator(migrateConifg);
            var script = new MigratorScriptingDecorator(migrator);

            script.ScriptUpdate(DbMigrator.InitialDatabase, null);
        }

        [TestMethod]
        public void MigrateIdentity()
        {
            var migrateConifg = new  Resume.IdentityMigrations.Configuration();
            var migrator = new DbMigrator(migrateConifg);
            var script = new MigratorScriptingDecorator(migrator);

            script.ScriptUpdate(DbMigrator.InitialDatabase, null);
        }
    }
}
