using System;
using System.Configuration;
using System.IO.Compression;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace AffiliateMedia.HttpModules
{
    public sealed class PreProcessingModule : IHttpModule
    {
        private static void ContextPostReleaseRequestState(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            if (new Regex(@"(.*?)\.(jpg|jpeg|png|gif|swf)$").IsMatch(context.Request.Path.ToLower()))
            {
                context.Response.Cache.SetExpires(DateTime.Now.AddYears(-10));
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
            
        }

        #region IHttpModule Members

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += ContextPostReleaseRequestState;
        }

        #endregion
    }
}
