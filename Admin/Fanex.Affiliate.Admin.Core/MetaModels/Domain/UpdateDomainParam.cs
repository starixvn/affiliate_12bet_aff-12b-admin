namespace Fanex.Affiliate.Admin.Core.MetaModels.Domain
{
    using Constants;
    using System.ComponentModel.DataAnnotations;

    public class UpdateDomainParam : InsertDomainParam
    {
        [Range(Validation.MinId, Validation.MaxId)]
        public int DomainId { get; set; }
    }
}
