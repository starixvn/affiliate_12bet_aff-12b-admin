<%@ WebHandler Language="C#" Class="Favorite" %>

using System.Web.SessionState;
using System.Web;
using AffiliateAdmin.Business.Services;

public class Favorite : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new FavoriteService().Favorite(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}