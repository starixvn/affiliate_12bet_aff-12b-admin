namespace Fanex.Affiliate.Admin.Core.MetaModels.Operation
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.Affiliate.Admin.Core.Constants;

    public class DeleteDealParam : UserInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "DealId not exists!")]
        public int DealId { get; set; }
    }
}
