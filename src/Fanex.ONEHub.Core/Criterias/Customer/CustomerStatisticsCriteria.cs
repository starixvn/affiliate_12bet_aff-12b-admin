using Fanex.Data.Repository;
using System;

namespace Fanex.Affiliate.Admin.Core.Criterias.Customer
{
    public class CustomerStatisticsCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_Statistics_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public int AffId { get; set; }

        public DateTime? MonthDate { get; set; }
    }
}
