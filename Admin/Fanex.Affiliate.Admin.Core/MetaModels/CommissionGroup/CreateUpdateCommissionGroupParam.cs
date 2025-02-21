
namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class CreateUpdateCommissionGroupParam : CommGroupSettingParam
    {
        public int UserId { get; set; }

        public string Ip { get; set; }

        public bool IsPrivate { get; set; }
    }
}
