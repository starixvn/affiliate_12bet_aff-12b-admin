using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Extension
{
    public static class NameValueCollectionExtension
    {
        public static IEnumerable<KeyValuePair<string,string>> ToKeyValuePairCollection(this NameValueCollection col)
        {
            var result = new List<KeyValuePair<string, string>>();
            foreach (var key in col.AllKeys)
            {
                result.Add(new KeyValuePair<string, string>(key, col[key]));
            }
            return result;
        }
    }
}
