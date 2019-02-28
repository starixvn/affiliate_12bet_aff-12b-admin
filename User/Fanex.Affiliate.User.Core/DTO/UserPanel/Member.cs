using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class Member
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? RegisteredDate { get; set; }

        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
