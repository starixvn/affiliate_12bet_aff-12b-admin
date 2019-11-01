
namespace Fanex.Affiliate.Admin.Core.DTO.Administrator
{
    public class PolicyInfo
    {
        public int ParentId { get; set; }

        public int Policy { get; set; }

        public int FunctionId { get; set; }

        public int FunctionDescription { get; set; }

        public string GroupName { get; set; }

        public int SubFunctionCount { get; set; }
    }
}
