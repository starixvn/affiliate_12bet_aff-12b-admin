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

            string extension = string.Empty;
            if (!string.IsNullOrEmpty(newfilename))
            {
                extension = Path.GetExtension(newfilename);
                if (!BannerHelper.IsValidExtension(extension))
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

                tempSB.Append(".").Append(extension);
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

            string path = AppSettings.BannerDirectory;
            string strDst = string.Empty;

            if (File.Exists(path + newFolder + "\\" + inputfilename))
            {
                return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileExist);
            }

            var result = BannerHelper.UploadFileWithAutoDetectSize(context, newFolder, path, inputfilename, out size, ref strDst);
            if (result.IsSuccessful)
            {
                result = DataHandler.Banner.InsertBanner(fileName, category, language, currency, imageType, size, affType, type, deviceType);
                if (result.IsSuccessful == false && !string.IsNullOrEmpty(strDst))
                {
                    BannerHelper.DeleteFile(strDst);
                }
            }

            if (result.IsSuccessful)
            {
                result.Message = Resources.Lang.AddBannerSuccessful;
            }

            return result;
        }
    }
}