using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Utilities.MediaGallery
{
    public static class BannerHelper
    {
        private static readonly string[] ValidExtensions = {
            ".gif", ".jpg", ".png", ".jpeg", ".swf"
        };

        public static bool IsValidExtension(string extension)
        {
            return ValidExtensions.Contains(extension.ToLowerInvariant());
        }

        public static ResultInfo UploadFileWithAutoDetectSize(InsertUpdateBannerParam param, string newFolder)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            var uploadedFile = param.File;
            var result = ValidateFile(uploadedFile);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = EnsureFoldersExisted(newFolder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            foreach (var path in appSetting.BannerFolders)
            {
                var fullPath = Path.Combine(path, newFolder, param.FileName);
                if (File.Exists(fullPath))
                {
                    return new ResultInfo {
                        Code = Code.GeneralErrorCode,
                        Message = "The file name already exists."
                    };
                }

                uploadedFile.SaveAs(fullPath);

                if (!Path.GetExtension(uploadedFile.FileName).Equals(".swf"))
                {
                    var bitmap = new Bitmap(uploadedFile.InputStream);
                    param.Size = string.Format("{0}x{1}", bitmap.Width, bitmap.Height);
                }
                else
                {
                    SwfInfo swfInfo = new SwfInfo(fullPath);
                    param.Size = string.Format("{0}x{1}", swfInfo.Width, swfInfo.Height);
                }
            }

            return new ResultInfo();
        }

        public static void DeleteFile(string relativeFilePath)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            foreach (var folder in appSetting.BannerFolders)
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
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            foreach (var path in appSetting.BannerFolders)
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

        private static ResultInfo ValidateFile(HttpPostedFile uploadedFile)
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            if (uploadedFile == null)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "You must browse banner to update.",
                };
            }

            string extension = Path.GetExtension(uploadedFile.FileName);
            if (!IsValidExtension(extension) || !MimeTypeHelper.IsValidMimeType(uploadedFile.InputStream))
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Sorry! We just support these file types : .jpg, .gif, .png, .swf, .jpeg"
                };
            }

            if (uploadedFile.ContentLength > appSetting.ImageSize)
            {
                return new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = "Your upload file size must be <= 3 MB."
                };
            }

            return new ResultInfo();
        }
    }
}
