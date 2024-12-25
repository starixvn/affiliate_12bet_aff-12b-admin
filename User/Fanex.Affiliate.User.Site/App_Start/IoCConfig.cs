using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.User.Core.Apis;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Infrastructure;
using Fanex.Affiliate.User.Core.MemberInfoWebService;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Caching;
using Fanex.Caching.Memory;
using Fanex.Data.Repository;
using ONEbook.UM.Configurations;
using ONEbook.UM.Helpers;
using ONEbook.UM.NETFramework;
using ONEbook.UM.Transport;
using Refit;

namespace Fanex.Affiliate.User.Site
{
    public static class IoCConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //Register repository
            RegisterDependencies(builder);

            builder.RegisterMemberSiteApi();

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
            builder.RegisterInstance(new UMClientConfiguration().Initialize());
            builder.RegisterInstance<ICache>(new MemoryCache(new MemoryCacheOptions()));
            builder.RegisterInstance<ICacheService>(new CacheService());
            builder.RegisterInstance<IDynamicRepository>(new DynamicRepository());

            builder.RegisterType<UMCacheService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<IPHelperImpl>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<UMRestClient>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<HttpContextHelper>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<SessionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AppSetting>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<SystemService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MarketingService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<CryptographyService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NewsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NewsService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MailProcessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatementHistoryService>().AsImplementedInterfaces().InstancePerLifetimeScope();
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
            builder.RegisterType<SecurityQuestionService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<AccountOverviewService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<StatisticsService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UnderMaintenanceService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<QrCodeGeneratorService>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ObjectDBBuilder>().AsImplementedInterfaces().UsingConstructor().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<NewsLetterRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<NewsLetterService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<OfferService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<OfferRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<HttpClientUtility>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<FileSystem>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        private static void RegisterMemberSiteApi(this ContainerBuilder builder)
        {
            var appSetting = new AppSetting();
            var cacheService = new MemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
            var handler = new AuthenticatedHttpClientHandler(appSetting, cacheService);

            builder.RegisterInstance(RestService.For<ILicenseeApi>(
                new HttpClient(handler)
                {
                    BaseAddress = new Uri(appSetting.MemberSiteApiUrl)
                }));
        }
    }
}