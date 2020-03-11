using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
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
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            var param = CreateValidCreateUpdateCommissionGroupParam();
            commGroupRepository.Setup(comm => comm.CreateCommissionGroup(param)).Returns(new ResultInfo());
            var service = CreateCommGroupCommonService(commGroupRepository.Object);

            // Action
            var result = service.CreateCommissionGroup(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [MemberData(nameof(TestCase.TestData), MemberType = typeof(TestCase))]
        public void CreateCommissionGroup_WhenCall_ExcuteUncorrectWithInvalidTierSettingList(List<TierSetting> tierSettingList)
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.TierSettingList = tierSettingList;
            var service = CreateCommGroupCommonService();

            // Action
            var result = service.CreateCommissionGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void UpdateCommissionGroup_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            var param = CreateValidCreateUpdateCommissionGroupParam();
            commGroupRepository.Setup(comm => comm.UpdateCommissionGroup(param)).Returns(new ResultInfo());
            var service = CreateCommGroupCommonService(commGroupRepository.Object);

            // Action
            var result = service.UpdateCommissionGroup(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [MemberData(nameof(TestCase.TestData), MemberType = typeof(TestCase))]
        public void UpdateCommissionGroup_WhenCall_ExcuteUncorrectWithInvalidTierSettingList(List<TierSetting> tierSettingList)
        {
            // Arrange
            var param = CreateValidCreateUpdateCommissionGroupParam();
            param.TierSettingList = tierSettingList;
            var service = CreateCommGroupCommonService();

            // Action
            var result = service.UpdateCommissionGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void GetDeductionList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var commGroupRepository = new Mock<ICommissionGroupRepository>();
            var expectedResult = new List<Deduction>
                    {
                        new Deduction
                        {
                            DeductedPercentage= 0, DeductionId= 14, DeductionName = "ExchangeCommissionShare"
                        }
                    };
            commGroupRepository.Setup(comm => comm.GetDeductionList()).Returns(expectedResult);
            var service = CreateCommGroupCommonService(commGroupRepository.Object);

            // Action
            var result = service.GetDeductionList();

            // Assert
            Assert.Single(result);
        }

        private static CommGroupCommonService CreateCommGroupCommonService(ICommissionGroupRepository commGroupRepository = null)
        {
            var fakeCommGroupRepository = commGroupRepository ?? Mock.Of<ICommissionGroupRepository>();

            return new CommGroupCommonService(fakeCommGroupRepository);
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

    public static class TestCase
    {
        private static readonly List<object[]> _data =
            new List<object[]>
            {
                new object[] {
                    null
                },
                new object[] {
                    new List<TierSetting>
                    {
                    }
                },
                new object[] {
                    new List<TierSetting>
                    {
                        new TierSetting
                        {
                            Commission = 0,
                            MaxAmount = 100,
                            MaxQm = 100,
                            MinAmount = 0,
                            MinQm = 0,
                            TierId = 1
                        }
                    }
                },
                new object[] {
                    new List<TierSetting>
                    {
                        new TierSetting
                        {
                            Commission = 0,
                            MaxAmount = 0,
                            MaxQm = 100,
                            MinAmount = 100,
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
                },
                new object[] {
                    new List<TierSetting>
                    {
                        new TierSetting
                        {
                            Commission = 0,
                            MaxAmount = 100,
                            MaxQm = 0,
                            MinAmount = 0,
                            MinQm = 100,
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
                },
            };
        public static IEnumerable<object[]> TestData
        {
            get { return _data; }
        }
    }
}
