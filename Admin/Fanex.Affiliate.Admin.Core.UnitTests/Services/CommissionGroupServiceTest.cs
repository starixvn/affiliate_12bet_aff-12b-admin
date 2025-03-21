using Fanex.Affiliate.Admin.Core.Commands.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class CommissionGroupServiceTest
    {
        [Fact]
        public void GetCommissionGroupList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commGroupList = new List<DTO.CommissionGroup.CommGroup>
            {
                new DTO.CommissionGroup.CommGroup
                {
                    RowIdx = 1,
                    GroupId = 101,
                    GroupName = "Default",
                    CreatedDate = new DateTime(2018, 7, 11),
                    Creator = "Admin",
                    Description = "Description",
                    NoAffs = 2
                }
            };
            var tierDeductionSettingList = new List<DeductionSetting>
            {
                new DeductionSetting
                {
                    GroupId = 101,
                    DeductionId = 1,
                    DeductionName = "DeductionName",
                    ProductIds = "1,2,3",
                    DeductedPercentage = 0.7m
                },
                new DeductionSetting
                {
                    GroupId = 101,
                    DeductionId = 13,
                    DeductionName = "ExchangeGgrShare",
                    ProductIds = "19",
                    DeductedPercentage = 0.0m
                },
                new DeductionSetting
                {
                    GroupId = 101,
                    DeductionId = 11,
                    DeductionName = "BankingFee",
                    ProductIds = "0",
                    DeductedPercentage = 0.07m
                }
            };
            var tierSettingList = new List<TierSetting>
            {
                new TierSetting
                {
                    GroupId = 101,
                    TierId = 1,
                    MinAmount = 0m,
                    MinQm = 0,
                    Commission = 0.2m
                }
            };
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.FetchMultiple<DTO.CommissionGroup.CommGroup, TierSetting, DeductionSetting>(It.IsAny<GetCommissionGroupCriteria>(), It.IsAny<string>())).Returns((commGroupList, tierSettingList, tierDeductionSettingList));
            var service = CreateCommissionGroupService(dynamicRepository.Object);

            // Action
            var commissionGroupList = service.GetCommissionGroupList(new GetCommissionGroupParam());

            // Assert
            Assert.Single(commissionGroupList);
            dynamicRepository.Verify(d => d.FetchMultiple<DTO.CommissionGroup.CommGroup, TierSetting, DeductionSetting>(It.Is<GetCommissionGroupCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
        }

        [Fact]
        public static void CreateCommissionGroup_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new CreateUpdateCommissionGroupParam();
            var expectedResult = new ResultInfo();
            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.CreateCommissionGroup(param)).Returns(expectedResult);
            var service = CreateCommissionGroupService(commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.CreateCommissionGroup(param);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public static void UpdateCommissionGroup_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new CreateUpdateCommissionGroupParam();
            var expectedResult = new ResultInfo();
            var commGroupCommonService = new Mock<ICommGroupCommonService>();
            commGroupCommonService.Setup(comm => comm.UpdateCommissionGroup(param)).Returns(expectedResult);
            var service = CreateCommissionGroupService(commGroupCommonService: commGroupCommonService.Object);

            // Action
            var result = service.UpdateCommissionGroup(param);

            // Assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public static void DeleteCommissionGroup_WhenCall_Successfully()
        {
            // Arrange
            var param = new DeleteCommissionGroupParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<DeleteCommGroupCommand>(), It.IsAny<string>()));
            var service = CreateCommissionGroupService(dynamicRepository.Object);

            // Action
            var result = service.DeleteCommissionGroup(param);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<DeleteCommGroupCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        private static CommissionGroupService CreateCommissionGroupService(
            IDynamicRepository dynamicRepository = null,
            ICommGroupCommonService commGroupCommonService = null)
        {
            var fakeCommissionGroupRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();
            var fakeCommGroupCommonService = commGroupCommonService ?? Mock.Of<ICommGroupCommonService>();

            return new CommissionGroupService(fakeCommissionGroupRepository, fakeCommGroupCommonService);
        }
    }
}
