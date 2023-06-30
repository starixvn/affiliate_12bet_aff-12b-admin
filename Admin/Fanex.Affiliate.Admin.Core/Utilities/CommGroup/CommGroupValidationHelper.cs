using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.CommGroup
{
    public static class CommGroupValidationHelper
    {
        public static ResultInfo ValidatePlatformFeeList(ICommonSettingService commonSettingService, IEnumerable<DeductionSetting> platformFeeList)
        {
            var result = ResultInfo.GenerateErr(CommGroupErrorMessage.InvalidPlatformFee);

            if (platformFeeList == null || !platformFeeList.Any() || platformFeeList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                return result;
            }

            var productIdOfDB = commonSettingService.GetProductList().Select(p => p.ProductId.ToString());
            var productIdOfPlatform = string.Join(",", platformFeeList.Select(p => p.ProductIds.Replace(" ", "")))
                                        .Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            var productIdValidation = ValidateProductIds(productIdOfDB, productIdOfPlatform);
            if (!productIdValidation.IsSuccessful)
            {
                return result;
            }

            return new ResultInfo();
        }

        public static ResultInfo ValidateTierSettingList(IEnumerable<TierSetting> tierSettingList)
        {
            if (tierSettingList == null || !tierSettingList.Any())
            {
                return ResultInfo.GenerateErr("Tier Setting must have at least 1 tier!");
            }

            if (!tierSettingList.Any(item => (item.MaxAmount == Validation.InfinityAmount && item.MaxQm == Validation.InfinityQualifiedMember)))
            {
                return ResultInfo.GenerateErr("Last Tier Setting must have maximum value=∞!");
            }

            if (tierSettingList.Any(item => item.MinAmount > item.MaxAmount))
            {
                return ResultInfo.GenerateErr("Max Amount must be greater than or equal to Min Amount!");
            }

            if (tierSettingList.Any(item => item.MinQm > item.MaxQm))
            {
                return ResultInfo.GenerateErr("Max QM must be greater than or equal to Min QM!");
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateProductIds(IEnumerable<string> productIdOfDB, IEnumerable<string> productIdOfPlatform)
        {
            var result = ResultInfo.GenerateErr();

            var inputProductIdListCount = productIdOfPlatform.Count();
            if (productIdOfDB.Count() != inputProductIdListCount)
            {
                return result;
            }

            var distinctProductIdOfPlatform = productIdOfPlatform.Distinct();
            if (inputProductIdListCount != distinctProductIdOfPlatform.Count() ||
                distinctProductIdOfPlatform.Any(item => !productIdOfDB.Contains(item)))
            {
                return result;
            }

            return new ResultInfo();
        }
    }
}