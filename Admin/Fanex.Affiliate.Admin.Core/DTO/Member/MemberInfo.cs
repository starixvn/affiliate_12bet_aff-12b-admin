using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Member
{
    public class MemberInfo
    {
        public int MemberId { get; set; }

        public string UserName { get; set; }

        public string IbcUserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CurrentBalance { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string AffName { get; set; }

        public string CreatedDateText => CreatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
