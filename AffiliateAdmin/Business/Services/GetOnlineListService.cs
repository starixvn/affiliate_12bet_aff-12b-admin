using System.Web;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class GetOnlineListService
    {
        public void GetOnlineList(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string param = context.Request["params"];
            int page = CastAide.AsInt32(param.Split(',')[0]);
            int pagesize = CastAide.AsInt32(param.Split(',')[1]);
            string ret = OnlineList.GenerateOnlineListTable(page, pagesize);
            context.Response.Write(ret);
        }
    }
}