using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;

public partial class UserInfo_MediaLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["AffId"] != null && Request["PageId"] != null && Request["Lang"] != null)
        {
            int affId = int.Parse(Request["AffId"].ToString());
            string track = (Request["TrackId"] != null ? Request["TrackId"].ToString() : null);
            string pageId = Request["PageId"].ToString().ToLower();
            string lang = Request["Lang"].ToString();

            //string redirectPage = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : string.Empty;
            string redirectPage = string.Empty;
            string affDomain = Request.Url.Host.ToLower() + Request.RawUrl.ToLower();
            Int64 clickID = 0;
            int errCode = 0;
            string errMsg = string.Empty;

            //save user's info here to database
            DataHandler.TrackingSystem.InsertStatistic(affId, track, pageId, lang, Request, affDomain, out redirectPage, out clickID, ref errCode, ref errMsg);
            
            if (errCode != 0)
            {
                DNAide.Web.DNAideLog.Write(errMsg);
                Response.Redirect("http://www.12bet.com");
                return;
            }
            //remember write log when error occur and redirect to default page
            Response.Redirect(redirectPage);
        }
    }
}