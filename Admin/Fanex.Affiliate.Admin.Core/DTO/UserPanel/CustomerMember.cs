using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class CustomerMember
    {
        [PermissionCheck(Function.AffMemberName)]
        public string FirstName { get; set; }

        [PermissionCheck(Function.AffMemberName)]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string CurrencyName { get; set; }

        public DateTime RegisteredDate { get; set; }

        public string RegisteredDateString => RegisteredDate.ToString("MM/dd/yyyy hh:mm:ss tt");
    }
}
