<%@ WebHandler Language="C#" Class="ChangeAdminPass" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class ChangeAdminPass : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        new ChangeAdminPassService().ChangeAdminPass(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}