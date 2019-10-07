using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Logging;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

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

        public static ResultInfo UploadFileWithAutoDetectSize(IAppSetting _appSetting, InsertUpdateBannerParam param, string newFolder)
        {
            var uploadedFile = param.File;
            var result = ValidateFile(_appSetting, uploadedFile);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = EnsureFoldersExisted(_appSetting, newFolder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            foreach (var path in _appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(path, param.FileName);
                if (File.Exists(fullPath))
                {
                    return new ResultInfo
                    {
                        Code = Code.GeneralErrorCode,
                        Message = Lang.InvalidBannerAlready
                    };
                }

                uploadedFile.SaveAs(fullPath);

                var bitmap = new Bitmap(uploadedFile.InputStream);
                param.Size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
            }

            return new ResultInfo();
        }

        public static void MoveFiles(IAppSetting _appSetting, string newRelativeFilePath, string oldRelativeFilePath)
        {
            foreach (var folder in _appSetting.BannerFolders)
            {
                var oldFullPath = Path.Combine(folder, oldRelativeFilePath);
                var newFullPath = Path.Combine(folder, newRelativeFilePath);

                if (File.Exists(oldFullPath))
                {
                    File.Move(oldFullPath, newFullPath);
                }
            }
        }

        public static void DeleteFile(IAppSetting _appSetting, string relativeFilePath)
        {
            foreach (var folder in _appSetting.BannerFolders)
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

        public static ResultInfo EnsureFoldersExisted(IAppSetting _appSetting, string newFolder)
        {
            foreach (var path in _appSetting.BannerFolders)
            {
                if (!Directory.Exists(Path.Combine(path, newFolder)))
                {
                    try
                    {
                        Directory.CreateDirectory(path + newFolder);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(ex.Message, ex);
                        return new ResultInfo {
                            Code = Code.GeneralErrorCode,
                            Message = "Can not access folder."
                        };
                    }
                }
            }

            return new ResultInfo();
        }

        private static ResultInfo ValidateFile(IAppSetting _appSetting, HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidBannerBrowser
                };
            }

            if (!MimeTypeHelper.IsValidMimeType(uploadedFile.InputStream))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidBannerExtensionFile
                };
            }

            if (uploadedFile.ContentLength > _appSetting.ImageSize)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.InvalidBannerSize
                };
            }

            return new ResultInfo();
        }
    }
}
