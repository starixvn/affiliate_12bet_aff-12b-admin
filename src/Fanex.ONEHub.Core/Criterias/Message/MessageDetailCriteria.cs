using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.Message
{
    public class MessageDetailCriteria : CriteriaBase
    {
        const string SP_NAME = "Aff_Admin_Message_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int MessageId { get; set; }
    }
}
