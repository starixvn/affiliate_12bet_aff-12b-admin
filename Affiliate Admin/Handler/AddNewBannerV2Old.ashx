<%@ WebHandler Language="C#" Class="AddNewBannerV2Old" %>
using System;
using System.Web;
using System.Web.SessionState;
public class AddNewBannerV2Old : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/html";           
            string newfilename = context.Request.Files[0].FileName;            
            string category = context.Request.Params["slCategory"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string imageType = context.Request.Params["slImageType"];
            string size = context.Request.Params["slSize"];
            
            string ext = string.Empty;
            if (!Common.IsValidExtension(newfilename, ref ext))
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Resources.Lang.errFileExtension + "\");</script>");
                return;
            }

               
            string err =string.Empty;
            int errCode=0;
            int bannerID = DataHandler.Banner.InsBannerOld(ext, category, language, currency, imageType, size, ref errCode, ref err);
            
            if (!string.IsNullOrEmpty(err))
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                return;
            }

            string path = context.Server.MapPath("../Banner//");

            Common.UploadFile(context, category, path, ref errCode, bannerID.ToString());
            
            if (errCode == 1)
            {
                DataHandler.Banner.DelDataBannerOld(bannerID.ToString());
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFileEmpty + "');</script>");
                return;
            }
            else if (errCode == 2)
            {
                DataHandler.Banner.DelDataBannerOld(bannerID.ToString());
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFile + "');</script>");
                return;
            }
            else if (errCode == 3)
            {
                DataHandler.Banner.DelDataBannerOld(bannerID.ToString());
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgSizeFile + "');</script>");
                return;
            }

            context.Response.Write("<script type=\"text/javascript\">alert('" + Resources.Lang.AddBannerSuccessful + "');parent.Cancelfunction();</script>");
        }
              
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
