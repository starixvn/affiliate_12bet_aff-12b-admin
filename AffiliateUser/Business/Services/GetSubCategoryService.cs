using System.Data;
using System.Web;

namespace AffiliateUser.Business.Services
{
    public class GetSubCategoryService
    {
        public void GetSubCategory(HttpContext context)
        {
            AffHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string category = context.Request["category"];
            string activeValue = context.Request["activeValue"];

            DataView dv = (CacheUtil.GetBannerTypes() as DataTable).DefaultView;
            string subCategory = "CategoryMedia" + category;
            context.Response.Write(Common.GetDataOptionBanner(dv, "slSubCategoryMedia", subCategory,
                activeValue == "none" ? string.Empty : activeValue, true));
        }
    }
}