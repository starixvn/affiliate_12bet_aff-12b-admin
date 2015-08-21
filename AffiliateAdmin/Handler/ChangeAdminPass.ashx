<%@ WebHandler Language="C#" Class="ChangeAdminPass" %>

using System;
using System.Web;
using System.Web.SessionState;
using DNAide.Web;
using DNAide.Core;
public class ChangeAdminPass : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context = IBCHandler.InitializeCulture(context);
        try
        {
            string strOldPass = context.Request["OldPass"];
            string strNewPass = context.Request["NewPassword"];
            int intCustId = CastAide.AsInt32(context.Request["custid"]);
            int intErrCode = DataHandler.Account.ChangePass(AppSettings.Session.CustID, strOldPass, strNewPass, Common.GetLangChar());
            if (intErrCode == 0)
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateSucess('" + Resources.Lang.UpdateSucc + "');parent.location.href='../Home/Home.aspx';</script>");///Alert.aspx?user=" + Resources.Lang.Password + "&code=0&link=UserInfo/AffiliateList.aspx?status=approved';</script>");
            }
            else//alert error
            {
                context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + Resources.Lang.OldPassIncorrect + "\");</script>");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("<script type='text/javascript'>parent.UpdateFail(\"" + ex.Message + "\");</script>");
            Fanex.Logging.LogService.Error(ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
