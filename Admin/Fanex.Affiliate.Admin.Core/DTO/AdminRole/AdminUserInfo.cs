using System;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminUserInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? LoggedIn { get; set; }

        public string LoggedInText => LoggedIn?.ToString("MM/dd/yyyy HH:mm:ss");

        public DateTime? LastRequest { get; set; }

        public string LastRequestText => LastRequest?.ToString("MM/dd/yyyy HH:mm:ss");

        public string Ip { get; set; }

        public int Role { get; set; }
    }
}
