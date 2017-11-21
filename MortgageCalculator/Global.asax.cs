using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MortgageCalculator.Infrastructure;
//using LoggerApi.Models.Repositories;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
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

        protected override IKernel CreateKernel()
        {
            // NHibernate configuration
            var configuration = new NHibernate.Cfg.Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(MortgageHistory).Assembly);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();

            // Ninject
            IKernel container = new StandardKernel();
            container.Load(Assembly.GetExecutingAssembly());

            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<ISessionManager>().To<SessionManager>();
            container.Bind<IRepository>().To<GenericRepository>();

            //            container.Bind<IApplicationService>().To<ApplicationService>();
            //            container.Bind<ITokenService>().To<TokenService>();
            //            container.Bind<ILogService>().To<LogService>();
            return container;
        }
    }
}
