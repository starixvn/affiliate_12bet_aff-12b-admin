using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class CustomerMember
    {
        public int CustId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Currency { get; set; }

        public DateTime RegisteredDate { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
