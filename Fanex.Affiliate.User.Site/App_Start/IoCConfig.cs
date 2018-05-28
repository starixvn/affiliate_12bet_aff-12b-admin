using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Caching;

namespace Fanex.Affiliate.User.Site
{
    public static class IoCConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //Register repository
            RegisterRepository(builder);

            var asselmblyList = GetExecutingAssembly();
            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(asselmblyList);

            // OPTIONAL: Register model binders that require DI.
            // builder.RegisterModelBinders(asselmblyList);
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //builder.InjectActionInvoker();

            // Set the dependency resolver to be Autofac.

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static Assembly[] GetExecutingAssembly()
        {
            Assembly[] assemblies = null;
            try
            {
                assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();
            }
            catch
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(i => !i.IsDynamic).ToArray();
            }

            return assemblies = assemblies.Where(x => x.FullName.StartsWith("Fanex.Affiliate.User", StringComparison.InvariantCulture))
                .ToArray();
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterInstance(CacheService.CreateInstance(CacheType.MemoryCache)).As<ICache>();

            builder.RegisterType<AuthRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SessionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AppSetting>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CookieService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountOverviewRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MarketingRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MarketingService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatisticsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}