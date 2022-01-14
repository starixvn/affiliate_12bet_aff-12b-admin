
using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class NewsLetter : HtmlFileInfo
    {
        public int NewsLetterId { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }
    }
}
