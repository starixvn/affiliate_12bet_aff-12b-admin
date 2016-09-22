<%@ WebHandler Language="C#" Class="UpdateDepartment" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UpdateDepartment : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateDepartmentService().Update(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}