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

        public static ResultInfo UploadFileWithAutoDetectSize(HttpContext context, string newFolder, string fileName, out string size)
        {
            size = string.Empty;
            var uploadedFile = context.Request.Files.Count == 0 ? null : context.Request.Files[0];
            var result = ValidateFile(uploadedFile);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            result = EnsureFoldersExisted(newFolder);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            foreach (var path in AppSettings.BannerFolders)
            {
                var fullPath = Path.Combine(path, newFolder, fileName);
                if (File.Exists(fullPath))
                {
                    return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrmsgFileExist);
                }

                uploadedFile.SaveAs(fullPath);
                if (!Path.GetExtension(uploadedFile.FileName).ToLower().Equals(".swf"))
                {
                    var bitmap = new Bitmap(uploadedFile.InputStream);
                    size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                }
                else
                {
                    SwfInfo swfInfo = new SwfInfo(fullPath);
                    size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                }
            }

            return new ResultInfo();
        }

        public static ResultInfo MoveFiles(
            string category,
            string oldCategory,
            string newFolder,
            string newRelativeFilePath,
            string oldRelativeFilePath)
        {
            if (!category.Equals(oldCategory))
            {
                var result = EnsureFoldersExisted(newFolder);
                if (result.IsSuccessful == false)
                {
                    return result;
                }
            }

            foreach (var folder in AppSettings.BannerFolders)
            {
                var oldFullPath = Path.Combine(folder, oldRelativeFilePath);
                var newFullPath = Path.Combine(folder, newRelativeFilePath);
                if (File.Exists(oldFullPath))
                {
                    File.Move(oldFullPath, newFullPath);
                }
            }

            return new ResultInfo();
        }

        public static void DeleteFile(string relativeFilePath)
        {
            foreach (var folder in AppSettings.BannerFolders)
            {
                try
                {
                    var fullPath = Path.Combine(folder, relativeFilePath);
                    var file = new FileInfo(fullPath);
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
        }

        private static ResultInfo EnsureFoldersExisted(string newFolder)
        {
            foreach (var path in AppSettings.BannerFolders)
            {
                if (Directory.Exists(Path.Combine(path, newFolder)) == false)
                {
                    try
                    {
                        Directory.CreateDirectory(path + newFolder);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex.Message, ex);
                        return new ResultInfo(ResultInfo.UnknownErrorCode, Resources.Lang.ErrAccessFolder); ;
                    }
                }
            }

            return new ResultInfo();
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
    }
}