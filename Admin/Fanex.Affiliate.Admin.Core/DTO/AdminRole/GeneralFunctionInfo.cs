using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.DTO.AdminRole
{
    public class GeneralFunctionInfo
    {
        public int FuncId { get; set; }

        public string FuncName { get; set; }

        public int Policy { get; set; }

        public bool IsRead => Policy == Constants.Policy.ReadButNotHost || Policy == Constants.Policy.ReadAndHost;

        public bool IsHost => Policy == Constants.Policy.ReadAndHost;

        public IEnumerable<GeneralFunctionInfo> Children { get; set; }
    }
}
