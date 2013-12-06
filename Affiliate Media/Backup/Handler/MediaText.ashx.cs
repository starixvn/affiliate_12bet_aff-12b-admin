using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DNAide.Core;
using DNAide.Data;
using System.Data;
using System.Collections;
using System.Configuration;

namespace AffiliateMedia
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class MediaText : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.QueryString["textid"] != null)
            {
                int BannerId = Convert.ToInt32(context.Request.QueryString["textid"].ToString());
                DbResult result = GetTextDetail(BannerId);
                if (result.ResultSet.Tables.Count > 0)
                {
                    DataTable dtbResult = result.ResultSet.Tables[0];
                    context.Response.Write("document.getElementById('_affMediaText').innerHTML ='" + HttpUtility.HtmlDecode( dtbResult.Rows[0]["Message"].ToString()).Replace("\r\n","<br/>") + "';");
                }
            }
        }
        public static DbResult GetTextDetail(int textId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("textid", textId);
            return DbHelper.Execute(ConfigurationManager.AppSettings["sp_AffTextSelDetail"], hsParam, "affiliate_12bet");
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
