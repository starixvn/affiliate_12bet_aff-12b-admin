using Fanex.Affiliate.Admin.Core.Commands.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Criterias.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.CommGroup
{
    public class CommissionGroupService : ICommissionGroupService
    {
        private readonly IDynamicRepository _dynamicRepository;

        private readonly ICommGroupCommonService _commGroupCommonService;

        public CommissionGroupService(IDynamicRepository dynamicRepository, ICommGroupCommonService commGroupCommonService)
        {
            _dynamicRepository = dynamicRepository;
            _commGroupCommonService = commGroupCommonService;
        }

        public IEnumerable<CommissionGroupSetting> GetCommissionGroupList(GetCommissionGroupParam param)
        {
            var criteria = new GetCommissionGroupCriteria
            {
                GroupId = param.GroupId,
                GroupName = param.GroupName,
                GroupTypeId = param.GroupTypeId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            var (commGroupList, tierSettingList, tierDeductionSettingList, p2pDeductionSettingList)
                = _dynamicRepository.FetchMultiple<DTO.CommissionGroup.CommGroup, TierSetting, DeductionSetting, P2pDeductionSetting>(criteria);

            param.Nrows = criteria.Nrows;

            var result = from c in commGroupList
                         join t in tierSettingList on c.GroupId equals t.GroupId into groupTierSetting
                         join d in tierDeductionSettingList on c.GroupId equals d.GroupId into groupTierDeductionSetting
                         join p in p2pDeductionSettingList on c.GroupId equals p.GroupId into groupP2pDeductionSetting
                         select new CommissionGroupSetting
                         {
                             RowIdx = c.RowIdx,
                             GroupId = c.GroupId,
                             GroupName = c.GroupName,
                             GroupTypeId = c.GroupTypeId,
                             GroupTypeName = c.GroupTypeName,
                             GroupTypeBy = c.GroupTypeBy,
                             SubCommRate = c.SubCommRate,
                             CreatedDate = c.CreatedDate,
                             Creator = c.Creator,
                             Description = c.Description,
                             NoAffs = c.NoAffs,
                             IsActivePlayerSum = c.IsActivePlayerSum,
                             TierSettingList = groupTierSetting,
                             P2pDeductionList = groupP2pDeductionSetting,
                             PlatformFeeList = groupTierDeductionSetting.Where(p => p.DeductionId == Constants.Deduction.PlatformFeeId),
                             DepositFee = groupTierDeductionSetting.FirstOrDefault(p => p.DeductionId == Constants.Deduction.DepositFeeId),
                             WithdrawalFee = groupTierDeductionSetting.FirstOrDefault(p => p.DeductionId == Constants.Deduction.WithdrawalFeeId),
                             KillRateFeeList = groupTierDeductionSetting.Where(p => p.DeductionId == Constants.Deduction.KillRateFeeId),
                         };

            return result;
        }

        public ResultInfo CreateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            return _commGroupCommonService.CreateCommissionGroup(param);
        }

        public ResultInfo UpdateCommissionGroup(CreateUpdateCommissionGroupParam param)
        {
            return _commGroupCommonService.UpdateCommissionGroup(param);
        }

        public ResultInfo DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            var command = new DeleteCommGroupCommand
            {
                GroupId = param.GroupId,
                UserId = param.UserId,
                Ip = param.Ip
            };

            _dynamicRepository.Execute(command);

            return new ResultInfo(command.ErrCode, command.ErrMsg);
        }
    }
}