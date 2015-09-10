<%@ WebHandler Language="C#" Class="GetDataBanner" %>


using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using System.Data;

public class GetDataBanner : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string category = (context.Request["cate"].Equals("")) ? null : context.Request["cate"];
            string lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
            string currency = (context.Request["currency"].Equals("")) ? null : context.Request["currency"];
            string type = (context.Request["type"].Equals("")) ? null : context.Request["type"];
            string size = (context.Request["size"].Equals("")) ? null : context.Request["size"];
            int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
            int pageSize = CastAide.AsInt32(context.Request["pagesize"], 20);
            string trackingcode = string.IsNullOrEmpty(context.Request["TrackingCode"]) ? "" : context.Request["TrackingCode"];
            if (AppSettings.Session.TrackingCode.CompareTo(trackingcode.Trim()) ==0)
            {
                //context.Response.Write(GenerateHTML.Banner.GetBannerList(category, lang, type, size, currency, page,pageSize, AppSettings.Session.TrackingCode));
            }
            else
            {
                AppSettings.Session.TrackingCode = trackingcode;
                DataView dv = (CacheUtil.GetBannerTypes() as DataTable).DefaultView;
                /* Generate TestLinkList */
                dv.RowFilter = "type='Language'";
                DataTable dt = dv.ToTable();
                //context.Response.Write(GenerateHTML.Banner.GetBannerList(category, lang, type, size, currency, page,pageSize, trackingcode) + "$$$" + GenerateHTML.Banner.GetTextLink(trackingcode, dt));
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
