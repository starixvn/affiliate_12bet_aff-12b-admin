using System.Text;
using System.Web;
using AffiliateAdmin.Business.Constants;

namespace AffiliateAdmin.Business.Services
{
    public class DeleteMailListService
    {
        public void DeleteMailList(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string midlist = context.Request.Params["params"].Trim();
                if (string.IsNullOrEmpty(midlist))
                {
                    context.Response.Write("Null data is sent to server");
                    return;
                }
                bool isHost = Common.HasHostPermission(AppSettings.Session.CustID, PermissionNames.HotNews);
                if (isHost)
                {
                    //Drop the comma at the end of message
                    midlist = midlist.Substring(0, midlist.Length - 1);
                    StringBuilder result = new StringBuilder();
                    result.Append(DataHandler.EmailHistory.DeleteMailHistory(midlist));
                    context.Response.Write(result.ToString());
                }
                else
                {
                    context.Response.Write(Resources.Lang.UpdateRolePolicyErr);
                }
            }
        }
    }
}