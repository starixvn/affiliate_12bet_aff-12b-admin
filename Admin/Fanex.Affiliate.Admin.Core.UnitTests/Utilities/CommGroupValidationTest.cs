using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Utilities
{
    public class CommGroupValidationTest
    {
        [Fact]
        public void HasPermissionAssignOnAff_WhenCallWithApprovedAff_ExcuteSucceed()
        {
            // Arrange
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), It.IsAny<string>())).Returns(new AffiliateBasicInfo { AffCode = "123456" });

            var helper = CreateCommGroupValidationHelper(affiliateService: affiliateService.Object);

            // Action
            var result = helper.HasPermissionAssignOnAff(123, 234);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasPermissionAssignOnAff_WhenCallWithPendingOrDisapprovedAff_ExcuteSucceed()
        {
            // Arrange
            var affiliateService = new Mock<IAffiliateService>();
            affiliateService.Setup(a => a.GetAffiliateBasicInfo(It.IsAny<int?>(), It.IsAny<string>())).Returns(new AffiliateBasicInfo());

            var authRepository = new Mock<IAuthRepository>();
            authRepository.Setup(a => a.GetPermissionList(It.IsAny<int>())).Returns(new List<UserPermission> { new UserPermission { FunctionName = Function.EditAffStatus, IsHost = true } });

            var helper = CreateCommGroupValidationHelper(authRepository.Object, affiliateService.Object);

            // Action
            var result = helper.HasPermissionAssignOnAff(123, 234);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateCommGroup_WhenCall_ExcuteSucceed()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateP2pDeductionList_WhenCallWithNull_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.P2pDeductionList = new List<P2PDeductionSettingParam>();

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateP2pDeductionList_WhenCallWithInvalidProductIds_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.P2pDeductionList = new List<P2PDeductionSettingParam>
            {
                new P2PDeductionSettingParam
                {
                    DeductedPercentage = 0.17m,
                    DeductionId = 10,
                    ProductIds = "1,2,4",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                }
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateP2pDeductionList_WhenCallWithEmptyProductIds_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.P2pDeductionList = new List<P2PDeductionSettingParam>
            {
                new P2PDeductionSettingParam
                {
                    DeductedPercentage = 0.17m,
                    DeductionId = 10,
                    ProductIds = "",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                },
                new P2PDeductionSettingParam
                {
                    DeductedPercentage = 0.17m,
                    DeductionId = 10,
                    ProductIds = "1",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                }
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidatePlatformFeeList_WhenCallWithNull_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.PlatformFeeList = new List<TierDeductionSettingParam>();

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidatePlatformFeeList_WhenCallWithInvalidProductIds_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.PlatformFeeList = new List<TierDeductionSettingParam>
            {
                new TierDeductionSettingParam
                {
                    DeductedPercentage = 0.17m,
                    DeductionId = 10,
                    ProductIds = "1,2,4",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                }
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("100_000_000_001")]
        public void ValidateCommGroup_ValidateDeductedFee_WhenCallWithInvalidAmount_ReturnError(string deductedAmount)
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();

            param.WithdrawalFee = new TierDeductionSettingParam
            {
                DeductedPercentage = 0.07m,
                DeductionId = 14,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = deductedAmount
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateDeductedFee_WhenCallWithHasException_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.DepositFee = new TierDeductionSettingParam
            {
                DeductedPercentage = 0.07m,
                DeductionId = 13,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = "abc"
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithNull_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.DepositFee = new TierDeductionSettingParam
            {
                DeductedPercentage = 0.07m,
                DeductionId = 13,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = "100"
            };
            param.TierSettingList = new List<TierSettingParam>();

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithMinAmountGreaterThanInfinityAmount_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.TierSettingList = new List<TierSettingParam>
            {
                new TierSettingParam
                {
                    Commission = 0.01m,
                    MinAmount = 101,
                    MinAp = 0,
                    TierId = 1
                },
                new TierSettingParam
                {
                    Commission = 0.10m,
                    MinAmount = 100_000_000_001,
                    MinAp = 101,
                    TierId = 2
                }
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithMinApGreaterThanInfinityQualifiedMember_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.TierSettingList = new List<TierSettingParam>
            {
                new TierSettingParam
                {
                    Commission = 0.01m,
                    MinAmount = 0,
                    MinAp = 101,
                    TierId = 1
                },
                new TierSettingParam
                {
                    Commission = 0.10m,
                    MinAmount = 101,
                    MinAp = 1_000_000_001,
                    TierId = 2
                }
            };

            var helper = CreateCommGroupValidationHelper();

            // Action
            var result = helper.ValidateCommGroup(param);

            // Assert
            Assert.False(result.IsSuccessful);
        }

        private static CommGroupValidation CreateCommGroupValidationHelper(IAuthRepository authRepository = null, IAffiliateService affiliateService = null)
        {
            var fakeIAuthRepository = authRepository ?? Mock.Of<IAuthRepository>();
            var fakeIAffiliateService = affiliateService ?? Mock.Of<IAffiliateService>();
            var productRepository = new Mock<IProductRepository>();
            var commGroupCommonService = new Mock<ICommGroupCommonService>();

            productRepository.Setup(p => p.GetProductList()).Returns(new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            });
            commGroupCommonService.Setup(c => c.GetCommGroupTypes()).Returns(new List<CommGroupType>
            {
                new CommGroupType {GroupTypeId = 1},
                new CommGroupType {GroupTypeId = 2}
            });

            return new CommGroupValidation(fakeIAuthRepository, fakeIAffiliateService, productRepository.Object, commGroupCommonService.Object);
        }
        private static CommGroupSettingParam CreateValidCommGroupSettingParam()
        {
            return new CommGroupSettingParam
            {
                GroupTypeId = 1,
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
                P2pDeductionList = new List<P2PDeductionSettingParam>
                {
                    new P2PDeductionSettingParam
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "",
                        Commission = 0.1m
                    }
                },
                PlatformFeeList = new List<TierDeductionSettingParam>
                {
                    new TierDeductionSettingParam
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "1,2,3",
                        IsDeductedPercent = true,
                        DeductedAmount = ""
                    }
                },
                TierSettingList = new List<TierSettingParam>
                {
                    new TierSettingParam
                    {
                        Commission = 0.01m,
                        MinAmount = 0,
                        MinAp = 0,
                        TierId = 1
                    },
                    new TierSettingParam
                    {
                        Commission = 0.10m,
                        MinAmount = 101,
                        MinAp = 101,
                        TierId = 2
                    }
                }
            };
        }
    }
}
