using Fanex.Affiliate.Admin.Core.DTO;
using System.Drawing;
using System.IO;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public interface ISystemFile
    {
        // Existing methods
        void UploadFile(Bitmap bitmap, string filePath);
        void MoveFile(string serverPath, string newFilePath, string oldFilePath);
        void DeleteFile(string serverPath, string filePath);
        ResultInfo CreateFolder(string serverPath, string folder);
        ResultInfo CheckExistedFile(string serverPath, string filePath);
        string GetExtension(string fileName);

        // New methods for Landing Page Builder
        /// <summary>
        /// Save uploaded file to specified path
        /// </summary>
        /// <param name="file">Uploaded file</param>
        /// <param name="path">Target file path</param>
        void SaveFile(HttpPostedFileBase file, string path);

        /// <summary>
        /// Check if file exists at specified path
        /// </summary>
        /// <param name="path">File path to check</param>
        /// <returns>True if file exists</returns>
        bool FileExists(string path);

        /// <summary>
        /// Get file size in bytes
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File size in bytes, or 0 if file doesn't exist</returns>
        long GetFileSize(string path);

        /// <summary>
        /// Copy file from source to destination
        /// </summary>
        /// <param name="sourcePath">Source file path</param>
        /// <param name="destinationPath">Destination file path</param>
        /// <param name="overwrite">Whether to overwrite existing file</param>
        void CopyFile(string sourcePath, string destinationPath, bool overwrite = false);

        /// <summary>
        /// Create directory if it doesn't exist
        /// </summary>
        /// <param name="directoryPath">Directory path to create</param>
        void EnsureDirectoryExists(string directoryPath);

        /// <summary>
        /// Get file information
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>FileInfo object or null if file doesn't exist</returns>
        FileInfo GetFileInfo(string path);

        /// <summary>
        /// Read all text from file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>File content as string</returns>
        string ReadAllText(string path);

        /// <summary>
        /// Write text to file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="content">Content to write</param>
        void WriteAllText(string path, string content);
    }
}
