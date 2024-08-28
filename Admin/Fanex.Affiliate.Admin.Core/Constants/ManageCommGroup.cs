using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Constants
{
    public static class ManageCommGroup
    {
        private const string DefaultCommGroupId = "101";
        private const string NoCommissionId = "102";
        private const string MediaId = "117";

        public static readonly string[] GroupIds = new[] { DefaultCommGroupId, NoCommissionId, MediaId };

        public static IEnumerable<SelectItem> GetManageCommGroups(IEnumerable<SelectItem> normalCommGroups)
        {
            var manageCommGroups = new List<SelectItem>();
            foreach (var id in GroupIds)
            {
                var commGroup = normalCommGroups.FirstOrDefault(c => c.Id.MatchWith(id));
                if (commGroup != null)
                {
                    manageCommGroups.Add(commGroup);
                }
            }

            return manageCommGroups;
        }

        public static bool IsValidManageCommGroup(int commGroupId)
        {
            var groupId = commGroupId.ToString();
            return Array.Exists(GroupIds, id => id.MatchWith(groupId));
        }
    }
}
