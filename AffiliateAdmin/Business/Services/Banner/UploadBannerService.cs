using System;
using System.IO;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.Helpers;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Banner
{
    public class UploadBannerService
    {
        public ResultInfo UploadBanner(HttpContext context)
        {
            string upFileName = context.Request.Files[0].FileName;
            string inputFileName = context.Request.Params["inpFileName"];
            string category = context.Request.Params["slCategory"];
            string subCategory = context.Request.Params["slSubCategoryMedia"];
            string language = context.Request.Params["slLanguage"];
            string currency = context.Request.Params["slCurrency"];
            string imageType = context.Request.Params["slImageType"];
            string size = context.Request.Params["hdSize"];
            string afftype = context.Request.Params["slAffType"];
            string idbanner = context.Request.Params["hdBannerID"];
            string oldRelativeFilePath = context.Request.Params["hdOldFileName"];
            string oldCategory = context.Request.Params["hdOldCategory"];
            bool needToUploadFile = false;
            string deviceType = context.Request.Params["slDevice"];

            string extension = string.Empty;
            if (!string.IsNullOrEmpty(upFileName))
            {
                needToUploadFile = true;
                extension = Path.GetExtension(upFileName);
                if (!BannerHelper.IsValidExtension(extension))
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.errFileExtension);
                }
            }
            else
            {
                extension = Path.GetExtension(oldRelativeFilePath);
            }

            if (string.IsNullOrEmpty(inputFileName))
            {
                if (needToUploadFile)
                {
                    inputFileName = upFileName;
                }
                else
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileNameEmpty);
                }
            }

            string err = string.Empty;
            string newRelativeFilePath = string.Empty;
            string fullFilePath = string.Empty;
            string newFolder = string.Empty;

            if (oldRelativeFilePath.Contains("CategoryMedia"))  // banner with new categories
            {
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
                sb.Append(inputFileName);
                newRelativeFilePath = sb.ToString();
            }
            else
            {
                if (category == "Casino" || category == "Sportsbook")
                    category = subCategory;

                int index = oldRelativeFilePath.LastIndexOf("/") + 1;
                newFolder = oldRelativeFilePath.Substring(0, index);
                inputFileName = oldRelativeFilePath.Substring(index, oldRelativeFilePath.LastIndexOf(".") - index) + "." + extension;
                newRelativeFilePath = newFolder + inputFileName;
            }

            if (!category.Equals(oldCategory) && oldRelativeFilePath.Contains("CategoryMedia"))
            {
                foreach (var folder in AppSettings.BannerFolders)
                {
                    if (File.Exists(Path.Combine(folder, newRelativeFilePath)))
                    {
                        return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileExist);
                    }
                }
            }

            if (needToUploadFile)
            {
                var uploadingResult = BannerHelper.UploadFileWithAutoDetectSize(context, newFolder, inputFileName, out size);
                if (uploadingResult.IsSuccessful == false)
                {
                    return uploadingResult;
                }

                if (!oldRelativeFilePath.Equals(newRelativeFilePath))
                {
                    BannerHelper.DeleteFile(oldRelativeFilePath);
                }
            }
            else
            {
                var movingResult = BannerHelper.MoveFiles(category, oldCategory, newFolder, newRelativeFilePath, oldRelativeFilePath);
                if (movingResult.IsSuccessful == false)
                {
                    return movingResult;
                }
            }

            var result = DataHandler.Banner.UpdateBanner(Convert.ToInt32(idbanner), newRelativeFilePath, category, language, currency, imageType, size, afftype, deviceType);
            if (result.IsSuccessful == false)
            {
                BannerHelper.DeleteFile(newRelativeFilePath);
                return result;
            }

            return new ResultInfo()
            {
                Message = Resources.Lang.UpdateBannerSuccessfull
            };
        }
    }
}