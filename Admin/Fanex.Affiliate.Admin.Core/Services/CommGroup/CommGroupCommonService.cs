using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public class CommGroupCommonService : ICommGroupCommonService
    {
        private readonly ICommissionGroupRepository _commGroupRepository;

        public CommGroupCommonService(ICommissionGroupRepository commGroupRepository)
        {
            _commGroupRepository = commGroupRepository;
        }

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var result = CommGroupValidationHelper.ValidateTierSettingList(param.TierSettingList);

            if (result.IsSuccessful)
            {
                var deductionSettingList = param.PlatformFeeList.Concat(new[] { param.OperationalFee, param.BankingFee, param.ExchangeWinLossShare, param.ExchangeCommissionShare });
                param.TierList = BuildTierSettingXml(param.TierSettingList);
                param.DeductionList = BuildDeductionSettingXml(deductionSettingList, true);

                return _commGroupRepository.CreateCommissionGroup(param);
            }

            return result;
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var result = CommGroupValidationHelper.ValidateTierSettingList(param.TierSettingList);

            if (result.IsSuccessful)
            {
                var deductionSettingList = param.PlatformFeeList.Concat(new[] { param.OperationalFee, param.BankingFee, param.ExchangeWinLossShare, param.ExchangeCommissionShare });
                param.TierList = BuildTierSettingXml(param.TierSettingList);
                param.DeductionList = BuildDeductionSettingXml(deductionSettingList, false);

                return _commGroupRepository.UpdateCommissionGroup(param);
            }

            return result;
        }

        public IEnumerable<Deduction> GetDeductionList()
        {
            return _commGroupRepository.GetDeductionList();
        }

        private static string BuildTierSettingXml(IEnumerable<TierSetting> tierSettingList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in tierSettingList)
            {
                var row = $"<row id=\"{item.TierId}\" minamount=\"{item.MinAmount}\" maxamount=\"{item.MaxAmount}\" minqm=\"{item.MinQm}\" maxqm=\"{item.MaxQm}\" commission=\"{item.Commission}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildDeductionSettingXml(IEnumerable<DeductionSetting> deductionSettingList, bool isInsert)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in deductionSettingList)
            {
                var percent = isInsert ? item.DeductedPercentage : item.NextDeductedPercentage;
                var row = $"<row id=\"{item.DeductionId}\" productids=\"{item.ProductIds}\" percent=\"{percent}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}