using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class Update12BetDomainService
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

            int _12BetDomainId = CastAide.AsInt32(context.Request.Form["_12BetDomainId"]);
            string _12BetDomainName = CastAide.AsString(context.Request.Form["_12BetDomainName"]);
            bool ssl = CastAide.AsBoolean(context.Request.Form["ssl"]);
            DbResult dbr = null;

            switch (action)
            {
                case "Edit":
                    {
                        dbr = DataHandler.Domain.Update12BetDomain(_12BetDomainId, _12BetDomainName, ssl, AppSettings.Session.CustID);
                        break;
                    }
                case "Add":
                    {
                        dbr = DataHandler.Domain.Add12BetDomain(_12BetDomainName, ssl, AppSettings.Session.CustID);
                        break;
                    }
                case "Del":
                    {
                        dbr = DataHandler.Domain.Del12BetDomain(_12BetDomainId, _12BetDomainName, AppSettings.Session.CustID);
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