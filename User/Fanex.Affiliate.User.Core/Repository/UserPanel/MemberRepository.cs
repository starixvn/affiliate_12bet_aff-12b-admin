using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IObjectDBBuilder _builder;

        public MemberRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<Member> GetMemberList(MemberParam customerParam)
        {
            const string uSpKey = "Aff_MemberList_Sel";
            var mode = customerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.Query<Member>(customerParam);
            }
        }
    }
}
