<%@ Application Language="C#" %>
<%@ Import Namespace="Fanex.Logging" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
      /*  CacheUtil.GetLatestHotNews(null);*/
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
</script>
