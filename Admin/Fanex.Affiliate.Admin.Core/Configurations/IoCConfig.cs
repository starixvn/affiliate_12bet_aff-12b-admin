using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Repository.Customer;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Report;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.Payment;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.Utilities;

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
            builder.RegisterType<AppSetting>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SessionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionGroupRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionGroupService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MailProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LanguageRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AuthRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateReportRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateReportService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatementHistoryRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatementHistoryService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PlayerDetailRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LandingPageRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LandingPageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CustomerStatisticsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMemberRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMemberService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateInfoRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NoteRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<TrackingStatisticsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ReferrersRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LandingPageStatisticsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ViewLogRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AdjustmentRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PaymentRequestRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PaymentRequestService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CustomerLeftPanelRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserAccountRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserAccountService>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}