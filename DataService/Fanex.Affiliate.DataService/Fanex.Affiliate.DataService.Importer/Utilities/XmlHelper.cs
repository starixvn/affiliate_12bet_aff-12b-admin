using System;
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
    }
}
