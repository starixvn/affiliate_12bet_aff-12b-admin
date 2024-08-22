using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class ManageCommGroup
    {
        private const int DefaultCommGroup = 101;
        private const int NoCommission = 102;
        private const int Media = 117;

        public static readonly int[] GroupIds = new []{ DefaultCommGroup, NoCommission, Media };

        public static readonly IEnumerable<SelectItem> NormalItems = new List<SelectItem> {
            new SelectItem { Id = DefaultCommGroup.ToString(), Name = "Default Comm Group"},
            new SelectItem { Id = NoCommission.ToString(), Name = "NoCommission"},
            new SelectItem { Id = Media.ToString(), Name = "Media"},
        };
    }
}
