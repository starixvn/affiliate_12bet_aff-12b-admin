using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Operation;
using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
using Fanex.Affiliate.Admin.Core.Utilities.Operation;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Utilities
{
    public class CommGroupValidationTest
    {
        [Fact]
        public void ValidateCommGroup_WhenCall_ExecuteSucceed()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.True(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidatePlatformFeeList_WhenCallWithNull_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.PlatformFeeSetting = new List<RevenueDeductionSetting>();

            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidatePlatformFeeList_WhenCallWithInvalidProductIds_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.PlatformFeeSetting = new List<RevenueDeductionSetting>
            {
                new RevenueDeductionSetting
                {
                    DeductedPercentage = 0.17m,
                    DeductionId = 10,
                    ProductIds = "1,2,4",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                }
            };
            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.False(result.isValid);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("100_000_000_001")]
        public void ValidateCommGroup_ValidateDeductedFee_WhenCallWithInvalidAmount_ReturnError(string deductedAmount)
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();

            param.WithdrawalFeeSetting = new RevenueDeductionSetting
            {
                DeductedPercentage = 0.07m,
                DeductionId = 14,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = deductedAmount
            };

            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidateDeductedFee_WhenCallWithHasException_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.DepositFeeSetting = new RevenueDeductionSetting
            {
                DeductedPercentage = 0.07m,
                DeductionId = 13,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = "abc"
            };

            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithNull_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.DepositFeeSetting = new RevenueDeductionSetting
            {
                DeductedPercentage = 0.07m,
                DeductionId = 13,
                ProductIds = "0",
                IsDeductedPercent = false,
                DeductedAmount = "100"
            };
            param.RevenueTierSetting = new List<RevenueTierSetting>();

            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.True(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithMinAmountGreaterThanInfinityAmount_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.RevenueTierSetting = new List<RevenueTierSetting>
            {
                new RevenueTierSetting
                {
                    Commission = 0.01m,
                    MinAmount = 101,
                    MinQm = 0,
                    TierId = 1
                },
                new RevenueTierSetting
                {
                    Commission = 0.10m,
                    MinAmount = 100_000_000_001,
                    MinQm = 101,
                    TierId = 2
                }
            };
            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);
            
            // Assert
            Assert.False(result.isValid);
        }

        [Fact]
        public void ValidateCommGroup_ValidateTierSettingList_WhenCallWithMinAmGreaterThanInfinityQualifiedMember_ReturnError()
        {
            // Arrange
            var param = CreateValidCommGroupSettingParam();
            param.RevenueTierSetting = new List<RevenueTierSetting>
            {
                new RevenueTierSetting
                {
                    Commission = 0.01m,
                    MinAmount = 0,
                    MinQm = 101,
                    TierId = 1
                },
                new RevenueTierSetting
                {
                    Commission = 0.10m,
                    MinAmount = 101,
                    MinQm = 1_000_000_001,
                    TierId = 2
                }
            };
            var products = CreateProductList();

            // Action
            var result = RevenueShareValidator.ValidateRevenueShare(param, products);

            // Assert
            Assert.False(result.isValid);
        }

        private static IEnumerable<Product> CreateProductList()
        {
            return new List<Product>
            {
                new Product { ProductId = 1 },
                new Product { ProductId = 2 },
                new Product { ProductId = 3 }
            };
        }

        private static DealSettingParam CreateValidCommGroupSettingParam()
        {
            return new DealSettingParam
            {
                DepositFeeSetting = new RevenueDeductionSetting
                {
                    DeductedPercentage = 0.02m,
                    DeductionId = 13,
                    ProductIds = "0",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                },
                WithdrawalFeeSetting = new RevenueDeductionSetting
                {
                    DeductedPercentage = 0.02m,
                    DeductionId = 14,
                    ProductIds = "0",
                    IsDeductedPercent = true,
                    DeductedAmount = ""
                },
                PlatformFeeSetting = new List<RevenueDeductionSetting>
                {
                    new RevenueDeductionSetting
                    {
                        DeductedPercentage = 0.17m,
                        DeductionId = 10,
                        ProductIds = "1,2,3",
                        IsDeductedPercent = true,
                        DeductedAmount = ""
                    }
                },
                RevenueTierSetting = new List<RevenueTierSetting>
                {
                    new RevenueTierSetting
                    {
                        Commission = 0.01m,
                        MinAmount = 0,
                        MinQm = 0,
                        TierId = 1
                    },
                    new RevenueTierSetting
                    {
                        Commission = 0.10m,
                        MinAmount = 101,
                        MinQm = 101,
                        TierId = 2
                    }
                }
            };
        }
    }
}
