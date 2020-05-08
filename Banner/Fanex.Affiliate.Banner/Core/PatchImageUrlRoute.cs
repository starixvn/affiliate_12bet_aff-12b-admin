using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fanex.Affiliate.Banner.Core
{
    public sealed class PatchImageUrlRoute : RouteBase
    {
        private readonly string Folder;
        private readonly IEnumerable<string> SupportedExtension;

        public PatchImageUrlRoute(string categoryName, IEnumerable<string> fileExtension)
        {
            Folder = categoryName;
            SupportedExtension = fileExtension;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (IsNeedToChangePath(httpContext))
            {
                var fileName = Path.GetFileName(httpContext.Request.FilePath);
                var path = $"~/{Folder}/{fileName}";
                return new RouteData(this, new StaticFileRouteHandler(path));
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }

        private bool IsNeedToChangePath(HttpContextBase httpContext)
        {
            var isNeedChangeFilePath = false;
            var path = httpContext.Request.CurrentExecutionFilePath;
            var extension = httpContext.Request.CurrentExecutionFilePathExtension;
            var fileAndFolders = path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrWhiteSpace(extension)
                && fileAndFolders.Count() > 2
                && SupportedExtension.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase))
                && fileAndFolders[0].Equals(Folder, StringComparison.OrdinalIgnoreCase)) 
            {
                var currentFilePath = httpContext.Request.PhysicalPath;
                if (!File.Exists(currentFilePath))
                {
                    isNeedChangeFilePath = true;
                }
            }

            return isNeedChangeFilePath;
        }
    }

    internal class StaticFileRouteHandler : IRouteHandler
    {
        private readonly string Path;
        public StaticFileRouteHandler(string path)
        {
            Path = path;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            requestContext.HttpContext.RewritePath(Path);
            var path = requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath.Remove(0,1);
            HttpContext.Current.Server.Transfer(path);
            return null;
        }
    }
}