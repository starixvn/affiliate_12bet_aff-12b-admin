using System.Data;
using System.Web;
using AffiliateAdmin.Business.DataAccess;

namespace AffiliateAdmin.Business.Services
{
    public class GetSubCategoryService
    {
        public void GetSubCategory(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string category = context.Request["category"];
                bool genAll = bool.Parse(context.Request["genAll"]);
                string selectedSubCategory = context.Request["selectedSubCategory"];
                if (selectedSubCategory == "None")
                    selectedSubCategory = string.Empty;

                DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
                string subCategory = "CategoryMedia" + category;
                context.Response.Write(Common.GetDataOptionBanner(dv, "slSubCategoryMedia", subCategory, selectedSubCategory, genAll));
            }
        }
    }
}