using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class BannerListServiceTest
    {
        private const string ErrorMessage = "Sorry! We just support these file types : .jpg, .gif, .png, .jpeg <br />";

        [Fact]
        public void GetBannerList_WhenCallWithNotSubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new BannerParam();
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(b => b.GetBannerList(param)).Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetBannerList(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetBannerList_WhenCallWithHaveSubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new BannerParam { Category = "Casino", SubCategory = "Casino Bonus" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(b => b.GetBannerList(param))
                .Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetBannerList(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetBannerList_WhenCallWithHaveEmptySubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new BannerParam { Category = "Casino" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(b => b.GetBannerList(It.IsAny<BannerParam>()))
                .Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetBannerList(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetBannerDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var selectedBannerId = 123;
            var passedParam = 0;
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(selectedBannerId)).Returns(new BannerInfo { BannerId = 123 }).Callback<int>(p => passedParam = p);
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetBannerDetail(selectedBannerId);

            // Assert
            Assert.Equal(selectedBannerId, result.BannerId);
            Assert.Equal(selectedBannerId, passedParam);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = ErrorMessage });
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream, fileName: "abc.pdf")) }, new InsertUpdateMultipleBannersParam());

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("File abc.pdf is incorrect.<br />" + ErrorMessage, result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithInputFileNamesMaxLenght_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "123test123test123test123test123test123test123test123test.png" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream)) }, param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("The maximum length of file name is 50. Please choose another name File Name 1.<br />", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithSpecialCharacter_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "abc*.png" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream)) }, param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("A File Name can't contain any of the following characters: \\/:*?\"<>|#%&+", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithDuplicateFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png|abc.png" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream)) }, param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("File Name is unique, please enter other file name!", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidInputFileNames_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "test.pdf|abc.png" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.SetupSequence(s => s.GetExtension(It.IsAny<string>())).Returns(".pdf").Returns(".png");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream)) }, param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("File Name 1 is incorrect.<br />Sorry! We just support these file types : .jpg, .gif, .png, .jpeg<br />", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidFileMaxLenght_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream, fileName: "123test123test123test123test123test123test123test123test.png")) }, param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("The maximum length of file name is 50. Please choose another name for File Name 1 or select another File.<br />", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithExistedFile_ReturnError()
        {
            // Arrange
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "The file name already exists." });
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream, fileName: "abc.png")) }, new InsertUpdateMultipleBannersParam { InputFileNames = "" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("abc.png, This file name(s) belong to existing banner(s). Please change the file name.", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithCanNotAccessFolder_ReturnError()
        {
            // Arrange
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.ErrCanNotAccessFolder, Message = "Can not access folder." });
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.InsertBanner(new MockFile[1] { (new MockFile(memoryStream, fileName: "abc.png")) }, new InsertUpdateMultipleBannersParam { InputFileNames = "" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("Can not access folder.", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCallWithInsertIsFailed_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.InsertBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "Insert the banner is failed!" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)))
            {
                var result = service.InsertBanner(new MockFile[1] { new MockFile(memoryStream) }, new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });


                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("Insert the banner is failed!", result.Message);
            }
        }

        [Fact]
        public void InsertBanner_WhenCall_InsertSuccessfully()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(new string[] { "abc" });
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.InsertBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)))
            {
                var result = service.InsertBanner(new MockFile[1] { new MockFile(memoryStream) }, new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });


                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateBanner_WhenCallWithSpecialCharacter_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "abc*.png" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.UpdateBanner(new MockFile(memoryStream), param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("A File Name can't contain any of the following characters: \\/:*?\"<>|#%&+", result.Message);
            }
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "test.pdf" };
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".pdf");
            var service = CreateBannerListService(systemFile: systemFile.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(), param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Sorry! We just support these file types : .jpg, .gif, .png, .jpeg<br />", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateMultipleBannersParam { InputFileNames = "" };
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var service = CreateBannerListService(systemFile: systemFile.Object);

            using (var memoryStream = new MemoryStream())
            {
                // Action
                var result = service.UpdateBanner(new MockFile(memoryStream, fileName: "123test123test123test123test123test123test123test123test.png"), param);

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("The maximum length of file name is 50. Please either choose another file name or select another file.", result.Message);
            }
        }

        [Fact]
        public void UpdateBanner_WhenCallWithExistedFile_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.ErrTheFileNameAlreadyExists, Message = "The file name already exists." });
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(), new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The file name already exists.", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithHasInvalidFile_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = Lang.InvalidBannerExtensionFile });
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(null, "", "abc.html"), new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidBannerExtensionFile, result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithHasFileButUpdateFailed_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(s => s.UpdateBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "Updated the banner is failed!" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)))
            {
                var result = service.UpdateBanner(new MockFile(memoryStream), new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });


                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal("Updated the banner is failed!", result.Message);
            }
        }

        [Fact]
        public void UpdateBanner_WhenCallWithHasFile_UpdateSuccessfully()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(new string[] { "abc" });
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(s => s.UpdateBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.LogoImageBase64)))
            {
                var result = service.UpdateBanner(new MockFile(memoryStream), new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });


                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateBanner_WhenCallWithValidParam_UpdateSuccess()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.ErrTheFileNameAlreadyExists, Message = "The file name already exists." });
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "CategoryMedia/abc.png" });
            bannerListRepository.Setup(s => s.UpdateBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(null, new InsertUpdateMultipleBannersParam { InputFileNames = "abc.png" });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithValidParamHasMoveFile_UpdateSuccess()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".png");
            systemFile.Setup(s => s.MoveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "CategoryMedia/abc.png" });
            bannerListRepository.Setup(s => s.UpdateBanner(It.IsAny<InsertUpdateMultipleBannersParam>())).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(new string[] { "abc" });
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(null, new InsertUpdateMultipleBannersParam { InputFileNames = "xyz.png" });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteBanners_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            var param = new DeleteBannerParam { BannerIdList = "123" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(s => s.DeleteBanners(param)).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = service.DeleteBanners(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteBanners_WhenCall_DeleteIsFailed()
        {
            // Arrange
            var param = new DeleteBannerParam { BannerIdList = "123" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(s => s.DeleteBanners(param)).Returns(new ResultInfo { Code = 999, Message = "Delete is failed" });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.DeleteBanners(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Delete is failed", result.Message);
        }

        [Fact]
        public void GetSizeList_WhenCallWithNotSubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new SizeParam();
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetSizeList(param);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetSizeList_WhenCallWithHaveSubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new SizeParam { Category = "Casino", SubCategory = "Casino Bonus" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetSizeList(param);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetSizeList_WhenCallWithHaveEmptySubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new SizeParam { Category = "Casino" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(t => t.GetSizeList(It.IsAny<SizeParam>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem { Id = 456, Name = "320x250" } });
            var service = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = service.GetSizeList(param);

            // Assert
            Assert.Single(result);
        }

        private static BannerListService CreateBannerListService(
            IBannerListRepository bannerListRepository = null,
            IAppSetting appSetting = null,
            ISystemFile systemFile = null,
            IBannerValidationService bannerValidationService = null)
        {
            var fakeBannerListRepository = bannerListRepository ?? Mock.Of<IBannerListRepository>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();
            var fakeSystemFile = systemFile ?? Mock.Of<ISystemFile>();
            var fakeBannerValidationService = bannerValidationService ?? Mock.Of<IBannerValidationService>();

            return new BannerListService(fakeBannerListRepository, fakeAppSetting, fakeSystemFile, fakeBannerValidationService);
        }
    }
}
