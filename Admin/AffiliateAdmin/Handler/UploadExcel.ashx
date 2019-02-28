<%@ WebHandler Language="C#" Class="UploadExcel" %>

using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class UploadExcel : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new UploadExcelService().Upload(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}