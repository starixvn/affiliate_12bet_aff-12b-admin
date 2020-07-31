using System;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminUserInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedDateText => CreatedDate.ToString("MM/dd/yyyy HH:mm:ss");

        public DateTime? LastLoginDate { get; set; }

        public string LastLoginDateText => LastLoginDate?.ToString("MM/dd/yyyy HH:mm:ss");

        public string LastLoginIp { get; set; }

        public string RoleName { get; set; }
    }
}
