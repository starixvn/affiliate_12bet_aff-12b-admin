using Fanex.Affiliate.Admin.Core.DTO;
using System.Collections.Generic;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IHtmlFileService
    {
        ResultInfo ValidateFileName(string fileName);

        void DeleteFile(string fileName, string folder);

        void DeleteFiles(IEnumerable<string> fileNames, string folder);

        ResultInfo HandleUploadFile(HttpPostedFileBase file, string fileName, string folder);

        ResultInfo HandleRenameOldFile(string oldFileName, string newFileName, string folder);

        ResultInfo HandleReplaceFileByNewFile(HttpPostedFileBase file, string oldFileName, string newFileName, string folder);
    }
}
