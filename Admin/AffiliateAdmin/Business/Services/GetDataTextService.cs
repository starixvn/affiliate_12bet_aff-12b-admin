using System.Text;
using System.Web;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class GetDataTextService
    {
        public void GetDataText(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string category = (context.Request["cate"].Equals("")) ? null : context.Request["cate"];
                string lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
                string currency = (context.Request["currency"].Equals("")) ? null : context.Request["currency"];
                string deviceType = string.IsNullOrEmpty(context.Request["deviceType"]) ? "0" : context.Request["deviceType"];

                int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
                int pageSize = (context.Request["PageSize"].Equals("")) ? 20 : CastAide.AsInt32(context.Request["PageSize"]);
                int minNo = 0, maxNo = 0, totalPage = 0, totalBanner = 0;
                StringBuilder result = new StringBuilder();
                string content = GenerateHTML.Text.GetListText(category, lang, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, deviceType);
                result.Append(minNo.ToString());
                result.Append("$$$");
                result.Append(maxNo.ToString());
                result.Append("$$$");
                result.Append(content);

                context.Response.Write(result.ToString());
            }
        }
    }
}