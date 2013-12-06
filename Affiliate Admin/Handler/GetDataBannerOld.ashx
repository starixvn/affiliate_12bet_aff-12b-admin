<%@ WebHandler Language="C#" Class="GetDataBannerOld" %>

using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
public class GetDataBannerOld : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string category = (context.Request["cate"].Equals("")) ? null : context.Request["cate"];
            string lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
            string currency = (context.Request["currency"].Equals("")) ? null : context.Request["currency"];
            string type = (context.Request["type"].Equals("")) ? null : context.Request["type"];
            string size = (context.Request["size"].Equals("")) ? null : context.Request["size"];
            int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
            int pageSize = (context.Request["PageSize"].Equals("")) ? 20 : CastAide.AsInt32(context.Request["PageSize"]);
            int minNo = 0, maxNo = 0, totalPage = 0, totalBanner = 0;
            StringBuilder result = new StringBuilder();
            string content = GenerateHTML.Banner.GetBannerListOld(category, lang, type, size, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize);
            result.Append(minNo.ToString());
            result.Append("$$$");
            result.Append(maxNo.ToString());
            result.Append("$$$");
            result.Append(content);

            context.Response.Write(result.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
