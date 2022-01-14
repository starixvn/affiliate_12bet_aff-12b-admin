
using System;

namespace Fanex.Affiliate.User.Core.DTO.UserPanel
{
    public class Offer : HtmlFileInfo
    {
        public int PageId { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }
    }
}
