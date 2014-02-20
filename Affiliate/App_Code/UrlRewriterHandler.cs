using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.SessionState;
using DNAide.Core;

public class BetterUrlRewriter : IHttpHandler, IRequiresSessionState
{    
    const string ORIGINAL_PATHINFO = "UrlRewriterOriginalPathInfo";    
    const string ORIGINAL_QUERIES = "UrlRewriterOriginalQueries";     
    
    public void ProcessRequest(HttpContext context)    
    {
        // Record the original request PathInfo and QueryString information to handle graceful postbacks        
        context.Items[ORIGINAL_PATHINFO] = context.Request.PathInfo;
        context.Items[ORIGINAL_QUERIES] = context.Request.QueryString.ToString();

        // Map the friendly URL to the back-end one.    .        
        String strVirtualPath = "";
        String strQueryString = "";
        int showLoading = 0;
        MapFriendlyUrl(context, out strVirtualPath, out strQueryString, out showLoading);
        if (showLoading == 1)
        {
            // show loading text in case of waiting for data returned             
            context.Response.StatusCode = 300;
            context.Response.ContentType = "text/plain";
            context.Response.Write("LOADING ...");
            context.Response.End();
            return;
        }

        if (strVirtualPath.Length > 0)
        {
            foreach (string strOriginalQuery in context.Request.QueryString.Keys)
            {
                // To ensure that any query strings passed in the original request are preserved, we append these                 
                // to the new query string now, taking care not to add any keys which have been rewritten during the handler..                
                if (strQueryString.ToLower().IndexOf(strOriginalQuery.ToLower() + "=") < 0)
                {
                    strQueryString += string.Format("{0}{1}={2}", ((strQueryString.Length > 0) ? "&" : ""), strOriginalQuery, context.Request.QueryString[strOriginalQuery]);
                }
            }

            // Apply the required query strings to the request            
            context.RewritePath(context.Request.Path, string.Empty, strQueryString);
            // Now get a page handler for the ASPX page required, using this context.              
            Page aspxHandler = (Page)PageParser.GetCompiledPageInstance(strVirtualPath, context.Server.MapPath(strVirtualPath), context);
            // Execute the handler..            
            aspxHandler.PreRenderComplete += new EventHandler(AspxPage_PreRenderComplete);
            aspxHandler.ProcessRequest(context);
        }
        else
        {
            // No mapping was found - emit a 404 response.             
            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/plain";
            context.Response.Write("Page Not Found");
            context.Response.End();
        }

    }     
    
    void MapFriendlyUrl(HttpContext context, out String strVirtualPath, out String strQueryString, out int showLoading)    
    {
        strVirtualPath = ""; strQueryString = ""; showLoading = 0;
            
        // TODO: This routine should examine the context.Request properties and implement        
        //       an appropriate mapping system.        
        //        
        //       Set strVirtualPath to the virtual path of the target aspx page.        
        //       Set strQueryString to any query strings required for the page.     

        string strPath = context.Request.RawUrl;
        if (strPath.ToLower().IndexOf("index.html") >= 0)
        {

            List<string> listParam = new List<string>(strPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries));

            while (listParam.Count > 0)
            {
                if (CastAide.AsInt32(listParam[0], 0) == 0) //Remove virtual directory
                    listParam.RemoveAt(0);
                else
                    break;
            }

            // Example hard coded mapping of "FriendlyPage.html" to "UnfriendlyPage.aspx"                         
            strVirtualPath = "~/UserInfo/MediaLink.aspx";

            //Generate list param here            
            if (listParam.Count == 4)//Has tracking code                
                strQueryString = "AffId=" + listParam[0] + "&PageId=" + listParam[1] + "&Lang=" + listParam[2];
            else if (listParam.Count == 5)//Has tracking code                
                strQueryString = "AffId=" + listParam[0] + "&TrackId=" + listParam[1] + "&PageId=" + listParam[2] + "&Lang=" + listParam[3];
        }
        else if (strPath.ToLower().IndexOf("blank.html") >= 0)
        {
            showLoading = 1;
        }
    }     
    
    void AspxPage_PreRenderComplete(object sender, EventArgs e)    
    {        
        // We need to rewrite the path replacing the original tail and query strings..        
        // This happens AFTER the page has been loaded and setup but has the effect of ensuring        
        // postbacks to the page retain the original un-rewritten pages URL and queries.         
        
        HttpContext.Current.RewritePath(HttpContext.Current.Request.Path,                                        
        HttpContext.Current.Items[ORIGINAL_PATHINFO].ToString(),                                        
        HttpContext.Current.Items[ORIGINAL_QUERIES].ToString());    
    }     
    
    public bool IsReusable    
    {        
        get        
        {            
            return true;        
        }    
    }
}
