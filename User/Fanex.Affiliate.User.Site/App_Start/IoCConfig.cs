using System;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.MemberInfoWebService;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Repository.BetList;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Caching;
using ONEbook.UM.Services.Impl;

namespace Fanex.Affiliate.User.Site
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

            return assemblies.Where(x => x.FullName.StartsWith("Fanex.Affiliate.User", StringComparison.InvariantCulture))
                .ToArray();
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<MaintenanceService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterInstance(CacheService.CreateInstance(CacheType.MemoryCache)).As<ICache>();

            builder.RegisterType<AuthRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SessionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AppSetting>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MarketingRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<OldMarketingRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MarketingService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<OldMarketingService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatisticsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CryptographyService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NewsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NewsService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MailProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatementHistoryRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<BetListRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PaymentRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PaymentService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemberRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmailHistoryRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ContactService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ServiceSoapClient>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemberInfoServiceWrapper>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemberTransactionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionReportRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CommissionReportService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CaptchaService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<TwoFactorAuthService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<VerificationService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<WrapperUtility>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<VerificationRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SecurityQuestionRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SecurityQuestionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountOverviewRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<QrCodeUrlGeneratorService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PngImageHandler>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}