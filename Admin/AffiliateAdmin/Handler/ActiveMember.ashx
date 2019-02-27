<%@ WebHandler Language="C#" Class="ActiveMember" %>
using System.Web.SessionState;
using System.Web;
using AffiliateAdmin.Business.Services;

public class ActiveMember : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new ActiveMemberService().ActiveMember(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}