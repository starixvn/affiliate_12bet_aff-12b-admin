using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.Member
{
    public interface IActiveMemberService
    {
        AllMemberEntities GetAllMembers(string userNames);

        Task<ResultInfo> ActiveMemberAsync(ActiveMemberParam param);
    }
}
