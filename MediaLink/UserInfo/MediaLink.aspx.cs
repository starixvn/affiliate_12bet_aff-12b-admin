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
            string pageId = Request["PageId"].ToString();
            string lang = Request["Lang"].ToString();

            //string redirectPage = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : string.Empty;
            string redirectPage = string.Empty;
            Int64 clickID = 0;
            //save user's info here to database
            DataHandler.TrackingSystem.InsertStatistic(affId, track, pageId, lang, Request, out redirectPage, out clickID);
            //Get click id here
            TemplateHandler.Assign(ref redirectPage, "custid", affId);
            TemplateHandler.Assign(ref redirectPage, "target", pageId);
            TemplateHandler.Assign(ref redirectPage, "lang", lang);
            TemplateHandler.Assign(ref redirectPage, "clickid", clickID);//For clickId
            //redirectPage = redirectPage.Replace("{clickid}/", string.Empty);//for test without clickid
            redirectPage = redirectPage.Replace("{/TrackingCode}", (track == null ? string.Empty : "/" + track));
            //build url and redirect user
            Response.Redirect(redirectPage);
        }
    }
}