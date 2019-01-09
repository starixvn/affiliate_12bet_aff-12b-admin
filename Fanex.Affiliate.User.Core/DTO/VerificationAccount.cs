using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class VerificationAccount
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string SecretKey { get; set; }

        public int Counter { get; set; }
    }
}
