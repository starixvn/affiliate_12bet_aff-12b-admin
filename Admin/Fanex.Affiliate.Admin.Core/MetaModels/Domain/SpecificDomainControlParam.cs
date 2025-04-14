namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class SpecificDomainControlParam
    {
        public string AffUsername { get; set; }

        public string AffDomain { get; set; }

        public string RedirectDomain { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
