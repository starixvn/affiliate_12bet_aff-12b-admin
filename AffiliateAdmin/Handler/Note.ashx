<%@ WebHandler Language="C#" Class="Note" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class Note : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new NoteService().Note(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}