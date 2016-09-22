using System.IO;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.Helpers;
using AffiliateAdmin.Business.Models.Core;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services.Banner
{
    public class AddBannerService
    {
        public ResultInfo AddBanner(HttpContext context)
        {
            string newfilename = context.Request.Files[0].FileName;
            string inputfilename = context.Request.Params["inpFileName"];
            string category = context.Request.Params["slCategory"];
            string subCategory = context.Request.Params["slSubCategoryMedia"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string imageType = context.Request.Params["slImageType"];
            string size = context.Request.Params["slSize"];
            string affType = context.Request.Params["slAffType"];
            string deviceType = context.Request.Params["slDevice"];

            int type = CastAide.AsInt32(context.Request.Params["hidBannerType"], 0);

            string ext = string.Empty;
            if (!string.IsNullOrEmpty(newfilename))
            {
                if (!BannerHelper.IsValidExtension(newfilename, ref ext))
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.errFileExtension);
                }
            }

            if (string.IsNullOrEmpty(inputfilename))
            {
                inputfilename = newfilename;
            }
            else
            {
                StringBuilder tempSB = new StringBuilder();
                int temp = inputfilename.LastIndexOf(".");
                if (temp > 0)
                    tempSB.Append(inputfilename.Substring(0, temp));
                else
                    tempSB.Append(inputfilename);

                tempSB.Append(".").Append(ext);
                inputfilename = tempSB.ToString();
            }

            string fileName = string.Empty;
            string newFolder = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("CategoryMedia/");
            sb.Append(category);
            sb.Append("/");
            if (category == "Casino" || category == "Sportsbook")
            {
                sb.Append(subCategory);
                sb.Append("/");
                category = subCategory;
            }
            newFolder = sb.ToString();
            sb.Append(inputfilename);
            fileName = sb.ToString();

            string err = string.Empty;
            int errCode = 0;
            string path = context.Server.MapPath("../Banner//");
            string strDst = string.Empty;

            if (File.Exists(path + newFolder + "\\" + inputfilename))
            {
                return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileExist);
            }

            BannerHelper.UploadFileWithAutoDetectSize(context, newFolder, path, inputfilename, ref errCode, out size, ref strDst);
            if (errCode == 0)
            {
                int bannerID = DataHandler.Banner.InsBanner(fileName, category, language, currency, imageType, size, affType, ref errCode, ref err, type, deviceType);
                if (errCode != 0)
                {
                    if (!string.IsNullOrEmpty(strDst))
                        BannerHelper.DeleteFile(strDst);
                    return new ResultInfo(ResultInfo.UnknownErrorCode, err);
                }
            }

            if (errCode == 1)
            {
                return new ResultInfo(errCode, Resources.Lang.ErrmsgFileEmpty);
            }
            else if (errCode == 2)
            {
                return new ResultInfo(errCode, Resources.Lang.ErrmsgFile);
            }
            else if (errCode == 3)
            {
                return new ResultInfo(errCode, Resources.Lang.ErrmsgSizeFile);
            }

            return new ResultInfo()
            {
                Message = Resources.Lang.AddBannerSuccessful
            };
        }
    }
}