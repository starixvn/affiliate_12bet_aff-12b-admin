using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public class AffTypeService : BaseDynamicService, IAffTypeService
    {
        public AffTypeService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<AffTypeInfo> GetAffTypeList()
        {
            const string SpKey = "Aff_AffTypeSel";

            return FetchTData<AffTypeInfo>(SpKey);
        }
    }
}
