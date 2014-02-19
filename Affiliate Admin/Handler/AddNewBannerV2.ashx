<%@ WebHandler Language="C#" Class="AddNewBannerV2" %>
using System;
using System.Web;
using System.Web.SessionState;
using System.Text;
using DNAide.Core;

using System.IO;
using System.Drawing;

public class AddNewBannerV2 : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/html";

            string newfilename = context.Request.Files[0].FileName;
            string inputfilename = context.Request.Params["inpFileName"];
            string category = context.Request.Params["slCategory"];
            string subCategory = context.Request.Params["slSubCategoryMedia"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string imageType = context.Request.Params["slImageType"];
            string size = context.Request.Params["slSize"];
            string affType = context.Request.Params["slAffType"];
            // ++ Added by Frank.Ngo on IRP 671 - 10 Jan 2012
            int type = CastAide.AsInt32(context.Request.Params["hidBannerType"], 0);
            // -- Added by Frank.Ngo on IRP 671 - 10 Jan 2012

            string ext = string.Empty;
            if (!string.IsNullOrEmpty(newfilename))
            {
                if (!Common.IsValidExtension(newfilename, ref ext))
                {
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter(\"" + Resources.Lang.errFileExtension + "\");</script>");
                    return;
                }
            }

            if (string.IsNullOrEmpty(inputfilename))
                inputfilename = newfilename;
            else
            {
                StringBuilder tempSB = new StringBuilder();
                int temp = inputfilename.LastIndexOf(".");
                if (temp > 0)
                    tempSB.Append(inputfilename.Substring(0, temp));
                else
                    tempSB.Append(inputfilename);

                tempSB.Append(".");
                tempSB.Append(ext);
                inputfilename = tempSB.ToString();
            }

            string fileName = string.Empty;
            string newFolder = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("CategoryMedia/");
            sb.Append(category);
            sb.Append("/");
            if (category == "Casino" || category == "Sportsbook")
            {
                sb.Append(subCategory);
                sb.Append("/");
                category = subCategory;
            }
            newFolder = sb.ToString();
            sb.Append(inputfilename);
            fileName = sb.ToString();

            string err = string.Empty;
            int errCode = 0;
            string path = context.Server.MapPath("../Banner//");
            string strDst = string.Empty;

            if (File.Exists(path + newFolder + "\\" + inputfilename))
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" +
                    Resources.Lang.ErrmsgFileExist +
                    "');</script>");
                return;
            }

            Common.UploadFileWithAutoDetectSize(context, newFolder, path, inputfilename, ref errCode, out size, ref strDst);
            if (errCode == 0)
            {
                int bannerID = DataHandler.Banner.InsBanner(fileName, category, language, currency, imageType, size, affType, ref errCode, ref err, type);
                if (errCode != 0)
                {
                    //Delete image uploaded
                    if (!string.IsNullOrEmpty(strDst))
                        Common.DeleteFile(strDst);
                    context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + err + "');</script>");
                    return;
                }
            }

            if (errCode == 1)
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFileEmpty + "');</script>");
                return;
            }
            else if (errCode == 2)
            {
                context.Response.Write("<script type=\"text/javascript\">parent.ErrAlter('" + Resources.Lang.ErrmsgFile + "');</script>");
                return;
            }
            else if (errCode == 3)
            {
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
