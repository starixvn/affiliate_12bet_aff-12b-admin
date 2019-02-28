<%@ WebHandler Language="C#" Class="DeleteMember" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class DeleteMember : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new DeleteMemberService().DeleteMember(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}