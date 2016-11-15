using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;

namespace AffiliateAdmin.Business.Services
{
    public class CommGroupDelService
    {
        public void CommGroupDel(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                try
                {
                    AntiForgeryHelper.ValidateXsrfToken(context);
                }
                catch
                {
                    context.Response.Write(Resources.Lang.Err500);
                    return;
                }

                string groupID = context.Request.Params["params"];

                if (string.IsNullOrEmpty(groupID))
                {
                    context.Response.Write("Invalid group ID");
                    return;
                }

                try
                {
                    DbResult result = DataHandler.GroupSetting.CommGroupDel(Convert.ToInt32(groupID), context.Request.UserHostAddress);
                    context.Response.Write(result.Outputs["errmsg"]);
                }
                catch (Exception e)
                {
                    context.Response.Write(e.ToString());
#if DEBUG
                    throw;

#else
                Fanex.Logging.LogService.Error(e.ToString());
#endif
                }
            }
        }
    }
}