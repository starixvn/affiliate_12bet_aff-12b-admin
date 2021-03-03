using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery
{
    public class DeleteNewsLetterParam
    {
        public int UserId { get; set; }

        public string NewsLetterIds { get; set; }

        public string Ip { get; set; }
    }
}
