using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class CommissionGroupService : ICommissionGroupService
    {
        private readonly ICommissionGroupRepository _commGroupRepository;

        private readonly ICommGroupCommonService _commGroupCommonService;

        public CommissionGroupService(ICommissionGroupRepository commGroupRepository, ICommGroupCommonService commGroupCommonService)
        {
            _commGroupRepository = commGroupRepository;
            _commGroupCommonService = commGroupCommonService;
        }

        public IEnumerable<CommissionGroupSetting> GetCommissionGroupList(GetCommissionGroupParam param)
        {
            var rawData = _commGroupRepository.GetCommissionGroupList(param);

            var commGroupList = rawData.CommGroupList;
            var tierSettingList = rawData.TierSettingList;
            var deductionSettingList = rawData.DeductionSettingList;

            var result = from c in commGroupList
                         join t in tierSettingList on c.GroupId equals t.GroupId into groupTierSetting
                         join d in deductionSettingList on c.GroupId equals d.GroupId into groupDeduction
                         select new CommissionGroupSetting
                         {
                             RowIdx = c.RowIdx,
                             GroupId = c.GroupId,
                             GroupName = c.GroupName,
                             CreatedDate = c.CreatedDate,
                             Creator = c.Creator,
                             Description = c.Description,
                             QmTurnover = c.QmTurnover,
                             NextQmTurnover = c.NextQmTurnover,
                             NoAffs = c.NoAffs,
                             TierSettingList = groupTierSetting,
                             PlatformFeeList = groupDeduction.Where(p => p.DeductionId == DeductionId.PlatformFee),
                             OperationalFee = groupDeduction.FirstOrDefault(p => p.DeductionId == DeductionId.OperationalFee),
                             BankingFee = groupDeduction.FirstOrDefault(p => p.DeductionId == DeductionId.BankingFee),
                             ExchangeWinLossShare = groupDeduction.FirstOrDefault(p => p.DeductionId == DeductionId.ExchangeWinLossShare),
                             ExchangeCommissionShare = groupDeduction.FirstOrDefault(p => p.DeductionId == DeductionId.ExchangeCommissionShare)
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
            return _commGroupRepository.DeleteCommissionGroup(param);
        }
    }
}