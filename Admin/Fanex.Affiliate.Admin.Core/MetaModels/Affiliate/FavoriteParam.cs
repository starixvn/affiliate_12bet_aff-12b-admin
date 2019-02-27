using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.Affiliate
{
    public class FavoriteParam
    {
        public int UserId { get; set; }

        public int AffId { get; set; }

        public bool IsFavorite { get; set; }

        public string Ip { get; set; }
    }
}
