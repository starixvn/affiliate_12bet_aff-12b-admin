using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.AssetModification;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.AssetModification
{
    public class AssetModificationService : IAssetModificationService
    {
        private readonly IFileService _fileService;

        public AssetModificationService(
            IFileService fileService)
        {
            _fileService = fileService;
        }

        public ResultInfo DeleteAsset(DeleteAssetParam param)
        {
            var accessFolder = GetAssetFolder(param.RootFolder, param.SubPath);
            _fileService.DeleteFile(HttpUtility.HtmlEncode($"{accessFolder}/{param.FileName}"));

            return new ResultInfo { Message = "Delete Asset Successfully!" };
        }

        public ResultInfo UploadAssets(HttpPostedFileBase[] files, AddAssetsParam param)
        {
            var result = _fileService.ValidateUploadRequest(files, param.InputFileNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var inputFileNames = _fileService.PrepareInputFileNames(files, param.InputFileNames);

            result = _fileService.ValidateAllInputFileNames(inputFileNames, files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var folder = GetAssetFolder(param.RootFolder, param.SubPath);
            result = _fileService.CheckMultipleFilesInFolder(inputFileNames, folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            ProcessAndUploadFiles(files, inputFileNames, folder, param);

            return new ResultInfo { Message = "Upload Assets Successfully!" };
        }

        private void ProcessAndUploadFiles(HttpPostedFileBase[] files, string[] inputFileNames, string folder, AddAssetsParam param)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var fileName = inputFileNames[i];
                var path = $"{folder}/{fileName}";

                param.FilePaths.Add(path);

                _fileService.UploadFile(file, path);
            }
        }

        private static string GetAssetFolder(string rootFolder, string subPath)
        {
            if(string.IsNullOrEmpty(subPath))
            {
                return HttpUtility.HtmlEncode(rootFolder);
            }
            
            return HttpUtility.HtmlEncode($"{rootFolder}/{subPath}");
        }
    }
}
