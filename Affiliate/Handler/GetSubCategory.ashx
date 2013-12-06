<%@ WebHandler Language="C#" Class="GetSubCategory" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using DNAide.Data;
using System.Data;

public class GetSubCategory : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string category = context.Request["category"];
            string activeValue = context.Request["activeValue"];

            DataView dv = (CacheUtil.GetBannerTypes() as DataTable).DefaultView;
            string subCategory = "CategoryMedia" + category;
            context.Response.Write(Common.GetDataOptionBanner(dv, "slSubCategoryMedia", subCategory,
                activeValue=="none" ? string.Empty : activeValue, true));
        }

    }
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}
