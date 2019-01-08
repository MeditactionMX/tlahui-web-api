using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using System.Data.Entity;
using Tlahui.Context.WebAPI;
using Tlahui.Repositories.Store;
using Tlahui.Services.Store;
using Tlahui.Repositories.Infrastructure.CachedResources;
using Tlahui.Repositories.Infrastructure.DynamicForms;
using Infrastructure.providers;
using Tlahui.Services.Caching.simple;

namespace Tlahui.Web.API
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

 

            #region SimpleInjector
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();


            // Register your types, for instance using the scoped lifestyle:
            container.Register<DbContext, Tlahui.Context.WebAPI.WebAPIContext>(Lifestyle.Scoped);
            container.Register<ICategoriesRepository, CategoriesRepository>(Lifestyle.Scoped);
            container.Register<ICachedResourceStatisticsRepository, CachedResourceStatisticsRepository>(Lifestyle.Scoped);
            container.Register<IDynamicFormsRepository, DynamicFormsRepository>(Lifestyle.Scoped);
            container.Register<IStoreService, StoreService>(Lifestyle.Scoped);
            container.Register<ISQLSearchProvider, MSSQLSearchProvider>(Lifestyle.Scoped);
            container.Register<ICacheService, InMemoryCache>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                    new SimpleInjectorWebApiDependencyResolver(container);

            #endregion




        }
    }
}
