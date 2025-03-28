namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    using Constants;
    using System.ComponentModel.DataAnnotations;

    public class UpdateDomainControlParam : InsertDomainControlParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainControlId { get; set; }
    }
}
