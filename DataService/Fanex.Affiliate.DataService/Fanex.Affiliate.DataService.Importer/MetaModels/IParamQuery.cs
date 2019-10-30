using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.MetaModels
{
    public interface IParamQuery
    {
        NameValueCollection BuildQueryCollection();
        string BuildQueryString();
    }
}
