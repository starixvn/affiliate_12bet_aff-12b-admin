using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.DTO
{
    public class ApiParam
    {
        public NameValueCollection Param { get; set; }

        public IHttpContentProvider ContentProvider { get; set; }
    }
}
