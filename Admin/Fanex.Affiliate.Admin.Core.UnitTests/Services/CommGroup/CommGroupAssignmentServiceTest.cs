using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.CommGroup
{
    public class CommGroupAssignmentServiceTest
    {
        private IDependencyResolver _originalResolver = null;
        private ILifetimeScopeProvider _scopeProvider = null;

        [Fact]
        public void GetNormalCommGroupList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new GetGroupListParam();
            var expectedResult = new List<SelectItem>
            {
                new SelectItem
                {
                    Id = "133",
                    Name = "New Test"
                }
            };
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.GetNormalCommGroupList(param)).Returns(expectedResult);
            var service = CreateCommGroupAssignmentService(commGroupRepository.Object);

            // Action
            var result = service.GetNormalCommGroupList(param);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void AssignGroup_WhenCallWithAssignComm_ExcuteCorrect()
        {
            // Arrange
            var param = new AssignGroupParam();
            var affInfo = new AffiliateBasicInfo { GroupId = 123 };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(param)).Returns(expectedResult);

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object);

            // Action
            var result = service.AssignGroup(param, affInfo, emailParam);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void AssignGroup_WhenCallWithApprovedComm_ExcuteUnsuccesful()
        {
            // Arrange
            var param = new AssignGroupParam();
            var affInfo = new AffiliateBasicInfo { AffType = 4 };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(param)).Returns(expectedResult);

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.ApproveAffiliate(It.IsAny<ApproveAffiliateParam>())).Returns(new ResultInfo { Code = 999 });

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, affiliateService.Object);

            // Action
            var result = service.AssignGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignGroup_WhenCallWithApprovedComm_ExcuteSuccessful()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.EuropeanAffSiteUrl).Returns("https://ha.nexdev.net:7613/12BetUserEuNew");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this._scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            this._originalResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(resolver);

            var param = new AssignGroupParam();
            var affInfo = new AffiliateBasicInfo { AffType = 4 };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(param)).Returns(expectedResult);

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.ApproveAffiliate(It.IsAny<ApproveAffiliateParam>())).Returns(expectedResult);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendApprovedEmail(It.IsAny<AffInformationEmail>()));

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, affiliateService.Object, emailService.Object);
            
            // Action
            var result = service.AssignGroup(param, affInfo, emailParam);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [MemberData("TestData", MemberType = typeof(TestCase))]
        public void AssignPrivateGroup_WhenCall_ExcuteWithIncorrectParam(List<TierSetting> tierSettingList)
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.TierSettingList = tierSettingList;
            var affInfo = new AffiliateBasicInfo();
            var emailParam = new AffInformationEmail();

            var service = CreateCommGroupAssignmentService();

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteCreateCommIsFailed()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            var affInfo = new AffiliateBasicInfo();
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo
            {
                Code = 999,
                Message = "Create Commission Group is failed!"
            };

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.CreateCommissionGroup(param)).Returns(expectedResult);

            var service = CreateCommGroupAssignmentService(commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteUpdateCommIsFailed()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.GroupId = 123;
            var affInfo = new AffiliateBasicInfo { GroupId = 123 };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo
            {
                Code = 999,
                Message = "Update Commission Group is failed!"
            };

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.UpdateCommissionGroup(param)).Returns(expectedResult);

            var service = CreateCommGroupAssignmentService(commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteWithAssignGroupFail()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.GroupId = 123;
            var affInfo = new AffiliateBasicInfo();
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.CreateCommissionGroup(param)).Returns(expectedResult);

            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(It.IsAny<AssignGroupParam>())).Returns(new ResultInfo { Code = 999 });

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteWithApproveFaild()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.GroupId = 123;
            var affInfo = new AffiliateBasicInfo();
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.CreateCommissionGroup(param)).Returns(expectedResult);

            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(It.IsAny<AssignGroupParam>())).Returns(expectedResult);

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.ApproveAffiliate(It.IsAny<ApproveAffiliateParam>())).Returns(new ResultInfo { Code = 999 });


            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, affiliateService.Object, commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteCorrectWithCreateAndApprovedPrivateComm()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.AsianAffSiteUrl).Returns("https://ha.nexdev.net:7613/12BetUserAsianNew");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this._scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            this._originalResolver = DependencyResolver.Current;
            DependencyResolver.SetResolver(resolver);

            var param = CreateValidCreateUpdateCommissionGroupParam();
            var affInfo = new AffiliateBasicInfo { AffType = 3, PreferredLanguage = "US", PlayerUsername = "12Bet" };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();

            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.CreateCommissionGroup(param)).Returns(expectedResult);

            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(It.IsAny<AssignGroupParam>())).Returns(expectedResult);

            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(aff => aff.ApproveAffiliate(It.IsAny<ApproveAffiliateParam>())).Returns(expectedResult);

            var emailService = new Mock<IEmailService>();
            emailService.Setup(e => e.SendApprovedEmail(It.IsAny<AffInformationEmail>()));

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, affiliateService.Object , commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteCorrectWithUpdatePrivateComm()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.GroupId = 123;
            var affInfo = new AffiliateBasicInfo { GroupId = 123 };
            var emailParam = new AffInformationEmail();

            var expectedResult = new ResultInfo();
            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.UpdateCommissionGroup(param)).Returns(expectedResult);

            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            commGroupRepository.Setup(comm => comm.AssignGroup(It.IsAny<AssignGroupParam>())).Returns(expectedResult);

            var service = CreateCommGroupAssignmentService(commGroupRepository.Object, commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.AssignPrivateGroup(param, affInfo, emailParam);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private static CommGroupAssignmentService CreateCommGroupAssignmentService(
            ICommissionGroupRepository commGroupRepository = null,
            IAffiliateService affiliateService = null,
            IEmailService emailService = null,
            ICommGroupCommonService commGroupCommonService = null)
        {

            var fakeCommGroupRepository = commGroupRepository ?? Mock.Of<ICommissionGroupRepository>();
            var fakeAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var fakeEmailService = emailService ?? Mock.Of<IEmailService>();
            var fakeCommGroupCommonService = commGroupCommonService ?? Mock.Of<ICommGroupCommonService>();

            return new CommGroupAssignmentService(fakeCommGroupRepository, fakeAffiliateService, fakeEmailService, fakeCommGroupCommonService);
        }

        private static CreateUpdateCommissionGroupParam CreateValidCreateUpdateCommissionGroupParam()
        {
            return new CreateUpdateCommissionGroupParam
            {
                BankingFee = new DeductionSetting
                {
                    DeductedPercentage = 0.02m,
                    DeductionId = 12,
                    ProductIds = "0"
                },
                ExchangeCommissionShare = new DeductionSetting
                {
                    DeductedPercentage = 0.10m,
                    DeductionId = 14,
                    ProductIds = "19"
                },
                ExchangeWinLossShare = new DeductionSetting
                {
                    DeductedPercentage = 0.15m,
                    DeductionId = 13,
                    ProductIds = "19"
                },
                OperationalFee = new DeductionSetting
                {
                    DeductedPercentage = 0.07m,
                    DeductionId = 11,
                    ProductIds = "0"
                },
                PlatformFeeList = new List<DeductionSetting>
                {
                    new DeductionSetting
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "1,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18"
                    }
                },
                TierSettingList = new List<TierSetting>
                {
                    new TierSetting
                    {
                        Commission = 0.01m,
                        MaxAmount = 100,
                        MaxQm = 100,
                        MinAmount = 0,
                        MinQm = 0,
                        TierId = 1
                    },
                    new TierSetting
                    {
                        Commission = 0.10m,
                        MaxAmount = 100000000000000,
                        MaxQm = 1000000000,
                        MinAmount = 101,
                        MinQm = 101,
                        TierId = 2
                    }
                }
            };
        }
    }
}