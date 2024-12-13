using Fanex.Affiliate.Admin.Core.Commands.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias;
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
    public class CommGroupCommonService : ICommGroupCommonService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public CommGroupCommonService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var commandInsert = new InsertCommGroupCommand
            {
                GroupTypeId = param.GroupTypeId,
                IsPrivate = param.IsPrivate
            };

            AssignBaseCommissionGroupCommand(param, commandInsert);

            _dynamicRepository.Execute(commandInsert);

            param.GroupId = commandInsert.GroupId;

            return new ResultInfo(commandInsert.ErrCode, commandInsert.ErrMsg);
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            var commandUpdate = new UpdateCommGroupCommand
            {
                GroupId = param.GroupId
            };

            AssignBaseCommissionGroupCommand(param, commandUpdate);

            _dynamicRepository.Execute(commandUpdate);

            return new ResultInfo(commandUpdate.ErrCode, commandUpdate.ErrMsg);
        }

        public IEnumerable<DeductionItem> GetDeductionList()
        {
            const string SpKey = "Aff_Admin_CommGroup_Deductions_Sel";

            return _dynamicRepository.Fetch<DeductionItem>(new BaseCriteria(SpKey)).Where(d => d.DeductionId != Deduction.OperationalFeeId);
        }

        public IEnumerable<CommGroupType> GetCommGroupTypes()
        {
            const string SpKey = "Aff_Admin_CommGroup_GroupTypeSel";

            return _dynamicRepository.Fetch<CommGroupType>(new BaseCriteria(SpKey));
        }

        public IEnumerable<NormalCommGroup> GetNormalCommGroupList(GetGroupListParam param)
        {
            var criteria = new GetNormalCommGroupCriteria
            {
                AffId = param.AffId,
                GroupTypeId = param.GroupTypeId,
            };

            var normalCommGroups = _dynamicRepository.Fetch<NormalCommGroup>(criteria);
            param.GroupId = criteria.GroupId;

            return normalCommGroups;
        }

        private static void AssignBaseCommissionGroupCommand(CreateUpdateCommissionGroupParam param, BaseCommGroupCommand command)
        {
            var (tierSettingXml, tierDeductionsXml, p2pDeductionsXml) = GetSettingXmls(param);

            command.UserId = param.UserId;
            command.GroupName = param.GroupName;
            command.Description = param.Description;
            command.SubAffCommRate = param.SubAffCommRate;
            command.IsActivePlayerSum = param.IsActivePlayerSum;
            command.Ip = param.Ip;
            command.TierSettingXml = tierSettingXml;
            command.TierDeductionsXml = tierDeductionsXml;
            command.P2pDeductionsXml = p2pDeductionsXml;
        }

        private static (string tierSettingXml, string tierDeductionsXml, string p2pDeductionsXml) GetSettingXmls(CreateUpdateCommissionGroupParam param)
        {
            var tierSettingXml = BuildTierSettingXml(param.TierSettingList, param.PlatformFeeList);

            var tierDeductionSettingList = param.PlatformFeeList.Concat(new[] { param.DepositFee, param.WithdrawalFee });
            if (param.KillRateFeeList != null && param.KillRateFeeList.Any())
            {
                tierDeductionSettingList = tierDeductionSettingList.Concat(param.KillRateFeeList);
            }

            var tierDeductionsXml = BuildTierDeductionSettingXml(tierDeductionSettingList);
            var p2pDeductionsXml = BuildP2pDeductionSettingXml(param.P2pDeductionList);

            return (tierSettingXml, tierDeductionsXml, p2pDeductionsXml);
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
                        $" minam=\"{item.MinAm}\" commission=\"{item.Commission}\" productids=\"{productIds}\" />";
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

        private static string BuildP2pDeductionSettingXml(IEnumerable<P2PDeductionSettingParam> p2pDeductionSettingList)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            foreach (var item in p2pDeductionSettingList)
            {
                var row = $"<row id=\"{item.DeductionId}\" productids=\"{item.ProductIds}\" percent=\"{item.DeductedPercentage}\"" +
                    $" commission=\"{item.Commission}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            return result.ToString();
        }
    }
}