using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume.Models
{
    public class NHibernateSessionFactory
    {
        public ISessionFactory GetSessionFactory()
        {
            var map = AutoMap.AssemblyOf<Entity>()
                .Where(t => typeof(Entity).IsAssignableFrom(t));

            var session = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("DefaultConnection")))
                .Mappings(m => m.AutoMappings.Add(map))
                .ExposeConfiguration(UpdateSchema)
                .BuildSessionFactory();

            return session;
        }

        public static void UpdateSchema(Configuration config)
        {
            var update = new SchemaUpdate(config);
            update.Execute(false, true);
        }
    }
}