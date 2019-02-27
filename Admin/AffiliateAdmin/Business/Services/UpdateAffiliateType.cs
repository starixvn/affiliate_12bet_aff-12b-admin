using System;
using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateAffiliateType
    {
        public void Update(HttpContext context)
        {
            context = IBCHandler.InitializeCulture(context);
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("1@" + Resources.Lang.Err500);
                return;
            }

            try
            {
                int afftype = CastAide.AsInt32(context.Request["afftype"], 2);

                DbResult result = DataHandler.Affiliate.AffUpdateAffType(AppSettings.Session.AffId, afftype);

                if (CastAide.AsInt32(result.Outputs["errcode"], 0) == 0)
                    context.Response.Write("0@" + Resources.Lang.UpdateAffTypeSuccessful);
                else
                    context.Response.Write("1@" + Resources.Lang.UpdateAffTypeFailed + result.Outputs["errcode"]);
            }
            catch (Exception ex)
            {
                context.Response.Write("1@" + ex.Message);
                AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
            }
        }
    }
}