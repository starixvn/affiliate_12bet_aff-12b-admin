using Fanex.Affiliate.Admin.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.AdminRole
{
    public class PermissionInfoParam
    {
        [Range(Validation.MinId, Validation.MaxId, ErrorMessage = "Func Id is invalid!")]
        public int FuncId { get; set; }

        public bool IsRead { get; set; }

        public bool IsHost { get; set; }

        public int Policy => GetPolicy(IsRead, IsHost);

        private static int GetPolicy(bool isRead, bool isHost)
        {
            if (isHost)
            {
                return Constants.Policy.ReadAndHost;
            }

            if (isRead)
            {
                return Constants.Policy.ReadButNotHost;
            }

            return Constants.Policy.NotReadAndHost;
        }
    }
}
