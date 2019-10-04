using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Utilities.CommGroup
{
    public static class CommGroupValidationHelper
    {
        private const string brElementHtml = "<br />";
        public static ResultInfo ValidatePlatformFeeList(IProductRepository productRepository, IEnumerable<DeductionSetting> platformFeeList)
        {
            var result = new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = CommGroupErrorMessage.InvalidPlatformFee
            };

            if (platformFeeList == null || !platformFeeList.Any() || platformFeeList.Any(item => string.IsNullOrEmpty(item.ProductIds)))
            {
                return result;
            }

            var productIdOfDB = productRepository.GetProductList().Select(p => p.ProductId.ToString());
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
            var result = new ResultInfo();

            if (tierSettingList == null || !tierSettingList.Any())
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + brElementHtml, "Tier Setting must have at least 1 tier!");
                return result;
            }

            if (!tierSettingList.Any(item => (item.MaxAmount == Validation.InfinityAmount && item.MaxQm == Validation.InfinityQualifiedMember)))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + brElementHtml, "Last Tier Setting must have maximum value=∞!");
                return result;
            }

            if (tierSettingList.Any(item => item.MinAmount > item.MaxAmount))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + brElementHtml, "Max Amount must be greater than or equal to Min Amount!");
                return result;
            }

            if (tierSettingList.Any(item => item.MinQm > item.MaxQm))
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = string.Join(result.Message + brElementHtml, "Max QM must be greater than or equal to Min QM!");
            }

            return result;
        }

        private static ResultInfo ValidateProductIds(IEnumerable<string> productIdOfDB, IEnumerable<string> productIdOfPlatform)
        {
            var result = new ResultInfo { Code = Code.GeneralErrorCode };

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