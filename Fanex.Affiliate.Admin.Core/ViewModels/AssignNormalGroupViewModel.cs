using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.ViewModels
{
    public class AssignNormalGroupViewModel
    {
        public IEnumerable<SelectItem> CommGroupList { get; set; }

        public int SelectedGroup { get; set; }

        public string AffName { get; set; }
    }
}
