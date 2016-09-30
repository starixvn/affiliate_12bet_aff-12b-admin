using System.Web;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Text
{
    public class DeleteTextService
    {
        public ResultInfo Delete(HttpContext context)
        {           
            string listIdBanner = context.Request.Params["params"];
            string result = DataHandler.Text.DelDataText(listIdBanner);

            return new ResultInfo { Message = result };
        }
    }
}