using Newtonsoft.Json;
using System;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
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
        /// <returns></returns>
        public static XDocument ParseToXDocumentFromJson(string json, string rootTagName = "root")
        {
            try
            {
                var rootTag = String.IsNullOrEmpty(rootTagName) ? "root" : rootTagName;
                return JsonConvert.DeserializeXNode(json, rootTag, true);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
