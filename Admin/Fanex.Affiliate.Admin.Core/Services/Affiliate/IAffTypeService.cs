using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.Affiliate
{
    public interface IAffTypeService
    {
        IEnumerable<AffTypeInfo> GetAffTypeList();
    }
}
