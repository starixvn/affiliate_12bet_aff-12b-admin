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
            string oldFileName = context.Request.Params["hdOldFileName"];
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
                extension = Path.GetExtension(oldFileName);
            }

            if (string.IsNullOrEmpty(inputFileName))
            {
                if (needToUploadFile)
                    inputFileName = upFileName;
                else
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileNameEmpty);
                }
            }
            else
            {
                var tempSB = new StringBuilder();
                int temp = inputFileName.LastIndexOf(".");
                if (temp > 0)
                    tempSB.Append(inputFileName.Substring(0, temp));
                else
                    tempSB.Append(inputFileName);

                tempSB.Append(".");
                tempSB.Append(extension);
                inputFileName = tempSB.ToString();
            }

            string err = string.Empty;
            int errCode = 0;
            string newFileName = string.Empty;
            string strDst = string.Empty;
            string newFolder = string.Empty;

            if (oldFileName.Contains("CategoryMedia"))  // banner with new categories
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
                newFileName = sb.ToString();
            }
            else
            {
                if (category == "Casino" || category == "Sportsbook")
                    category = subCategory;

                int index = oldFileName.LastIndexOf("/") + 1;
                newFolder = oldFileName.Substring(0, index);
                inputFileName = oldFileName.Substring(index, oldFileName.LastIndexOf(".") - index) + "." + extension;
                newFileName = newFolder + inputFileName;
            }

            string path = AppSettings.BannerFolders[0];

            string oldFullPath = path + oldFileName;
            strDst = path + newFileName;
            if (!category.Equals(oldCategory) && oldFileName.Contains("CategoryMedia"))
            {
                if (File.Exists(strDst))
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileExist);
                }
            }

            if (needToUploadFile)
            {
                var result = BannerHelper.UploadFileWithAutoDetectSize(context, newFolder, inputFileName, out size);
                if (result.IsSuccessful == false)
                {
                    return result;
                }
            }

            err = DataHandler.Banner.UpdBanner(Convert.ToInt32(idbanner), newFileName, category, language, currency, imageType, size, afftype, deviceType);
            if (!string.IsNullOrEmpty(err))
            {
                if (!string.IsNullOrEmpty(strDst))
                    BannerHelper.DeleteFile(strDst);
                return new ResultInfo(ResultInfo.UnknownErrorCode, err);
            }

            if (!needToUploadFile)
            {
                if (File.Exists(oldFullPath))
                {
                    if (!category.Equals(oldCategory))
                    {
                        if (Directory.Exists(path + newFolder) == false)
                            Directory.CreateDirectory(path + newFolder);
                    }
                    File.Move(oldFullPath, strDst);
                }
            }
            else if (!oldFileName.Equals(newFileName))
                BannerHelper.DeleteFile(oldFullPath);

            return new ResultInfo()
            {
                Message = Resources.Lang.UpdateBannerSuccessfull
            };
        }
    }
}