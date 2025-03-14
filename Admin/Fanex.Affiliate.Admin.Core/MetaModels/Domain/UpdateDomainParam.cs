namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    using System.ComponentModel.DataAnnotations;
    using Constants;

    public class UpdateDomainParam : InsertDomainParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainId { get; set; }
    }
}
