<%@ WebHandler Language="C#" Class="AddUser" %>

using System;
using System.Web;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Web.SessionState;
public class AddUser : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        string username = (string.IsNullOrEmpty(context.Request.Form["us"])) ? "" : context.Request.Form["us"];
        string password = (string.IsNullOrEmpty(context.Request.Form["pw"])) ? "" : context.Request.Form["pw"];
        string firstname=(string.IsNullOrEmpty(context.Request.Form["fn"])) ? "" : context.Request.Form["fn"];
        string lastname = (string.IsNullOrEmpty(context.Request.Form["ln"])) ? "" : context.Request.Form["ln"];
        int groupid = (string.IsNullOrEmpty(context.Request.Form["gpid"])) ? 0 :CastAide.AsInt32(context.Request.Form["gpid"]);
        int isexist = (string.IsNullOrEmpty(context.Request.Form["isex"])) ? 0 : CastAide.AsInt32(context.Request.Form["isex"]);
        string errmsg="";
        int errcode = 0;
        if (isexist == 0)
        {
            errcode = DataHandler.UserControl.AddNewUser(firstname, lastname,"AA" + username, password, groupid, ref errmsg);
        }
        else
        {
            errcode = DataHandler.UserControl.AddNewUserExist(username, groupid, ref errmsg);
        }
        if (errcode == 0)
        {
            context.Response.Write("Ok");
        }
        else
        {
            context.Response.Write(errmsg);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
