<%@ WebHandler Language="C#" Class="GetStatisticsDetail_New" %>

using System;
using System.Web;
using System.Web.SessionState;
using AffiliateAdmin.Business.Services;

public class GetStatisticsDetail_New : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        new GetStatisticsDetailNewService().GetStatisticsDetail(context);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}