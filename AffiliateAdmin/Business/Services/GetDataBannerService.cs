using System.Text;
using System.Web;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class GetDataBannerService
    {
        public void GetDataBanner(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string category = (context.Request["cate"].Equals("")) ? null : context.Request["cate"];
                string lang = (context.Request["lang"].Equals("")) ? null : context.Request["lang"];
                string currency = (context.Request["currency"].Equals("")) ? null : context.Request["currency"];
                string type = (context.Request["type"].Equals("")) ? null : context.Request["type"];
                string size = (context.Request["size"].Equals("")) ? null : context.Request["size"];
                int page = string.IsNullOrEmpty(context.Request["page"]) ? 1 : CastAide.AsInt32(context.Request["page"]);
                int pageSize = (context.Request["PageSize"].Equals("")) ? 20 : CastAide.AsInt32(context.Request["PageSize"]);
                int minNo = 0, maxNo = 0, totalPage = 0, totalBanner = 0;
                StringBuilder result = new StringBuilder();
                // tam thoi type=0 <=> normal banner
                int bannerType = CastAide.AsInt32(context.Request.Params["bannerType"], 0);
                bool isNewCateStruct = CastAide.AsBoolean(context.Request.Params["isNewCateStruct"], false);
                int affType = CastAide.AsInt32(context.Request["afftype"], (int)AffType.Asian);
                var deviceType = string.IsNullOrEmpty(context.Request["deviceType"]) ? "0" : context.Request["deviceType"];
                string content = string.Empty;
                if (isNewCateStruct)
                {
                    bool isSearchByParentCategory = false;
                    string paramCategory = category;
                    if (category == "Casino" || category == "Sportsbook")
                    {
                        if (string.IsNullOrEmpty(context.Request["subCategory"]) || context.Request["subCategory"].ToString() == "All")
                            isSearchByParentCategory = true;
                        else
                            paramCategory = context.Request["subCategory"].ToString();
                    }
                    content = GenerateHTML.Banner.GetBannerList(paramCategory, isSearchByParentCategory, lang, type, size, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, bannerType, affType, deviceType);
                }
                else
                    content = GenerateHTML.Banner.GetBannerList(category, false, lang, type, size, currency, page, ref minNo, ref maxNo, ref totalBanner, ref totalPage, pageSize, bannerType, affType, deviceType);

                result.Append(minNo.ToString());
                result.Append("$$$");
                result.Append(maxNo.ToString());
                result.Append("$$$");
                result.Append(content);

                context.Response.Write(result.ToString());
            }
        }
    }
}