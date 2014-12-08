<%@ Application Language="C#" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Security.Principal"%>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Sunplus.Agent.Utilities" %>
<%@ Import Namespace="Fanex.Logging" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup            
        Sunplus.Agent.Configurations.AppSettings.Configuration.Load();
        // Code that runs on application startup
        Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();    
        
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError().GetBaseException();
        if (ex is HttpException)
        {
            return;
        }

        string message = ex.Message +
                        "\r\nSOURCE: " + ex.Source +
                        "\r\nFORM: " + Request.Form.ToString() +
                        "\r\nQUERYSTRING: " + Request.QueryString.ToString() +
                        "\r\nTARGETSITE: " + ex.TargetSite +
                        "\r\nSTACKTRACE: " + ex.StackTrace;
        //LogService.Error("12BET Admin " + System.Environment.NewLine + message);
        DNAide.Web.DNAideLog.Write(message);
    }   
    
    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_AcquireRequestState(object sender, EventArgs e)
    {        
         try
        {           

            if (!User.Identity.IsAuthenticated || Request.RawUrl.IndexOf(".aspx") == -1 || Session["RoleAccess"] == null)
                return;
            
            
            string _accessPermission = "";
            _accessPermission = Session["RoleAccess"].ToString();
            if (_accessPermission.IndexOf("[All]", StringComparison.CurrentCultureIgnoreCase ) != -1) return;

            string url;
            if (AppSettings.VirtualDir)
            {
                url = Request.RawUrl.Substring(Request.RawUrl.IndexOf("/", 1) + 1);
            }
            else
            {
                url = Request.RawUrl.Substring(1);
            }
            
            url = url.Substring(0, url.IndexOf(".aspx") + 5);
            
            if (Context.Cache["AccessPermission"] == null)
                Common.InitAccessPermission();
            string pageRole = ((Dictionary<string, string>)Context.Cache["AccessPermission"]).ContainsKey(url) ? ((Dictionary<string, string>)Context.Cache["AccessPermission"])[url] : "";
            //HttpContext.Current.Response.Write("Raw url: " + Request.RawUrl.ToString() + "");
            if (string.Compare(pageRole, "[All]", true) == 0) return;
            if (_accessPermission.IndexOf(pageRole) == -1)
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }
        catch 
        {

        }        
    }
    private void OnRemoveRolePermissionCache(String k, Object v, CacheItemRemovedReason r)
    {
        //InitRolePermission();
    }   
</script>
