using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Ninject.Activation;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Resume.Models
{
    public class NHibernateSessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new NHibernateSessionFactory();
            return sessionFactory.GetSessionFactory();
        }
    }

    public class NHibernateModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>().ToProvider<NHibernateSessionFactoryProvider>().InSingletonScope();
            Bind<ISession>().ToMethod(context => context.Kernel.Get<ISessionFactory>().OpenSession()).InRequestScope();
        }
    }
}