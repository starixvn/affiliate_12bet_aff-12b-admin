using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class NewsLetter
    {
        public int NewsLetterId { get; set; }

        public string FileName { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public string Language { get; set; }
    }
}
