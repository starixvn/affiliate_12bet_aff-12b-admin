using System.Web;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateStatusService
    {
        public void Update(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int custid = (string.IsNullOrEmpty(context.Request.Form["cstid"])) ? 0 : CastAide.AsInt32(context.Request.Form["cstid"]);
            bool isclosed = (string.IsNullOrEmpty(context.Request.Form["iscls"])) ? true : CastAide.AsBoolean(context.Request.Form["iscls"]);
            isclosed = (isclosed == true) ? false : true;
            if (custid == 0)
            {
                context.Response.Write("Fail");
            }
            else
            {
                string errmsg = "";
                int errcode = DataHandler.UserControl.UpdateStatusAccount(custid, isclosed, ref errmsg);
                if (errcode == 0)
                {
                    context.Response.Write("Ok");
                }
                else
                {
                    context.Response.Write(errmsg);
                }
            }
        }
    }
}