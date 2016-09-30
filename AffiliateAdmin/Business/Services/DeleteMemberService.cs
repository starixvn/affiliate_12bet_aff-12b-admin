using System;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class DeleteMemberService
    {
        public void DeleteMember(HttpContext context)
        {
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