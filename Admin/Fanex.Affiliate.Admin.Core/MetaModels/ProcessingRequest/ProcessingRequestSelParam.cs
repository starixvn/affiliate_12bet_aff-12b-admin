using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest
{
    public class ProcessingRequestSelParam
    {
        public string UserName { get; set; }

        public string Status { get; set; }

        public int PageSize { get; set; }

        public string PreferredLanguages { get; set; }

        public int PageIndex { get; set; }

        public bool IsExportExcel { get; set; }

        public int NRows { get; set; }

    }
}
