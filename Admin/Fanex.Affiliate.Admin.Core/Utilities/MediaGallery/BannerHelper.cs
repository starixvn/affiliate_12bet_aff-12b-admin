using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using System;
using System.Linq;
using System.Text;

namespace Fanex.Affiliate.Admin.Core.Utilities.MediaGallery
{
    public static class BannerHelper
    {
        private static readonly string[] ValidExtensions = {
            ".gif", ".jpg", ".png", ".jpeg"
        };

        public static bool IsValidExtension(string extension)
        {
            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        public static string GetSize(int width, int height)
        {
            return string.Format("{0}x{1}", width, height);
        }

        public static string GeneralFolder(string category, string subCategory)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("CategoryMedia/");
            stringBuilder.Append(category);
            stringBuilder.Append("/");
            if (string.Compare(category, Lang.Casino, StringComparison.CurrentCultureIgnoreCase) == 0
                || string.Compare(category, Lang.Sportsbook, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                stringBuilder.Append(subCategory);
                stringBuilder.Append("/");
            }

            return stringBuilder.ToString();
        }

        public static ResultInfo CreateErrorResult()
        {
            return new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = Lang.InvalidBannerExtensionFile
            };
        }
    }
}
