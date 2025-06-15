namespace Fanex.Affiliate.Admin.Core.DTO.Operation
{
    using System.Collections.Generic;

    public class DealSettingDetail : DealSettingInfo
    {
        public IEnumerable<CpaTierSetting> CPATierSetting { get; set; }

        public IEnumerable<CpaGroupProduct> ProductGroup { get; set; }
    }
}
