using System;
using System.IO;
using System.Web;
using AffiliateAdmin.Business.Helpers;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Banner
{
    public class DeleteBannerService
    {
        public ResultInfo DeleteBanner(HttpContext context)
        {
            string param = context.Request.Params["params"];
            string listIdBanner = string.Empty, listFilename = string.Empty;

            string message = ValidateData(param, ref listIdBanner, ref listFilename);
            if (string.IsNullOrEmpty(message))
            {
                string[] files = listFilename.Split(new char[] { ',' });

                var result = DataHandler.Banner.DeleteBanner(listIdBanner);
                if (result.IsSuccessful)
                {
                    DeleteBannerFiles(files);
                }
                else
                {
                    return result;
                }
            }

            return new ResultInfo()
            {
                Message = message
            };
        }

        private static void DeleteBannerFiles(string[] files)
        {
            foreach (var folder in AppSettings.BannerFolders)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    BannerHelper.DeleteFile(Path.Combine(folder, files[i].Replace("/", "\\")));
                }
            }
        }

        private string ValidateData(string param, ref string bannerIDList, ref string filenameList)
        {
            try
            {
                string[] paramArray = param.Split(new string[] { "$$$" }, StringSplitOptions.None);

                if (paramArray.Length == 2)
                {
                    bannerIDList = paramArray[0].Trim();
                    bannerIDList = bannerIDList.Substring(0, bannerIDList.Length - 1);
                    filenameList = paramArray[1].Trim();
                    filenameList = filenameList.Substring(0, filenameList.Length - 1);
                }
                else
                {
                    return "Invalid data sent to server!";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Empty;
        }
    }
}