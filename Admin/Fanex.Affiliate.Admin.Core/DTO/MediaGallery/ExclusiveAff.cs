using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class ExclusiveAff
    {
        public int ExclusiveAffId { get; set; }

        public string FileName { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }
    }
}
