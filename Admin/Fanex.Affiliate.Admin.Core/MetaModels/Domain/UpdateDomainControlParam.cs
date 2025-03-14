namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class UpdateDomainControlParam : InsertDomainControlParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainControlId { get; set; }
    }
}
