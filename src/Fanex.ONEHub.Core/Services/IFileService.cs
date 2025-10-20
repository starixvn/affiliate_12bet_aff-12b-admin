
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IFileService
    {
        ResultInfo ValidateAllFiles(HttpPostedFileBase[] files);
        ResultInfo ValidateFile(string fileName, int contentLength, Stream inputStream);
        ResultInfo ValidateProvidedInputFileName(string inputFileName, int index, StringBuilder message);
        ResultInfo ValidateAllInputFileNames(string[] inputFileNames, HttpPostedFileBase[] files);
        ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames, string folder);
        ResultInfo CheckFileInFolder(string filePath);
        ResultInfo CheckDuplicateFileNames(string[] inputFileNames, string nameType = "File");
        void UploadFile(HttpPostedFileBase file, string fileName);
        void DeleteFile(string filePath);
        string[] PrepareInputFileNames(HttpPostedFileBase[] files, string inputFileNames);
        ResultInfo ValidateUploadRequest(HttpPostedFileBase[] files, string inputFileNames, string inputMediaNames = null);
        void CleanupUploadedFiles(List<string> filePaths);
    }
}
