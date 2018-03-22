using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.DTO
{
    public class JResult
    {
        public bool IsOk { get; set; }
        [JsonProperty("Msg")]
        public bool Message { get; set; }
    }
}
