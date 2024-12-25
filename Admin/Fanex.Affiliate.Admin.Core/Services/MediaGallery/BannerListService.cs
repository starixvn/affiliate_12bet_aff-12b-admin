using Fanex.Affiliate.Admin.Core.Commands.MediaGallery;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.MediaGallery;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class BannerListService : BaseDynamicService, IBannerListService
    {
        private const int MaxLength = 50;
        private const string RestrictedMultiInputFileNamesPattern = "[\\\\/:*?\"<>#%&+]";
        private const string RestrictedInputFileNamePattern = "[\\\\/:*?\"<>|#%&+]";

        private readonly IAppSetting _appSetting;
        private readonly ISystemFile _systemFile;
        private readonly IBannerValidationService _bannerValidationService;
        public BannerListService(
            IDynamicRepository dynamicRepository,
            IAppSetting appSetting,
            ISystemFile systemFile,
            IBannerValidationService bannerValidationService) : base(dynamicRepository)
        {
            _appSetting = appSetting;
            _systemFile = systemFile;
            _bannerValidationService = bannerValidationService;
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            var criteria = new BannerCriteria
            {
                Size = param.Size,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };
            AssignBaseBannerCriteria(criteria, param);

            var dataList = DynamicRepository.Fetch<BannerInfo>(criteria);

            return new EntityResult<BannerInfo>
            {
                MainData = dataList,
                TotalItem = criteria.TotalItem,
            };
        }

        public BannerInfo GetBannerDetail(int selectedBannerId, byte userType)
        {
            var criteria = new BannerDetailCriteria
            {
                BannerId = selectedBannerId,
                UserType = userType,
            };

            return GetTData<BannerInfo>(criteria);
        }

        private ResultInfo ValidateAllFiles(HttpPostedFileBase[] files)
        {
            var resultValidateAllFiles = new ResultInfo();
            var message = new StringBuilder();
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var result = _bannerValidationService.ValidateFile(fileName, file.ContentLength);

                if (!result.IsSuccessful)
                {
                    resultValidateAllFiles.Code = result.Code;
                    message.Append($"File {fileName} is incorrect.<br />" + result.Message);
                }
            }

            resultValidateAllFiles.Message = message.ToString();
            return resultValidateAllFiles;
        }

        private ResultInfo CheckDuplicateFileNames(string[] inputFileNames)
        {
            var length = inputFileNames.Length;
            for (var i = 0; i <= length - 1; i++)
            {
                var fileName = inputFileNames[i].Trim();

                for (var j = i + 1; j < length; j++)
                {
                    if (string.Equals(fileName, inputFileNames[j].Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        return new ResultInfo
                        {
                            Code = ErrCode.GeneralErrorCode,
                            Message = "File Name is unique, please enter other file name!"
                        };
                    }
                }
            }

            return new ResultInfo();
        }

        private ResultInfo ValidateAllInputFileNames(string[] inputFileNames, HttpPostedFileBase[] files)
        {
            var result = CheckDuplicateFileNames(inputFileNames);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var message = new StringBuilder();
            for (int i = 0; i < inputFileNames.Length; i++)
            {
                var inputFileName = inputFileNames[i];

                if (string.IsNullOrEmpty(inputFileNames[i]))
                {
                    var file = files[i];

                    inputFileNames[i] = file.FileName;

                    if (file.FileName.Length > MaxLength)
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name for File Name {i + 1} or select another File.<br />");
                    }
                }
                else
                {
                    if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(inputFileName)))
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"File Name {i + 1} is incorrect.<br />" + Lang.InvalidBannerExtensionFile);
                    }

                    if (inputFileName.Length > MaxLength)
                    {
                        result.Code = ErrCode.GeneralErrorCode;
                        message.Append($"The maximum length of file name is 50. Please choose another name File Name {i + 1}.<br />");
                    }
                }
            }

            result.Message = message.ToString();
            return result;
        }

        private ResultInfo CheckMultipleFilesInFolder(string[] inputFileNames, string folder)
        {
            var resultCheckMultipleFilesInFolder = new ResultInfo();
            var message = new StringBuilder();

            for (int i = 0; i < inputFileNames.Length; i++)
            {
                var inputFileName = inputFileNames[i];
                var filePath = $"CategoryMedia/{folder}/{inputFileName}";
                var result = _bannerValidationService.CheckFileInFolder($"CategoryMedia/{folder}/", filePath);

                if (!result.IsSuccessful)
                {
                    if (result.Code == ErrCode.ErrCanNotAccessFolder)
                    {
                        return result;
                    }

                    resultCheckMultipleFilesInFolder.Code = ErrCode.GeneralErrorCode;
                    message.Append($"{inputFileName}, ");
                }
            }

            resultCheckMultipleFilesInFolder.Message = resultCheckMultipleFilesInFolder.IsSuccessful ? string.Empty
                : message.Append("This file name(s) belong to existing banner(s). Please change the file name.").ToString();

            return resultCheckMultipleFilesInFolder;
        }

        private ResultInfo ValidateCurrency(InsertUpdateMultipleBannersParam param)
        {
            if (param.UserType == UserType.Player && string.IsNullOrEmpty(param.Currencies))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = "Please select Currency."
                };
            }

            return new ResultInfo();
        }

        private static void BuildFilesXml(InsertUpdateMultipleBannersParam param)
        {
            var result = new StringBuilder();
            result.Append("<root>");

            for (var i = 0; i < param.FilePaths.Count; i++)
            {
                var row = $"<row FileName=\"{param.FilePaths[i]}\" ImageType=\"Image\"  Size=\"{param.Sizes[i]}\" />";
                result.Append(row);
            }

            result.Append("</root>");

            param.Files = result.ToString();
        }

        public ResultInfo InsertBanner(HttpPostedFileBase[] files, InsertUpdateMultipleBannersParam param)
        {
            var result = ValidateCurrency(param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = ValidateAllFiles(files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            if (RegexHelper.IsMatch(param.InputFileNames, RestrictedMultiInputFileNamesPattern))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = Lang.FileNameCanNotContain
                };
            }

            var inputFileNames = param.InputFileNames.Split('|');
            result = ValidateAllInputFileNames(inputFileNames, files);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = CheckMultipleFilesInFolder(inputFileNames, param.Folder);
            if (!result.IsSuccessful)
            {
                return result;
            }

            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];

                using (var bitmap = new Bitmap(file.InputStream))
                {
                    var inputFileName = inputFileNames[i];
                    var path = $"CategoryMedia/{param.Folder}/{inputFileName}";
                    param.FilePaths.Add(path);
                    param.Sizes.Add(BannerHelper.GetSize(bitmap.Width, bitmap.Height));
                    UploadFile(bitmap, path);
                }
            }

            BuildFilesXml(param);
            result = InsertBanner(param);

            if (!result.IsSuccessful)
            {
                foreach (var path in param.FilePaths)
                {
                    DeleteFile(path);
                }
            }

            return result;
        }

        private ResultInfo ValidationInputFileName(HttpPostedFileBase file, InsertUpdateMultipleBannersParam param)
        {
            if (!string.IsNullOrEmpty(param.InputFileNames))
            {
                if (RegexHelper.IsMatch(param.InputFileNames, RestrictedInputFileNamePattern))
                {
                    return new ResultInfo
                    {
                        Code = ErrCode.GeneralErrorCode,
                        Message = Lang.FileNameCanNotContain
                    };
                }

                if (!BannerHelper.IsValidExtension(_systemFile.GetExtension(param.InputFileNames)))
                {
                    return BannerHelper.CreateErrorResult();
                }
            }
            else
            {
                param.InputFileNames = file.FileName;
            }

            return param.InputFileNames.Length > MaxLength ? new ResultInfo
            {
                Code = ErrCode.GeneralErrorCode,
                Message = Lang.MaxLenghtOfFileName
            } : new ResultInfo();
        }

        public ResultInfo UpdateBanner(HttpPostedFileBase file, InsertUpdateMultipleBannersParam param)
        {
            var result = ValidateCurrency(param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            result = ValidationInputFileName(file, param);
            if (!result.IsSuccessful)
            {
                return result;
            }

            var oldBannerInfo = GetBannerDetail(param.BannerId, param.UserType);
            var oldFilePath = oldBannerInfo.FileName;

            result = CheckFileInFolder(param, oldFilePath);

            if (!result.IsSuccessful)
            {
                return result;
            }

            if (file != null)
            {
                result = _bannerValidationService.ValidateFile(file.FileName, file.ContentLength);

                if (result.IsSuccessful)
                {
                    using (var bitmap = new Bitmap(file.InputStream))
                    {
                        param.Sizes.Add(BannerHelper.GetSize(bitmap.Width, bitmap.Height));
                        BuildFilesXml(param);
                        result = UpdateBanner(param);

                        if (result.IsSuccessful)
                        {
                            DeleteFile(oldFilePath);
                            UploadFile(bitmap, param.FilePaths[0]);
                        }
                    }
                }
            }
            else
            {
                param.Sizes.Add(oldBannerInfo.Size);
                BuildFilesXml(param);
                result = UpdateBanner(param);

                if (result.IsSuccessful && !string.Equals(param.FilePaths[0], oldFilePath, StringComparison.CurrentCultureIgnoreCase))
                {
                    MoveFile(param.FilePaths[0], oldFilePath);
                }
            }

            return result;
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            var fileNameList = GetFileNameList(param.BannerIdList, param.UserType);
            var command = new DeleteBannerCommand
            {
                BannerIds = param.BannerIdList,
                TargetId = param.UserType,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            var result = ExecuteReturnResultInfo(command);

            if (result.IsSuccessful)
            {
                DeleteFiles(fileNameList);
            }
            return result;
        }

        public IEnumerable<MediaGalleryFilterItem> GetSizeList(SizeParam param)
        {
            var criteria = new MediaSizeCriteria
            {
                ImageType = param.ImageType,
            };
            AssignBaseBannerCriteria(criteria, param);

            return FetchTData<MediaGalleryFilterItem>(criteria);
        }

        private void UploadFile(Bitmap bitmap, string fileName)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                var filePath = Path.Combine(serverPath, fileName);
                _systemFile.UploadFile(bitmap, filePath);
            }
        }

        private void DeleteFile(string filePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.DeleteFile(serverPath, filePath);
            }
        }

        private void DeleteFiles(List<string> fileList)
        {
            for (int i = 0; i < fileList.Count; i++)
            {
                DeleteFile(fileList[i]);
            }
        }

        private void MoveFile(string newFilePath, string oldFilePath)
        {
            foreach (var serverPath in _appSetting.BannerFolders)
            {
                _systemFile.MoveFile(serverPath, newFilePath, oldFilePath);
            }
        }

        private List<string> GetFileNameList(string bannerIds, byte userType)
        {
            var bannerIdList = bannerIds.Split(',');
            var fileNameList = new List<string>();

            foreach (var bannerId in bannerIdList)
            {
                var bannerInfo = GetBannerDetail(Convert.ToInt32(bannerId), userType);
                fileNameList.Add(bannerInfo.FileName);
            }

            return fileNameList;
        }

        private ResultInfo CheckFileInFolder(InsertUpdateMultipleBannersParam param, string oldFilePath)
        {
            var path = $"CategoryMedia/{param.Folder}/{param.InputFileNames}";
            var result = _bannerValidationService.CheckFileInFolder($"CategoryMedia/{param.Folder}/", path);
            param.FilePaths.Add(path);

            if (result.Code == ErrCode.ErrTheFileNameAlreadyExists && string.Compare(path, oldFilePath, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return new ResultInfo();
            }

            return result;
        }

        private ResultInfo InsertBanner(InsertUpdateMultipleBannersParam param)
        {
            var command = new InsertBannerCommand();
            AssignBaseBannerCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private ResultInfo UpdateBanner(InsertUpdateMultipleBannersParam param)
        {
            var command = new UpdateBannerCommand
            {
                BannerId = param.BannerId,
            };
            AssignBaseBannerCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignBaseBannerCommand(BaseBannerCommand command, InsertUpdateMultipleBannersParam param)
        {
            command.TargetId = param.UserType;
            command.Files = param.Files;
            command.Categories = param.Categories;
            command.SubCategories = param.SubCategories;
            command.Language = param.Language;
            command.Currencies = param.Currencies;
            command.AffTypes = param.AffTypes;
            command.DeviceTypes = param.DeviceTypes;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }

        private static void AssignBaseBannerCriteria(BaseBannerCriteria criteria, BaseBannerParam param)
        {
            criteria.Category = param.Category;
            criteria.SubCategory = param.SubCategory;
            criteria.Language = param.Language;
            criteria.Currency = param.Currency;
            criteria.AffType = param.AffType;
            criteria.DeviceType = param.DeviceType;
            criteria.UserType = param.UserType;
        }
    }
}
