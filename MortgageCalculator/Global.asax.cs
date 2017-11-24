using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MortgageCalculator.Infrastructure;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
using MortgageCalculator.Services;
using NHibernate;
using Ninject;
using Ninject.Web.Common.WebHost;
using WebGrease.Configuration;

namespace MortgageCalculator
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Configure dependency injection with Ninject
        /// </summary>
        /// <returns></returns>
        protected override IKernel CreateKernel()
        {
            // NHibernate configuration
            var configuration = new NHibernate.Cfg.Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(MortgageEntry).Assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            // Ninject
            IKernel container = new StandardKernel();
            container.Load(Assembly.GetExecutingAssembly());

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISessionManager>().To<SessionManager>();
            container.Bind<IRepository>().To<GenericRepository>();
            container.Bind<IMortgageService>().To<MortgageService>();
            return container;
        }
    }
}
