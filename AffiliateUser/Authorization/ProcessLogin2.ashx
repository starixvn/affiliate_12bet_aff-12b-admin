<%@ WebHandler Language="C#" Class="ProcessLogin2" %>
/* ---------------------------------------------------------------------------------- 
 * Desc: Authenticate login from Admin site
 * Writer: viet hoang 
 * Template: none
 * SPs: Aff_OnUserSelNetSessionID, Aff_AffiliateInternalLogin
 * ---------------------------------------------------------------------------------- */
using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Collections.Generic;
using Affiliate;
using DNAide.Utils;
using System.Data;
using DNAide.Web;
using DNAide.Core;
using DataHandler;

public class ProcessLogin2 : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context = AffHandler.InitializeCulture(context);
        context.Response.ContentType = "text/html";
        string _s = string.Empty;
        string _nl = Environment.NewLine;
        string _url = string.Empty;
        try
        {
            string _accId = context.Request["accId"];
            string _custName = context.Request["custName"];
            string _accKey = context.Request["accKey"];
            string _chalengeStr = context.Request["chalengeStr"];
            string _accOnId = context.Request["accOnId"];
            if (string.IsNullOrEmpty(_accId) || string.IsNullOrEmpty(_custName)
                || string.IsNullOrEmpty(_accKey) || string.IsNullOrEmpty(_chalengeStr) || string.IsNullOrEmpty(_accOnId))
            {
                context.Response.Write("<script type='text/javascript'>top.location.href='../Root.aspx';</script>");
                return;
            }
            AppSettings.Session.IsInternalSite = true;
            var dr = Auth.GetAdminNetSessionID(_accOnId, context);
            string _accNetSessionID = dr.Outputs["netsessionid"].ToString();
            if (string.Compare(_accKey, FormsAuthentication.HashPasswordForStoringInConfigFile(_accNetSessionID + _chalengeStr, "MD5"), false) != 0)
                throw new IBCException("Can not authenticate log-in, invalid ticket!");


            var dr2 = Auth.LoginAuthentication(CastAide.AsInt32(_accId), _custName, context);
            if (string.IsNullOrEmpty(dr2.Outputs["errmsg"].ToString()))
            {
                var afftype = (AffType)DNAide.Core.CastAide.AsInt32(dr2.Outputs["afftype"], 2);

                AppSettings.Session.AffType = afftype;
                AppSettings.Session.CustID = CastAide.AsInt32(dr2.Outputs["custid"], -1);
                AppSettings.Session.OnUser = DataHandler.Auth.LoginOnuser(context, CastAide.AsInt32(_accId) * -1);
                AppSettings.Session.UserName = _custName.ToUpper() + " : [" + dr2.Outputs["adminusername"].ToString() + "]";
                AppSettings.Session.FirstName = DNAide.Core.CastAide.AsString(dr2.Outputs["firstname"]);
                AppSettings.Session.LastName = DNAide.Core.CastAide.AsString(dr2.Outputs["lastname"]);
                AppSettings.Session.Email = DNAide.Core.CastAide.AsString(dr2.Outputs["email"]);
                AppSettings.Session.HasSecurityQuestion = true;
                AppSettings.Session.HasDailyDetail = DNAide.Core.CastAide.AsBoolean(dr2.Outputs["hasdailydetail"]);
                AppSettings.Session.PreferLang = DNAide.Core.CastAide.AsString(dr2.Outputs["preferlang"], "en-US");
                AppSettings.Session.IsCasino = DNAide.Core.CastAide.AsBoolean(dr2.Outputs["iscasino"], false);

                if (AppSettings.Session.Language == null)
                    AppSettings.Session.Language = "en-US";

                _s = "100:OK";
                //   AppSettings.Session.FirsTimeLoginSucc = true;
                AppSettings.Session.CommGroupName = DNAide.Core.CastAide.AsString(dr2.Outputs["commgroupname"]);

                context.Response.Cookies["Username"].Value = _custName.ToUpper();
                context.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                _s = "102:" + dr2.Outputs["errmsg"].ToString();
            }

            _url = FormsAuthentication.DefaultUrl;
        }
        catch (IBCException ex)
        {
            context.Response.Write("<body><script type='text/javascript'>alert('" + ex.Message.Replace("'", string.Empty) + "');location.href='" + Common.RootURL() + "';</script></body>");
            return;
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            _s = "102:" + ex.Message;
#if DEBUG
            throw;
#endif
        }
        context.Response.Redirect(_url);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
