namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetOfferByIdCriteria : CriteriaBase
    {
        public GetOfferByIdCriteria(int pageId)
        {
            PageId = pageId;
        }

        public int PageId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_OfferPage_Sel";

        public override bool IsValid() => PageId > 0;
    }
}
