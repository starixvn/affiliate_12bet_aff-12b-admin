
namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    public class DeleteDomainParam: UserInfoParam
    {
        public int DomainId { get; set; }

        public bool IsAffDomain { get; set; }
    }
}
