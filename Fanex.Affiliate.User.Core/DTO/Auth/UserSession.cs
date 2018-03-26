using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.DTO.Auth
{
    public class UserSession
    {
        public bool IsInternal { get; set; }
        public string LanguageCode { get; set; }
        public string UserName { get; set; }
    }
}
