using Fanex.Affiliate.DataService.Importer.Jasper.Constants;
using System;
using System.Xml.Linq;

namespace Fanex.Affiliate.DataService.Importer.Jasper.Helpers
{
    public static class Validations
    {
        public static bool IsRequestSuccess(XElement status)
        {
            var isSuccess = string.Equals(Status.Success, status.Value, StringComparison.InvariantCultureIgnoreCase);

            return isSuccess;
        }
    }
}
