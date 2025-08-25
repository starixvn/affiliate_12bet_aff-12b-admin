using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public class SystemFile : ISystemFile
    {
        public void DeleteFile(string serverPath, string filePath)
        {
            try
            {
                var fullPath = Path.Combine(serverPath, filePath);
                var file = new FileInfo(fullPath);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
        }

        public void MoveFile(string serverPath, string newFilePath, string oldFilePath)
        {
            try
            {
                var oldFullPath = Path.Combine(serverPath, oldFilePath);
                var newFullPath = Path.Combine(serverPath, newFilePath);

                if (File.Exists(oldFullPath))
                {
                    File.Move(oldFullPath, newFullPath);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
        }

        public void UploadFile(Bitmap bitmap, string filePath)
        {
            try
            {
                var extension = GetExtension(filePath);
                if (string.Equals(extension, ".gif", StringComparison.CurrentCultureIgnoreCase))
                {
                    bitmap.Save(filePath, ImageFormat.Gif);
                }
                else
                {
                    var isPng = string.Equals(extension, ".png", StringComparison.CurrentCultureIgnoreCase);
                    bitmap.Save(filePath, isPng ? ImageFormat.Png : ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
        }

        public ResultInfo CreateFolder(string serverPath, string folder)
        {
            if (!Directory.Exists(Path.Combine(serverPath, folder)))
            {
                try
                {
                    Directory.CreateDirectory(serverPath + folder);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex);
                    return new ResultInfo
                    {
                        Code = ErrCode.ErrCanNotAccessFolder,
                        Message = "Can not access folder."
                    };
                }
            }

            return new ResultInfo();
        }

        public ResultInfo CheckExistedFile(string serverPath, string filePath)
        {
            var newFullPath = Path.Combine(serverPath, filePath);
            if (File.Exists(newFullPath))
            {
                return new ResultInfo
                {
                    Code = ErrCode.ErrTheFileNameAlreadyExists,
                    Message = "This file name belong to an existing banner. Please change the file name."
                };
            }

            return new ResultInfo();
        }

        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        // New methods for Landing Page Builder
        public void SaveFile(HttpPostedFileBase file, string path)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            // Ensure directory exists
            var directory = Path.GetDirectoryName(path);
            EnsureDirectoryExists(directory);

            // Save the file
            file.SaveAs(path);
        }

        public bool FileExists(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            return File.Exists(path);
        }

        public long GetFileSize(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return 0;
            }

            try
            {
                var fileInfo = new FileInfo(path);
                return fileInfo.Length;
            }
            catch
            {
                return 0;
            }
        }

        public void CopyFile(string sourcePath, string destinationPath, bool overwrite = false)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                throw new ArgumentException("Source path cannot be null or empty", nameof(sourcePath));
            }

            if (string.IsNullOrWhiteSpace(destinationPath))
            {
                throw new ArgumentException("Destination path cannot be null or empty", nameof(destinationPath));
            }

            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"Source file not found: {sourcePath}");
            }

            // Ensure destination directory exists
            var destinationDirectory = Path.GetDirectoryName(destinationPath);
            EnsureDirectoryExists(destinationDirectory);

            File.Copy(sourcePath, destinationPath, overwrite);
        }

        public void EnsureDirectoryExists(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                return;
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public FileInfo GetFileInfo(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return null;
            }

            try
            {
                return new FileInfo(path);
            }
            catch
            {
                return null;
            }
        }

        public string ReadAllText(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                return string.Empty;
            }

            try
            {
                return File.ReadAllText(path);
            }
            catch
            {
                return string.Empty;
            }
        }

        public void WriteAllText(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            // Ensure directory exists
            var directory = Path.GetDirectoryName(path);
            EnsureDirectoryExists(directory);

            File.WriteAllText(path, content ?? string.Empty);
        }
    }
}
