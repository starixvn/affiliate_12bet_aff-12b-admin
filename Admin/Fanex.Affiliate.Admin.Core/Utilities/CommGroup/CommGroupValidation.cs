using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Auth;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.CommGroup
{
    public class CommGroupValidation : ICommGroupValidation
    {
        private const string brElementHtml = "<br />";

        private readonly IAuthService _authService;
        private readonly IAffiliateService _affiliateService;
        private readonly ISystemService _systemService;
        private readonly ICommGroupCommonService _commGroupCommonService;

        public CommGroupValidation(
            IAuthService authService,
            IAffiliateService affiliateService,
            ISystemService systemService,
            ICommGroupCommonService commGroupCommonService)
        {
            _authService = authService;
            _affiliateService = affiliateService;
            _systemService = systemService;
            _commGroupCommonService = commGroupCommonService;
        }

        public bool HasPermissionAssignOnAff(int userId, int affId)
        {
            var affInfo = _affiliateService.GetAffiliateBasicInfo(affId);

            if (string.IsNullOrEmpty(affInfo.AffCode))
            {
                var permissionList = _authService.GetPermissionList(userId);

                return permissionList.Any(i => i.FunctionName.MatchWith(Function.EditAffStatus) && i.IsHost);
            }

            return true;
        }

        public ResultInfo ValidateCommGroup(CommGroupSettingParam commGroup)
        {
            var result = ValidateAllFees(commGroup);

            var validateTierSettingList = ValidateTierSettingList(commGroup.TierSettingList);

            if (!validateTierSettingList.IsSuccessful)
            {
                result.Code = ErrCode.GeneralErrorCode;
                result.Message = string.Join(result.Message, validateTierSettingList.Message);
            }

            return result;
        }

        private ResultInfo ValidateAllFees(CommGroupSettingParam commGroup)
        {
            var result = new ResultInfo();
            var productIdOfDB = _systemService.GetProductList().Select(p => p.ProductId.ToString());

            var validatePlatformFeeList = ValidateDeductionList(commGroup.PlatformFeeList, productIdOfDB, CommGroupErrorMessage.InvalidPlatformFee);
            if (!validatePlatformFeeList.IsSuccessful)
            {
                UpdateResult(result, validatePlatformFeeList.Message);
            }

            var validateDepositFee = ValidateFixedAmountDeductedFee(commGroup.DepositFee, CommGroupErrorMessage.InvalidDepositFee);
            if (!validateDepositFee.IsSuccessful)
            {
                UpdateResult(result, validateDepositFee.Message);
            }

            var validateWithdrawalFee = ValidateFixedAmountDeductedFee(commGroup.WithdrawalFee, CommGroupErrorMessage.InvalidWithdrawalFee);
            if (!validateWithdrawalFee.IsSuccessful)
            {
                UpdateResult(result, validateWithdrawalFee.Message);
            }

            return result;
        }

        private static ResultInfo ValidateTierSettingList(IEnumerable<TierSettingParam> tierSettingList)
        {
            var tierSettingListByGroupProduct = tierSettingList.GroupBy(t => t.GroupProductId);

            foreach (var tierSettings in tierSettingListByGroupProduct)
            {
                var validateMinAmountResult = ValidateMinAmount(tierSettings);

                if (!validateMinAmountResult.IsSuccessful)
                {
                    return validateMinAmountResult;
                }

                if (tierSettings.Any(item => item.MinQm > Validation.InfinityQualifiedMember))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = "Min QP must be lesser than or equal to 1,000,000,000"
                    };
                }
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateMinAmount(IEnumerable<TierSettingParam> tierSettings)
        {
            var result = new ResultInfo();

            if (tierSettings == null || !tierSettings.Any())
            {
                UpdateResult(result, "Tier Setting must have at least 1 tier!");
                return result;
            }

            var firsMinAmount = tierSettings.First().MinAmount;
            if (firsMinAmount != 0 && firsMinAmount != Validation.NegativeInfinityAmount)
            {
                UpdateResult(result, "The first Tier of the Min Net Win column must be = 0 or -∞");
                return result;
            }

            if (IsMinNetWinNotStartFromOneAfterNegative(tierSettings))
            {
                UpdateResult(result, "The next Tier after a negative Tier must start from 1 in the Min Net Win column.");
                return result;
            }

            if (IsNotIncreasingMinNetWin(tierSettings))
            {
                UpdateResult(result, "The values in the Min Net Win column must increase sequentially.");
                return result;
            }

            if (tierSettings.Any(item => item.MinAmount < Validation.NegativeInfinityAmount))
            {
                UpdateResult(result, "Min Net Win must be greater than or equal to -100,000,000,000");
                return result;
            }

            if (tierSettings.Any(item => item.MinAmount > Validation.InfinityAmount))
            {
                UpdateResult(result, "Min Amount must be lesser than or equal to 100,000,000,000");
                return result;
            }

            return result;
        }

        private static bool IsNotIncreasingMinNetWin(IEnumerable<TierSettingParam> tierSettingList)
        {
            TierSettingParam previous = null;

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

        private static bool IsMinNetWinNotStartFromOneAfterNegative(IEnumerable<TierSettingParam> tierSettingList)
        {
            TierSettingParam previous = null;

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

        private ResultInfo ValidateDeductionList(IEnumerable<DeductionSettingParam> deductionList, IEnumerable<string> productIdOfDB, string message)
        {
            var result = new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = message
            };

            if (deductionList == null || !deductionList.Any())
            {
                return result;
            }

            if (deductionList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                return deductionList.Count() == Validation.OnlyOneRow ? new ResultInfo() : result;
            }

            var productIdOfDeduction = string.Join(",", deductionList.Select(p => p.ProductIds.Replace(" ", "")))
                                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var distinctProductIdOfPlatform = productIdOfDeduction.Distinct();

            if (distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                return result;
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateFixedAmountDeductedFee(TierDeductionSettingParam deductedFee, string message)
        {
            var result = new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = message
            };

            if (!deductedFee.IsDeductedPercent)
            {
                try
                {
                    var deductedAmount = Convert.ToDecimal(deductedFee.DeductedAmount);

                    if (deductedAmount < 0 || deductedAmount > Money.AllowedMaxValue)
                    {
                        return result;
                    }
                }
                catch
                {
                    return result;
                }
            }

            return new ResultInfo();
        }

        private static void UpdateResult(ResultInfo initialResult, string message)
        {
            initialResult.Code = ErrCode.GeneralErrorCode;
            initialResult.Message = string.Join(initialResult.Message + brElementHtml, message);
        }
    }
}