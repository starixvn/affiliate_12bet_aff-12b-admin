using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Affiliate.Infrastructure.Helpers;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Common;

namespace AffiliateAdmin.Business.Services.Auth
{
    public class LoginService
    {
        public void Login(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);

            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write(Resources.Lang.Err500);
                return;
            }

            string _username = context.Request.Form["un"];
            string _password = context.Request.Form["pw"];
            string errmsg = "";
            int custid = 0;
            int roleid = 0;
            string isok = "";
            string lang = "";
            //1 : invalid code
            //2 : wrong password
            //0 : successful
            try
            {
                int errcode = DataHandler.Login.ExecLogin(ref _username, _password, ref errmsg, context, ref custid, ref roleid, lang);
                var authRepository = new AuthRepository();
                ISessionService sessionService = new SessionService();
                var permissionList = custid > 0 ? authRepository.GetPermissionList(custid) : new List<UserPermission>();
                if (errcode == 0 && custid > 0)
                {
                    if (permissionList.Any())
                    {
                        var authTicket = new FormsAuthenticationTicket(_username, true, 20);
                        var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        {
                            HttpOnly = true,
                            Secure = FormsAuthentication.RequireSSL,
                            Path = FormsAuthentication.FormsCookiePath,
                            Domain = FormsAuthentication.CookieDomain,
                            Expires = authTicket.Expiration
                        };
                        var onUserId = DataHandler.Login.LoginOnUser(context);
                        AppSettings.Session.CustID = custid;
                        AppSettings.Session.OnUser = onUserId;
                        AppSettings.Session.RoleID = roleid;
                        AppSettings.Session.UserName = _username;
                        AppSettings.Session.RoleAccess = DataHandler.Login.AssignRoles(custid);
                        sessionService.UserPermissionList = permissionList;
                        sessionService.UserId = custid;
                        sessionService.OnUserId = onUserId;

                        isok = "OK";
                        context.Response.SetCookie(cookie);
                    }
                    else
                    {
                        isok = "Your account is not configured to access any functions! Please contact your administrator";
                    }
                }
                else
                {
                    isok = errmsg;
                }

                context.Response.Write(isok);
            }
            catch (Exception ex)
            {
                AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);

                if (Common.IsUnderMaintenance())
                {
                    context.Response.Write("UM");
                    return;
                }

                context.Response.Write(ex.Message);
            }
        }
    }
}