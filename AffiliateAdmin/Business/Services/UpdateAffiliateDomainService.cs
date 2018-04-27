using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateAffiliateDomainService
    {
        public void Update(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write(Resources.Lang.Err500);
                return;
            }

            string action = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

            int affDomainId = CastAide.AsInt32(context.Request.Form["affDomainId"]);
            string affDomainName = CastAide.AsString(context.Request.Form["affDomainName"]);
            var isSSL = CastAide.AsBoolean(context.Request.Form["ssl"], false);
            DbResult dbr = null;

            switch (action)
            {
                case "Edit":
                    {
                        dbr = DataHandler.Domain.UpdateAffDomain(affDomainId, affDomainName, AppSettings.Session.CustID, isSSL);
                        break;
                    }
                case "Add":
                    {
                        dbr = DataHandler.Domain.AddAffDomain(affDomainName, AppSettings.Session.CustID, isSSL);
                        break;
                    }
                case "Del":
                    {
                        dbr = DataHandler.Domain.DelAffDomain(affDomainId, affDomainName, AppSettings.Session.CustID);
                        break;
                    }
            }

            if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(dbr.Outputs["errmsg"].ToString());
            }
        }
    }
}