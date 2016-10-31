using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;

namespace AffiliateAdmin.Business.Services
{
    public class CommGroupAssignService
    {
        public void CommGroupAssign(HttpContext context)
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

                string[] arr = context.Request.Params["params"].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    DbResult result = DataHandler.GroupSetting.CommGroupAssign(Convert.ToInt32(arr[0]), arr[1], context.Request.UserHostAddress);
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