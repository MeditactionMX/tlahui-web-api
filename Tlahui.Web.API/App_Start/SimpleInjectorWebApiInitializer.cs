[assembly: WebActivator.PostApplicationStartMethod(typeof(Tlahui.Web.API.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace Tlahui.Web.API.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using Owin;
    using System.Data.Entity;
    using Tlahui.Repositories.Store;
    using Tlahui.Services.Store;
    using Tlahui.Repositories.Infrastructure.CachedResources;
    using Tlahui.Repositories.Infrastructure.DynamicForms;
    using Infrastructure.providers;
    using Tlahui.Services.Caching.simple;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            //container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }


        private static void InitializeContainer(Container container)
        {
            container.Register<DbContext, Tlahui.Context.WebAPI.WebAPIContext>(Lifestyle.Scoped);
            container.Register<ICategoriesRepository, CategoriesRepository>(Lifestyle.Scoped);
            container.Register<ICachedResourceStatisticsRepository, CachedResourceStatisticsRepository>(Lifestyle.Scoped);
            container.Register<IDynamicFormsRepository, DynamicFormsRepository>(Lifestyle.Scoped);
            container.Register<IStoreService, StoreService>(Lifestyle.Scoped);
            container.Register<ISQLSearchProvider, MSSQLSearchProvider>(Lifestyle.Scoped);
            container.Register<ICacheService, InMemoryCache>(Lifestyle.Scoped);


            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}