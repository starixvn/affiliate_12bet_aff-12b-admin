using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for SiteDomain
/// </summary>
public class HttpContextAnalysis
{
    public string DomainName{ get; set; }
    
    public static string GetDomain(HttpContext context)
    {
        return context.Request.Url.Host;
    }

    public static int GetPort(HttpContext context)
    {
        return context.Request.Url.Port;
    }
}