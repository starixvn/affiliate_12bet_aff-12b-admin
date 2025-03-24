using Fanex.Affiliate.Admin.Core.Commands.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public class CommGroupCommonService : BaseDynamicService, ICommGroupCommonService
    {
        public CommGroupCommonService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var commandInsert = new InsertCommGroupCommand
            {
                IsPrivate = param.IsPrivate
            };

            AssignBaseCommissionGroupCommand(commandInsert, param);

            var result = ExecuteReturnResultInfo(commandInsert);

            param.GroupId = commandInsert.GroupId;

            return result;
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var commandUpdate = new UpdateCommGroupCommand
            {
                GroupId = param.GroupId
            };

            AssignBaseCommissionGroupCommand(commandUpdate, param);

            return ExecuteReturnResultInfo(commandUpdate);
        }

        public IEnumerable<DeductionItem> GetDeductionList()
        {
            const string SpKey = "Aff_Admin_CommGroup_Deductions_Sel";

            return FetchTData<DeductionItem>(SpKey);
        }

        public IEnumerable<NormalCommGroup> GetNormalCommGroupList(GetGroupListParam param)
        {
            var criteria = new GetNormalCommGroupCriteria
            {
                AffId = param.AffId
            };

            var normalCommGroups = FetchTData<NormalCommGroup>(criteria);
            param.GroupId = criteria.GroupId;

            return normalCommGroups;
        }

        private static void AssignBaseCommissionGroupCommand(BaseCommGroupCommand command, CreateUpdateCommissionGroupParam param)
        {
            var (tierSettingXml, tierDeductionsXml) = GetSettingXmls(param);

            command.UserId = param.UserId;
            command.GroupName = param.GroupName;
            command.Description = param.Description;
            command.SubAffCommRate = param.SubAffCommRate;
            command.QmTurnover = param.QmTurnover;
            command.IsActivePlayerSum = param.IsActivePlayerSum;
            command.Ip = param.Ip;
            command.TierSettingXml = tierSettingXml;
            command.TierDeductionsXml = tierDeductionsXml;
        }

        private static (string tierSettingXml, string tierDeductionsXml) GetSettingXmls(CreateUpdateCommissionGroupParam param)
        {
            var tierSettingXml = BuildTierSettingXml(param.TierSettingList, param.PlatformFeeList);

            var tierDeductionSettingList = param.PlatformFeeList.Concat(new[] { param.DepositFee, param.WithdrawalFee });

            var tierDeductionsXml = BuildTierDeductionSettingXml(tierDeductionSettingList);

            return (tierSettingXml, tierDeductionsXml);
        }

        private static string BuildTierSettingXml(IEnumerable<TierSettingParam> tierSettingParamList, IEnumerable<TierDeductionSettingParam> platformFeeList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var platformFee in platformFeeList)
            {
                var tierSettings = tierSettingParamList.Where(t => t.GroupProductId == platformFee.GroupProductId);

                foreach (var item in tierSettings)
                {
                    var productIds = platformFeeList.FirstOrDefault(p => p.GroupProductId == item.GroupProductId)?.ProductIds;

                    var row = $"<row id=\"{item.TierId}\" minamount=\"{item.MinAmount}\"" +
                        $" minqm=\"{item.MinQm}\" commission=\"{item.Commission}\" productids=\"{productIds}\" />";
                    result.Append(row);
                }
            }

            result.Append("</root>");

            return result.ToString();
        }

        private static string BuildTierDeductionSettingXml(IEnumerable<TierDeductionSettingParam> feeDeductionSettingList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in feeDeductionSettingList)
            {
                var row = $"<row id=\"{item.DeductionId}\" productids=\"{item.ProductIds}\" percent=\"{item.DeductedPercentage}\"" +
                    $" amount=\"{item.DeductedAmount}\" isdeductedpercent=\"{item.IsDeductedPercent}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}