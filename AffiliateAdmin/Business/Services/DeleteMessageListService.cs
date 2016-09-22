using System.Text;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class DeleteMessageListService
    {
        public void DeleteMessageList(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";

                string msgidlist = context.Request.Params["params"].Trim();
                if (string.IsNullOrEmpty(msgidlist))
                {
                    context.Response.Write("Null data is sent to server");
                    return;
                }
                //Drop a comma at the end of message
                msgidlist = msgidlist.Substring(0, msgidlist.Length - 1);

                StringBuilder result = new StringBuilder();
                result.Append(DataHandler.HotNews.DeleteHotNews(msgidlist));
                context.Response.Write(result.ToString());
            }
        }
    }
}