using System.Web;
using AffiliateAdmin.Business.Constants;

namespace AffiliateAdmin.Business.Services
{
    public class ActiveMemberService
    {
        public void ActiveMember(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            if (!Common.HasHostPermission(AppSettings.Session.CustID, PermissionNames.ActiveMembers))
            {
                context.Response.Write("errMsg, You don't have permission to perform this action.");
                return;
            }

            string errUserNames = string.Empty;
            string errMsg = string.Empty;

            string usernames = context.Request.Form["usernames"].ToString();

            if (!string.IsNullOrEmpty(usernames))
            {
                Active12BetAccount.Active12BetUsernames(usernames, ref errUserNames, ref errMsg);

                if (string.IsNullOrEmpty(errUserNames) && string.IsNullOrEmpty(errMsg))
                {
                    context.Response.Write("Ok");
                }
                else
                {
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        context.Response.Write("errMsg," + errMsg);
                    }
                    else
                    {
                        context.Response.Write("errUserNames," + errUserNames);
                    }
                }
            }
        }
    }
}