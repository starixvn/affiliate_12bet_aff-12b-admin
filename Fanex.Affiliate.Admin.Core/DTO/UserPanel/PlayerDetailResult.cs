using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class PlayerDetailResult
    {
        public IEnumerable<PlayerDetail> MainData { get; set; }

        public int TotalItem { get; set; }
    }
}
