using System.Web;
using Affiliate.Infrastructure.Helpers;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class FavoriteService
    {
        public void Favorite(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                AntiForgeryHelper.ValidateXsrfToken(context);
            }
            catch
            {
                context.Response.Write("1;" + Resources.Lang.Err500);
                return;
            }

            int AffId = CastAide.AsInt32(context.Request["affid"], -1);
            int intAction = CastAide.AsInt32(context.Request["action"], -1);
            if (AffId == -1 || intAction == -1)
            {
                context.Response.Write("1");
                return;
            }
            DbResult rs = null;
            if (intAction == 0)
                rs = DataHandler.Affiliate.AffFavoriteDel(AffId);
            if (intAction == 1)
                rs = DataHandler.Affiliate.AffFavoriteIns(AffId);
            context.Response.Write(rs.Outputs["errcode"] + ";" + rs.Outputs["errmsg"]);
        }
    }
}