using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Core;

namespace Fanex.Affiliate.Admin.Core.Configurations
{
    public static class IoCConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //Register repository
            RegisterDependencies(builder);

            var asselmblyList = GetExecutingAssembly();
            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(asselmblyList);

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

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

            return assemblies.Where(x => x.FullName.StartsWith("Fanex.Affiliate.Admin", StringComparison.InvariantCulture)).ToArray();
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<SessionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}