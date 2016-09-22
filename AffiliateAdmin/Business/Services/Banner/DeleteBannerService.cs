using System;
using System.Web;
using AffiliateAdmin.Business.Helpers;
using AffiliateAdmin.Business.Models.Core;

namespace AffiliateAdmin.Business.Services.Banner
{
    public class DeleteBannerService
    {
        public ResultInfo DeleteBanner(HttpContext context)
        {
            string[] param = context.Request.Params["params"].Split(new string[] { "$$$" }, StringSplitOptions.None);
            string listIdBanner = string.Empty, listFilename = string.Empty;

            string result = ValidateData(param, ref listIdBanner, ref listFilename);
            if (string.IsNullOrEmpty(result))
            {
                string parentFolder = context.Server.MapPath("../Banner//");
                string[] files = listFilename.Split(new char[] { ',' });

                result = DataHandler.Banner.DelDataBanner(listIdBanner);
                if (result.ToString() == string.Empty)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        BannerHelper.DeleteFile(parentFolder + files[i].Replace("/", "\\"));
                    }
                }
            }

            return new ResultInfo()
            {
                Message = result
            };
        }

        private string ValidateData(string[] param, ref string bannerIDList, ref string filenameList)
        {
            try
            {
                if (param.Length == 2)
                {
                    bannerIDList = param[0].Trim();
                    bannerIDList = bannerIDList.Substring(0, bannerIDList.Length - 1);
                    filenameList = param[1].Trim();
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