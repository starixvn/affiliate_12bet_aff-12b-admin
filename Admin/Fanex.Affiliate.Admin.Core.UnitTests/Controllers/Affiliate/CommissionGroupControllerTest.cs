using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels;
using Moq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class CommissionGroupControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product> { new Product { ProductId = 123, ProductName = "abc" } });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetDeductionList()).Returns(new List<Deduction> { new Deduction { DeductionId = 12, DeductionName = "BankingFee", DeductedPercentage = 0.05m } });

            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object, systemServive: systemServive.Object);

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as CommissionGroupViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/Index.cshtml", viewName);
            Assert.Single(model.ProductList);
            Assert.Single(model.DeductionList);
        }

        [Fact]
        public void FetchInitState_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product> { new Product { ProductId = 123, ProductName = "abc" } });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetDeductionList()).Returns(new List<Deduction> { new Deduction { DeductionId = 12, DeductionName = "BankingFee", DeductedPercentage = 0.05m } });

            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object, systemServive: systemServive.Object);

            // Action
            var viewResult = controller.FetchInitState() as JsonNetResult;
            var model = viewResult.Data as CommissionGroupViewModel;

            // Assert
            Assert.Single(model.ProductList);
            Assert.Single(model.DeductionList);
        }

        [Fact]
        public void GetCommissionGroupList_GetData_ReturnCorrectData()
        {
            // Arrange
            var param = new GetCommissionGroupParam { GroupName = "abc", PageIndex = 1, PageSize = 20 };
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(param)).Returns(new List<CommissionGroupSetting> { new CommissionGroupSetting { GroupName = "abc" } }).Callback<GetCommissionGroupParam>(p => param.Nrows = 1);
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.GetCommissionGroupList(param) as JsonNetResult;
            var model = result.Data as GetCommissionGroupViewModel;

            // Assert
            Assert.Equal(1, model.TotalRows);
            Assert.Single(model.CommissionSettingList);
        }

        [Fact]
        public void GetCommissionGroup_GetData_ReturnCorrectData()
        {
            // Arrange
            var param = new GetCommissionGroupParam { GroupId = 123 };
            GetCommissionGroupParam passedParam = null;
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(param))
                .Returns(new List<CommissionGroupSetting> { new CommissionGroupSetting { GroupId = 123 } })
                .Callback<GetCommissionGroupParam>(p => passedParam = p);
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var model = result.Data as CommissionGroupSetting;

            // Assert
            Assert.Equal(param.GroupId, model.GroupId);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetCommissionGroup_GetData_ReturnNullData()
        {
            // Arrange
            var param = new GetCommissionGroupParam { GroupId = 123 };
            GetCommissionGroupParam passedParam = null;
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetCommissionGroupList(param)).Returns(new List<CommissionGroupSetting>()).Callback<GetCommissionGroupParam>(p => passedParam = p);
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(CommGroupErrorMessage.InvalidGroupId, resultInfo.Message);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetCommissionGroup_GetDataWithInvalidParam_ReturnError()
        {
            // Arrange
            var param = new GetCommissionGroupParam();
            var controller = CreateCommissionGroupController();
            var expectedResult = ResultInfo.GenerateErr(CommGroupErrorMessage.InvalidGroupId);

            // Action
            var result = controller.GetCommissionGroup(param) as JsonNetResult;
            var model = result.Data as ResultInfo;

            // Assert
            Assert.Equal(expectedResult.Code, model.Code);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithInvalidModelState_ExcuteIncorrect()
        {
            // Arrange
            const string ErrorMessage1 = "ErrorMessage1";
            var param = new CommissionGroupSetting();
            var controller = CreateCommissionGroupController();
            controller.ModelState.AddModelError("Error01", ErrorMessage1);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(ErrorMessage1, resultInfo.Message);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithEmptyGroupName_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.GroupName = string.Empty;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithGroupNameMoreThan100Characters_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.GroupName = "Create Commission Group When Call With Group Name More Than 100 Characters, Create Commission Group When Call With Group Name More Than 100 Characters";
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Theory]
        [MemberData(nameof(TestCase.TestData), MemberType = typeof(TestCase))]
        public void CreateCommissionGroup_WhenCallWithInvalidPlatformFee_ExcuteIncorrect(List<DeductionSetting> platformFeeList)
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.GroupName = "abc";
            param.PlatformFeeList = platformFeeList;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });

            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithInvalidGroupNameAndPlatformFee_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.GroupName = string.Empty;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.CreateCommissionGroup(It.IsAny<CreateUpdateCommissionGroupParam>())).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object, commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void CreateCommissionGroup_WhenCallWithValidParamAndDescriptionNull_ExcuteCorrectly()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.Description = null;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.CreateCommissionGroup(It.IsAny<CreateUpdateCommissionGroupParam>())).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object, commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.CreateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithInvalidModelState_ExcuteIncorrect()
        {
            // Arrange
            const string ErrorMessage1 = "ErrorMessage1";
            var param = CreateUpdateCommissionGroupSetting();
            var controller = CreateCommissionGroupController();
            controller.ModelState.AddModelError("Error01", ErrorMessage1);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(ErrorMessage1, resultInfo.Message);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithInvalidGroupId_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            var controller = CreateCommissionGroupController();

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithEmptyGroupName_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            param.GroupName = string.Empty;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithGroupNameMoreThan100Characters_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            param.GroupName = "Update Commission Group When Call With Group Name More Than 100 Characters, Update Commission Group When Call With Group Name More Than 100 Characters";

            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Theory]
        [MemberData(nameof(TestCase.TestData), MemberType = typeof(TestCase))]
        public void UpdateCommissionGroup_WhenCallWithInvalidPlatformFee_ExcuteIncorrect(List<DeductionSetting> platformFeeList)
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            param.GroupName = "abc";
            param.PlatformFeeList = platformFeeList;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });

            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithInvalidGroupNameAndPlatformFee_ExcuteIncorrect()
        {
            // Arrange
            var param = CreateCommissionGroupSetting();
            param.GroupName = string.Empty;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.UpdateCommissionGroup(It.IsAny<CreateUpdateCommissionGroupParam>())).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object, commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCallWithValidParamAndDescriptionNull_ExcuteCorrectly()
        {
            // Arrange
            var param = CreateUpdateCommissionGroupSetting();
            param.Description = null;
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });

            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.UpdateCommissionGroup(It.IsAny<CreateUpdateCommissionGroupParam>())).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object, commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.UpdateCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void DeleteCommissionGroup_WhenCallWithInvalidModelState_ExcuteIncorrect()
        {
            // Arrange
            const string ErrorMessage1 = "ErrorMessage1";
            var param = new DeleteCommissionGroupParam();
            var controller = CreateCommissionGroupController();
            controller.ModelState.AddModelError("Error01", ErrorMessage1);

            // Action
            var result = controller.DeleteCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(ErrorMessage1, resultInfo.Message);
        }

        [Fact]
        public void DeleteCommissionGroup_WhenCallWithValidParam_ExcuteCorrectly()
        {
            // Arrange
            var param = new DeleteCommissionGroupParam();
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.DeleteCommissionGroup(param)).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object);

            // Action
            var result = controller.DeleteCommissionGroup(param) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCallWithInvalidAffId_ReturnViewWithEmptyModel()
        {
            // Arrange
            var controller = CreateCommissionGroupController();

            // Action
            var viewResult = controller.AssignPrivateGroup(0) as ViewResult;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/AssignPrivateGroup.cshtml", viewResult.ViewName);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ReturnView()
        {
            // Arrange
            int affId = 1271808;
            var productList = new List<Product> { new Product { ProductId = 123, ProductName = "Casino" } };
            var deductionList = new List<Deduction> { new Deduction { DeductionId = 12, DeductionName = "BankingFee", DeductedPercentage = 0.05m } };
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(productList);
            var commissionGroupService = new Mock<ICommissionGroupService>();
            commissionGroupService.Setup(c => c.GetDeductionList()).Returns(deductionList);

            var controller = CreateCommissionGroupController(commissionGroupService: commissionGroupService.Object, systemServive: systemServive.Object);
            var expectModel = new AssignPrivateGroupViewModel
            {
                ProductList = productList,
                DeductionList = deductionList
            };
            // Action
            var viewResult = controller.AssignPrivateGroup(affId) as ViewResult;
            var model = viewResult.Model as AssignPrivateGroupViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/AssignPrivateGroup.cshtml", viewResult.ViewName);
            Assert.Equal(expectModel.ProductList, model.ProductList);
            Assert.Equal(expectModel.DeductionList, model.DeductionList);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCallPostWithInvalidParam_ReturnError()
        {
            // Arrange
            var commGroup = new CommissionGroupSetting();
            var affInfo = new AffiliateBasicInfo();
            const string ErrorMessage1 = "ErrorMessage1";
            var controller = CreateCommissionGroupController();
            controller.ModelState.AddModelError("Error01", ErrorMessage1);
            var expectResult = ResultInfo.GenerateErr(ErrorMessage1);
            // Action
            var jsonNetResult = controller.AssignPrivateGroup(commGroup, affInfo) as JsonNetResult;
            var resultInfo = jsonNetResult.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
            Assert.Equal(expectResult.Code, resultInfo.Code);
            Assert.Equal(expectResult.Message, resultInfo.Message);
        }

        [Theory]
        [MemberData(nameof(TestCase.TestData), MemberType = typeof(TestCase))]
        public void AssignPrivateGroup_WhenCallWithInvalidPlatformFee_ExcuteIncorrect(List<DeductionSetting> platformFeeList)
        {
            // Arrange
            var commGroup = CreateCommissionGroupSetting();
            commGroup.PlatformFeeList = platformFeeList;
            var affInfo = new AffiliateBasicInfo { AffId = 1271808 };
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });

            var controller = CreateCommissionGroupController(systemServive: systemServive.Object);

            // Action
            var result = controller.AssignPrivateGroup(commGroup, affInfo) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.False(resultInfo.IsSuccessful);
        }

        [Fact]
        public void AssignPrivateGroup_WhenCall_ExcuteCorrectly()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("LOCAL");
            appSetting.Setup(a => a.IsProduction).Returns(false);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            var _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var commGroup = CreateCommissionGroupSetting();
            var affInfo = new AffiliateBasicInfo { AffId = 1271808 };
            var systemServive = new Mock<ISystemService>();
            systemServive.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 3 },
                new Product { ProductId = 4 }
            });
            var commGroupAssignmentService = new Mock<ICommGroupAssignmentService>();
            commGroupAssignmentService.Setup(c => c.AssignPrivateGroup(It.IsAny<CreateUpdateCommissionGroupParam>(), affInfo, It.IsAny<AffInformationEmail>())).Returns(new ResultInfo());
            var controller = CreateCommissionGroupController(systemServive: systemServive.Object, commGroupAssignmentService: commGroupAssignmentService.Object);

            // Action
            var result = controller.AssignPrivateGroup(commGroup, affInfo) as JsonNetResult;
            var resultInfo = result.Data as ResultInfo;

            // Assert
            Assert.True(resultInfo.IsSuccessful);
        }

        private static CommissionGroupController CreateCommissionGroupController(
            ISessionService sessionService = null,
            ICommissionGroupService commissionGroupService = null,
            ISystemService systemServive = null,
            ICommGroupAssignmentService commGroupAssignmentService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeCommissionGroupService = commissionGroupService ?? Mock.Of<ICommissionGroupService>();
            var fakeCommonService = systemServive ?? Mock.Of<ISystemService>();
            var fakecommGroupAssignmentService = commGroupAssignmentService ?? Mock.Of<ICommGroupAssignmentService>();
            var controller = new CommissionGroupController(fakeSessionService, fakeCommissionGroupService, fakeCommonService, fakecommGroupAssignmentService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }

        private static CommissionGroupSetting CreateCommissionGroupSetting()
        {
            return new CommissionGroupSetting
            {
                BankingFee = new DeductionSetting { DeductionId = 12, ProductIds = "0", DeductedPercentage = 0.02m },
                Description = "Test",
                GroupId = 0,
                GroupName = "Test",
                OperationalFee = new DeductionSetting { DeductionId = 11, ProductIds = "0", DeductedPercentage = 0.07m },
                PlatformFeeList = new List<DeductionSetting>
                {
                    new DeductionSetting { DeductionId = 10, ProductIds = "1,3,4", DeductedPercentage = 0.17m }
                },
                QmTurnover = 100,
                TierSettingList = new List<TierSetting>
                {
                    new TierSetting { Commission = 0.05m, MaxAmount = 100, MaxQm = 100, MinAmount = 0, MinQm = 0, TierId = 1 },
                    new TierSetting { Commission = 0.15m, MaxAmount = 100000000000000, MaxQm = 1000000000, MinAmount = 101, MinQm = 101, TierId = 2 }
                }
            };
        }

        private static CommissionGroupSetting CreateUpdateCommissionGroupSetting()
        {
            var commissionGroupSetting = CreateCommissionGroupSetting();
            commissionGroupSetting.GroupId = 123;

            return commissionGroupSetting;
        }
    }

    public static class TestCase
    {
        private static readonly List<object[]> _data =
            new List<object[]>
            {
                new object[] {
                    null
                },
                new object[] {
                    new List<DeductionSetting>
                    {
                    }
                },
                new object[] {
                    new List<DeductionSetting>
                    {
                        new DeductionSetting
                        {
                            ProductIds = string.Empty
                        }
                    }
                },
                new object[] {
                    new List<DeductionSetting>
                    {
                        new DeductionSetting
                        {
                            ProductIds = "1,2"
                        }
                    }
                },
                new object[] {
                    new List<DeductionSetting>
                    {
                        new DeductionSetting
                        {
                            ProductIds = "1,,3"
                        }
                    }
                },
                new object[] {
                    new List<DeductionSetting>
                    {
                        new DeductionSetting
                        {
                            ProductIds = "1,2,4"
                        }
                    }
                }
            };
        public static IEnumerable<object[]> TestData
        {
            get { return _data; }
        }
    }
}
