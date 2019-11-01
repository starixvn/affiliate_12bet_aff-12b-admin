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
        /// Convert json to XDocument.
        /// Root tag name is root.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="arrayTagName">Array tag in case json is an array. The default tag for array is root</param>
        /// <returns></returns>
        public static XDocument ParseToXDocumentFromJson(string json, string rootTagName = "root", string arrayTagName = "")
        {
            try
            {
                var builder = new StringBuilder();
                var arrayTag = String.IsNullOrEmpty(arrayTagName) ? "root" : arrayTagName;
                var rootTag = String.IsNullOrEmpty(rootTagName) ? "root" : rootTagName;
                var result = builder.Append("{\"").Append(arrayTag).Append('"').Append(':').Append(json).Append('}').ToString();
                return JsonConvert.DeserializeXNode(result, rootTag, true);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
