
namespace Fanex.Affiliate.Admin.Core.Criterias.Message
{
    public class MessageCriteria : PagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_Message_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public byte SearchById { get; set; }

        public bool IsGetAll { get; set; }
    }
}
