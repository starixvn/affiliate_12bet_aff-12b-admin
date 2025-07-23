namespace Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery
{
    using System;

    public class Domain
    {
        public int DomainId { get; set; }

        public string DomainName { get; set; }

        public string DomainDescription { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsSsl { get; set; }

        public bool IsMobile { get; set; }
    }
}
