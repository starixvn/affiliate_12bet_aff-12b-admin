using System.Collections.Generic;
using System.Linq;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly ICommissionGroupRepository _commRepository;

        public CommissionService(ICommissionGroupRepository commRepository)
        {
            _commRepository = commRepository;
        }

        public IEnumerable<CommissionGroupSetting> GetCommissionSettingList(CommissionGroupParam param)
        {
            var rawData = _commRepository.GetCommissionGroupList(param);

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
                             IsAdjustment = c.IsAdjustment,
                             NoAffs = c.NoAffs,
                             TierSettingList = groupTierSetting,
                             PlatformFeeList = groupDeduction.Where(p => IsPlatformFee(p.ProductIds)),
                             RemainingFeeList = groupDeduction.Where(p => !IsPlatformFee(p.ProductIds))
                         };

            return result;
        }

        private static bool IsPlatformFee(string productIdList)
        {
            return productIdList != "0";
        }
    }
}
