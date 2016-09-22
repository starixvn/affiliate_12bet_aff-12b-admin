using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using AffiliateAdmin.Business.Models.Core;
using Fanex.Logging;

namespace AffiliateAdmin.Business.Helpers
{
    public class BannerHelper
    {
        public const int NullFileError = 1;

        public const int InvalidExtensionError = 2;

        public const int InvalidFileSizeError = 3;

        private static readonly string[] ValidExtensions = new string[]
        {
            ".gif", ".jpg", ".png", ".jpeg", ".swf"
        };

        public static bool IsValidExtension(string extension)
        {
            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        public static ResultInfo UploadFileWithAutoDetectSize(HttpContext context, string newFolder, string path, string fileName, out string size, ref string strDst)
        {
            size = string.Empty;
            var uploadedFile = GetUploadedFile(context);
            var result = ValidateFile(uploadedFile);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            if (Directory.Exists(path + newFolder) == false)
            {
                Directory.CreateDirectory(path + newFolder);
            }

            strDst = path + newFolder + "\\" + fileName;
            uploadedFile.SaveAs(strDst);
            if (!Path.GetExtension(uploadedFile.FileName).ToLower().Equals(".swf"))
            {
                var bitmap = new Bitmap(context.Request.Files[0].InputStream);
                size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
            }
            else
            {
                SwfInfo swfInfo = new SwfInfo(strDst);
                size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
            }

            return new ResultInfo();
        }

        public static void DeleteFile(string path)
        {
            try
            {
                var file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

        private static ResultInfo ValidateFile(HttpPostedFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                return new ResultInfo()
                {
                    Code = NullFileError,
                    Message = Resources.Lang.ErrmsgFileEmpty
                };
            }

            string extension = Path.GetExtension(uploadedFile.FileName);
            if (!IsValidExtension(extension))
            {
                return new ResultInfo()
                {
                    Code = InvalidExtensionError,
                    Message = Resources.Lang.ErrmsgFile
                };
            }

            if (uploadedFile.ContentLength > AppSettings.ImageSize)
            {
                return new ResultInfo()
                {
                    Code = InvalidFileSizeError,
                    Message = Resources.Lang.ErrmsgSizeFile
                };
            }

            return new ResultInfo();
        }

        private static HttpPostedFile GetUploadedFile(HttpContext context)
        {
            for (int i = 0; i < context.Request.Files.Count; i++)
            {
                if (string.IsNullOrEmpty(context.Request.Files[i].FileName) == false)
                {
                    return context.Request.Files[i];
                }
            }

            return null;
        }
    }
}