using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Operation;
using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.Operation
{
    public static class RevenueShareValidator
    {
        public static (bool isValid, List<string> messageList) ValidateRevenueShare(DealSettingParam dealInfo, IEnumerable<Product> products)
        {
            var messageList = new List<string>();

            if (dealInfo.QmTurnover < 0 || dealInfo.QmTurnover > Money.AllowedMaxValue)
            {
                messageList.Add("QpTurnover not valid!");
            }

            var validateAllFees = ValidateAllFees(dealInfo, products);

            if (!validateAllFees.isValid)
            {
                messageList.AddRange(validateAllFees.messageList);
            }

            var validateTierSettingList = ValidateTierSettingList(dealInfo.RevenueTierSetting);

            if (!validateTierSettingList.isValid)
            {
                messageList.Add(validateTierSettingList.message);
            }

            var isValid = messageList.Count == 0;

            return (isValid, messageList);
        }

        private static (bool isValid, List<string> messageList) ValidateAllFees(DealSettingParam dealInfo, IEnumerable<Product> products)
        {
            var messageList = new List<string>();
            var productIdOfDB = products.Select(p => p.ProductId.ToString());

            var validatePlatformFeeList = ValidateDeductionList(dealInfo.PlatformFeeSetting, productIdOfDB, CommGroupErrorMessage.InvalidPlatformFee);
            if (!validatePlatformFeeList.isValid)
            {
                messageList.Add(validatePlatformFeeList.message);
            }

            var validateDepositFee = ValidateFixedAmountDeductedFee(dealInfo.DepositFeeSetting, CommGroupErrorMessage.InvalidDepositFee);
            if (!validateDepositFee.isValid)
            {
                messageList.Add(validateDepositFee.message);
            }

            var validateWithdrawalFee = ValidateFixedAmountDeductedFee(dealInfo.WithdrawalFeeSetting, CommGroupErrorMessage.InvalidWithdrawalFee);
            if (!validateWithdrawalFee.isValid)
            {
                messageList.Add(validateWithdrawalFee.message);
            }

            var isValid = messageList.Count == 0;
            return (isValid, messageList);
        }

        private static (bool isValid, string message) ValidateTierSettingList(IEnumerable<RevenueTierSetting> tierSettingList)
        {
            var tierSettingListByGroupProduct = tierSettingList.GroupBy(t => t.GroupProductId);

            foreach (var tierSettings in tierSettingListByGroupProduct)
            {
                var validateMinAmountResult = ValidateMinAmount(tierSettings);

                if (!validateMinAmountResult.isValid)
                {
                    return (false, validateMinAmountResult.message);
                }

                if (tierSettings.Any(item => item.MinQm > Validation.InfinityQualifiedMember))
                {
                    return (false, "Min QP must be lesser than or equal to 1,000,000,000");
                }
            }

            return (true, string.Empty);
        }

        private static (bool isValid, string message) ValidateMinAmount(IEnumerable<RevenueTierSetting> tierSettings)
        {
            if (tierSettings == null || !tierSettings.Any())
            {
                return (false, "Tier Setting must have at least 1 tier!");
            }

            var firsMinAmount = tierSettings.First().MinAmount;
            if (firsMinAmount != 0 && firsMinAmount != Validation.NegativeInfinityAmount)
            {
                return (false, "The first Tier of the Min Net Win column must be = 0 or -∞");
            }

            if (IsMinNetWinNotStartFromOneAfterNegative(tierSettings))
            {
                return (false, "The next Tier after a negative Tier must start from 1 in the Min Net Win column.");
            }

            if (IsNotIncreasingMinNetWin(tierSettings))
            {
                return (false, "The values in the Min Net Win column must increase sequentially.");
            }

            if (tierSettings.Any(item => item.MinAmount < Validation.NegativeInfinityAmount))
            {
                return (false, "Min Net Win must be greater than or equal to -100,000,000,000");
            }

            if (tierSettings.Any(item => item.MinAmount > Validation.InfinityAmount))
            {
                return (false, "Min Amount must be lesser than or equal to 100,000,000,000");
            }

            return (true, string.Empty);
        }

        private static bool IsNotIncreasingMinNetWin(IEnumerable<RevenueTierSetting> tierSettingList)
        {
            RevenueTierSetting previous = null;

            foreach (var tierSetting in tierSettingList)
            {
                if (previous != null && tierSetting.MinAmount <= previous.MinAmount)
                {
                    return true;
                }
                previous = tierSetting;
            }

            return false;
        }

        private static bool IsMinNetWinNotStartFromOneAfterNegative(IEnumerable<RevenueTierSetting> tierSettingList)
        {
            RevenueTierSetting previous = null;

            foreach (var tierSetting in tierSettingList)
            {
                var minAmount = tierSetting.MinAmount;

                if (previous != null && previous.MinAmount < 0 && (minAmount == 0 || minAmount > 1))
                {
                    return true;
                }
                previous = tierSetting;
            }

            return false;
        }

        private static (bool isValid, string message) ValidateDeductionList(IEnumerable<RevenueDeductionSetting> deductionList, IEnumerable<string> productIdOfDB, string message)
        {
            if (deductionList == null || !deductionList.Any())
            {
                return (false, message);
            }

            if (deductionList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                if (deductionList.Count() != Validation.OnlyOneRow)
                {
                    return (false, message);
                }
                return (true, string.Empty);
            }

            var productIdOfDeduction = string.Join(",", deductionList.Select(p => p.ProductIds.Replace(" ", "")))
                                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var distinctProductIdOfPlatform = productIdOfDeduction.Distinct();

            if (distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                return (false, message);
            }

            return (true, string.Empty);
        }

        private static (bool isValid, string message) ValidateFixedAmountDeductedFee(RevenueDeductionSetting deductedFee, string message)
        {
            if (!deductedFee.IsDeductedPercent)
            {
                try
                {
                    var deductedAmount = Convert.ToDecimal(deductedFee.DeductedAmount);

                    if (deductedAmount < 0 || deductedAmount > Money.AllowedMaxValue)
                    {
                        return (false, message);
                    }
                }
                catch
                {
                    return (false, message);
                }
            }

            return (true, string.Empty);
        }
    }
}
