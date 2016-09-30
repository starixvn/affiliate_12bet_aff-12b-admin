using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateUserService
    {
        public void Update(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            int custid = (string.IsNullOrEmpty(context.Request.Form["custid"])) ? 0 : CastAide.AsInt32(context.Request.Form["custid"]);
            string fn = string.IsNullOrEmpty(context.Request.Form["fn"]) ? "" : context.Request.Form["fn"];
            string ln = string.IsNullOrEmpty(context.Request.Form["ln"]) ? "" : context.Request.Form["ln"];
            int depid = string.IsNullOrEmpty(context.Request.Form["depid"]) ? 0 : CastAide.AsInt32(context.Request.Form["depid"]);

            DbResult dbr = null;
            dbr = DataHandler.UserControl.UpdateUser(custid, fn, ln, depid);
            if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(dbr.Outputs["errmsg"]);
            }
        }
    }
}