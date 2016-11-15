using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;

namespace AffiliateAdmin.Business.Services
{
    public class DeleteMemberService
    {
        public void DeleteMember(HttpContext context)
        {
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write(Resources.Lang.Err500);
                return;
            }

            string ibccustid = context.Request.Params["ibccustid"];
            if (string.IsNullOrEmpty(ibccustid))
            {
                context.Response.Write(Resources.Lang.EmptyIBCCustID);
                return;
            }
            int id = Int32.Parse(ibccustid);
            context.Response.Write(DataHandler.Member.DeleteMember(id));
        }
    }
}