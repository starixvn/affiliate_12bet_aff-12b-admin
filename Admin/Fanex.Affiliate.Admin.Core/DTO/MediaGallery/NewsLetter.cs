using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.MediaGallery
{
    public class NewsLetter
    {
        public int NewsLetterId { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public DateTime? UploadDate { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }

        public string Language { get; set; }
    }
}
