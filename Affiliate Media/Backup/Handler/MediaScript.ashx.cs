using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;

namespace AffiliateMedia
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class MediaScript : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/x-javascript";
            
            StringBuilder sbContent = ReadFile(context.Server.MapPath("~/Scripts/affiliate.text.js"));
            sbContent = sbContent.Replace("{requesttime}", ConfigurationManager.AppSettings["RequestTime"]);
            sbContent = sbContent.Replace("{url}", ConfigurationManager.AppSettings["BannerUrl"]);
            sbContent = sbContent.Replace("{affid}", context.Request.QueryString["textid"]);

            context.Response.Write(sbContent.ToString());
        }
        public StringBuilder ReadFile(string fileScript)
        {
            StringBuilder sbContent = new StringBuilder();
            if (File.Exists(fileScript))
            {
                using (var fileReader = File.OpenText(fileScript))
                {
                    sbContent.Append(fileReader.ReadToEnd());
                    fileReader.Close();
                }
            }
            return sbContent;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
