using Newtonsoft.Json;
using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Utilities
{
    public static class XmlHelper
    {
        public static XDocument ParseToXDocument(string xml)
        {
            try
            {
                return XDocument.Parse(xml);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Convert json to XDocument
        /// </summary>
        /// <param name="json"></param>
        /// <param name="arrayName">root that is appended to json in case no root for an array in json</param>
        /// <returns></returns>
        public static XDocument ParseToXDocumentFromJson(string json, string arrayName = "")
        {
            try
            {
                var builder = new StringBuilder();
                var result = json;
                if (!string.IsNullOrEmpty(arrayName))
                {
                    result = builder.Append("{\"").Append("Response").Append('"').Append(':').Append(json).Append('}').ToString();
                }
                return JsonConvert.DeserializeXNode(result, "root", true);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
