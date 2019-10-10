using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using WebApi.Controllers;
using Service;
using Data.Repsitory;
using Data;
using Autofac.Integration.Mvc;
//using Autofac;
//using Autofac.Integration.WebApi;
using System.Reflection;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacRegister();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType(typeof(Repository<BankDbContext>)).AsImplementedInterfaces();
            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }

        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(CustomerController).Assembly);
            builder.RegisterType(typeof(CustomerService)).As<ICustomerService>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            //builder.RegisterType(typeof(UnitOfWork<BankDbContext>)).As<IRepository<BankDbContext>>();
            builder.RegisterGeneric(typeof(UnitOfWork<>)).As(typeof(IUnitOfWork<>));
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterFilterProvider();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);



        }
    }
}
