using Fanex.Affiliate.Admin.Core.DTO;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Extensions
{
    public static class ErrorExtension
    {
        public static JsonNetResult GenerateErrorResult(this ModelStateDictionary modelState)
        {
            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("<br />", modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)))
            };
        }
    }
}
