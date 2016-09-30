<%@ WebHandler Language="C#" Class="UpdateSecurityQuestion" %>

using System.Web;
using System.Web.SessionState;
using AffiliateUser.Business.Services;

public class UpdateSecurityQuestion : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UpdateSecurityQuestionService().UpdateSecurityQuestion(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}