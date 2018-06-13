using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    [Serializable]
    public class IbcPlayerAccountInsertResult : ResultInfo
    {
        public int CustId { get; set; }

        public string UserName { get; set; }
    }
}
