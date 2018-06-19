using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetMemberList(MemberParam customerParam)
        {
            const string uSpKey = "Aff_MemberList_Sel";
            var mode = customerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, mode))
            {
                return objectDb.DB.Query<Member>(customerParam);
            }
        }
    }
}
