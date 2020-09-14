using System;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class AdminListInfo
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Creator { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string LastLoginDateText => LastLoginDate?.ToString("MM/dd/yyyy HH:mm:ss");

        public string LastLoginIp { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }
    }
}
