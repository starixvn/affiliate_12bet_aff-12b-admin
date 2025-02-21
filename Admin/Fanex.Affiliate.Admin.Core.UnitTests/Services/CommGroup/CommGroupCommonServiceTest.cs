using Fanex.Affiliate.Admin.Core.Commands.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.CommGroup
{
    public class CommGroupCommonServiceTest
    {
        [Fact]
        public void CreateCommissionGroup_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<InsertCommGroupCommand>(), It.IsAny<string>()));

            var service = CreateCommGroupCommonService(dynamicRepository.Object);

            // Action
            var result = service.CreateCommissionGroup(param);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<InsertCommGroupCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParamHasKillRate();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<UpdateCommGroupCommand>(), It.IsAny<string>()));

            var service = CreateCommGroupCommonService(dynamicRepository.Object);

            // Action
            var result = service.UpdateCommissionGroup(param);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<UpdateCommGroupCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetDeductionList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            var expectedResult = new List<DeductionItem>
                    {
                        new DeductionItem
                        {
                            DeductedPercentage= 0, DeductionId= 14, DeductionName = "ExchangeCommissionShare"
                        }
                    };
            dynamicRepository.Setup(comm => comm.Fetch<DeductionItem>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(expectedResult);
            var service = CreateCommGroupCommonService(dynamicRepository.Object);

            // Action
            var result = service.GetDeductionList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<DeductionItem>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetCommGroupTypes_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            var expectedResult = new List<CommGroupType>
                    {
                        new CommGroupType()
                    };
            dynamicRepository.Setup(comm => comm.Fetch<CommGroupType>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(expectedResult);
            var service = CreateCommGroupCommonService(dynamicRepository.Object);

            // Action
            var result = service.GetCommGroupTypes();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<CommGroupType>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetNormalCommGroupList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new GetGroupListParam();
            var expectedResult = new List<NormalCommGroup>
            {
                new NormalCommGroup
                {
                    GroupId = 123,
                    GroupName = "New Test",
                    GroupTypeId = 1
                }
            };
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Fetch<NormalCommGroup>(It.IsAny<GetNormalCommGroupCriteria>(), It.IsAny<string>())).Returns(expectedResult);
            var service = CreateCommGroupCommonService(dynamicRepository.Object);

            // Action
            var result = service.GetNormalCommGroupList(param);

            // Assert
            dynamicRepository.Verify(d => d.Fetch<NormalCommGroup>(It.Is<GetNormalCommGroupCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        private static CommGroupCommonService CreateCommGroupCommonService(
            IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new CommGroupCommonService(fakedynamicRepository);
        }

        private static CreateUpdateCommissionGroupParam CreateValidCreateUpdateCommissionGroupParam()
        {
            return new CreateUpdateCommissionGroupParam
            {
                DepositFee = new TierDeductionSettingParam
                {
                    DeductedPercentage = 0.02m,
                    DeductionId = 13,
                    ProductIds = "0",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                },
                WithdrawalFee = new TierDeductionSettingParam
                {
                    DeductedPercentage = 0.02m,
                    DeductionId = 14,
                    ProductIds = "0",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                },
                PlatformFeeList = new List<TierDeductionSettingParam>
                {
                    new TierDeductionSettingParam
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "5,6,7,8,9,10,11,12,13,14,15,16,17,18",
                        IsDeductedPercent = true,
                        DeductedAmount = ""
                    }
                },
                P2pDeductionList = new List<P2PDeductionSettingParam>
                {
                    new P2PDeductionSettingParam
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "1,3,4",
                        Commission = 0.1m
                    }
                },
                TierSettingList = new List<TierSettingParam>
                {
                    new TierSettingParam
                    {
                        Commission = 0.01m,
                        MinAmount = 0,
                        MinQm = 0,
                        TierId = 1
                    },
                    new TierSettingParam
                    {
                        Commission = 0.10m,
                        MinAmount = 101,
                        MinQm = 101,
                        TierId = 2
                    }
                }
            };
        }

        private static CreateUpdateCommissionGroupParam CreateValidCreateUpdateCommissionGroupParamHasKillRate()
        {
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.KillRateFeeList = new List<TierDeductionSettingParam> { new TierDeductionSettingParam() };

            return param;
        }
    }
}
