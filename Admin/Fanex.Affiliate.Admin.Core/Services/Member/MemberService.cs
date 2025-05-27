using Fanex.Affiliate.Admin.Core.Commands.Member;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.Member;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public class MemberService : BaseDynamicService, IMemberService
    {
        public MemberService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public EntityResult<MemberInfo> GetMemberList(MemberParam param)
        {
            var criteria = new MemberCriteria
            {
                AffUsername = param.AffName,
                UserName = param.UserName,
                IBCUserName = param.IbcUserName,
                IsIncludeRemovedPlayer = param.IsIncludeRemovedPlayer,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };

            return FetchEntityResult<MemberInfo>(criteria);
        }

        public PlayerActionInfo GetMemberDetail(int memberId)
        {
            var criteria = new MemberDetailCriteria
            {
                MemberId = memberId,
            };

            return GetTData<PlayerActionInfo>(criteria);
        }

        public ResultInfo UpdateMember(UpdateMemberParam param)
        {
            var latestBillingDate = GetLatestBillingDate();
            var now = DateTimeHelper.Gmt8Now;
            var nextMonth = DateTimeHelper.CreateDateTime(now.Year, now.Month + 2, 1);

            if (param.FromDate > latestBillingDate && param.FromDate < nextMonth)
            {
                var command = new UpdateMemberCommand
                {
                    UserId = param.UserId,
                    Ip = param.Ip,
                    MemberId = param.MemberId,
                    FromDate = param.FromDate
                };

                return ExecuteReturnResultInfo(command);
            }

            return new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = "The selected date cannot be earlier than the latest billing date or greater than next month."
            };
        }

        public DateTime GetLatestBillingDate()
        {
            var latestBillingDate = GetTData<LatestBillingDate>(new LatestBillingDateCriteria());

            return latestBillingDate.SValue;
        }
    }
}
