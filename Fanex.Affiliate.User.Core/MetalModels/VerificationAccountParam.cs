using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class VerificationAccountParam
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }

        public string Captcha { get; set; }
    }
}
