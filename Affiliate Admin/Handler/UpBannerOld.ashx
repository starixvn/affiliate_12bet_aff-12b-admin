<%@ WebHandler Language="C#" Class="UpBannerOld" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
public class UpBannerOld : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {

            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/html";
            string idbanner = context.Request.Params["hdBannerID"];
            string newfilename = context.Request.Files[0].FileName;
            string oldfilename = context.Request.Params["hdOldFileName"];
            string category = context.Request.Params["slCategory"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string imageType = context.Request.Params["slImageType"];
            string size = context.Request.Params["slSize"];
            bool needToUploadFile = false;

            //Save DB Info
            if (!string.IsNullOrEmpty(newfilename))
            {
                needToUploadFile = true;
                string ext = string.Empty;
                if (!Common.IsValidExtension(newfilename, ref ext))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Resources.Lang.errFileExtension + "\");</script>");
                    return;
                }

                newfilename = category + "/" + idbanner + "." + ext;
            }
            
            newfilename = string.IsNullOrEmpty(newfilename) ? oldfilename : newfilename;
            string oldCategory = oldfilename.Substring(0, oldfilename.IndexOf("/"));
            bool changeCategory = !oldCategory.Equals(category);
            string path = context.Server.MapPath("../Banner//");
            
            //Upload new file
            if (needToUploadFile)
            { 
                int errcode = 0;
                Common.UploadFile(context, category, path, ref errcode, idbanner);

                if (errcode != 0)
                {
                    if (changeCategory)
                    {
                        if (File.Exists(path + oldfilename))
                        {
                            if (!Directory.Exists(path + category))
                                Directory.CreateDirectory(path + category);
                            File.Move(path + oldfilename, path + newfilename);
                        }
                    }

                    if (errcode == 1)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFileEmpty + "');</script>");
                        return;
                    }
                    else if (errcode == 2)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFile + "');</script>");
                        return;
                    }
                    else if (errcode == 3)
                    {
                        context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgSizeFile + "');</script>");
                        return;
                    }
                }
                else
                {
                    string tempOld = path + oldfilename;
                    string tempNew = path + newfilename;
                    if (!tempNew.Equals(tempOld))
                        Common.DeleteFile(path + oldfilename);
                }
            }
            else if (changeCategory)
            {
                if (File.Exists(path + oldfilename))
                {
                    if (!Directory.Exists(path + category))
                        Directory.CreateDirectory(path + category);
                    File.Move(path + oldfilename, path + newfilename);
                }
            }

            string err = DataHandler.Banner.UpdBannerOld(Convert.ToInt32(idbanner), newfilename, category, language, currency, imageType, size);
            if (!string.IsNullOrEmpty(err))
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                return;
            }
            
            context.Response.Write("<script type=\"text/javascript\">parent.CancelEdit();</script>");
        }
              
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
