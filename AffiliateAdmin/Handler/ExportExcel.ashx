<%@ WebHandler Language="C#" Class="ExportExcel" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class ExportExcel : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new ExportExcelService().ExportExcel(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}