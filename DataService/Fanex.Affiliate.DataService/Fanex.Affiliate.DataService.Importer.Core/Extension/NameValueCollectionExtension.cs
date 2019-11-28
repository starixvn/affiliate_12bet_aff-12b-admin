using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

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

        public static Dictionary<string,string> ToDictionary(this NameValueCollection col)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in col.AllKeys)
            {
                result.Add(key, col[key]);
            }
            return result;
        }

        public static string ToQuery(this NameValueCollection col)
        {
            var builder = new StringBuilder(50);
            var count = col.AllKeys.Count();
            var index = 1;
            foreach (var key in col.AllKeys)
            { 
                builder.Append(key).Append("=").Append(col[key]);
                if (index < count)
                {
                    builder.Append("&");
                }
                index++;
            }
            return builder.ToString();
        }
    }
}
