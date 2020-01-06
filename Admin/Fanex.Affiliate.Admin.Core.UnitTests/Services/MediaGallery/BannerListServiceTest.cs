using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class BannerListServiceTest
    {
        private const string ErrorMessage = "Sorry! We just support these file types : .jpg, .gif, .png, .jpeg";

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

            // Action
            var result = service.InsertBanner(new MockFile(stream: new MemoryStream()), new InsertUpdateBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { InputFileName = "test.pdf" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".pdf");
            var service = CreateBannerListService(bannerValidationService: bannerValidationService.Object, systemFile: systemFile.Object);

            // Action
            var result = service.InsertBanner(new MockFile(stream: new MemoryStream()), param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
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

            // Action
            var result = service.InsertBanner(new MockFile(stream: new MemoryStream()), new InsertUpdateBannerParam { InputFileName = "abc.gif"});

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The file name already exists.", result.Message);
        }

        //[Fact]
        //public void InsertBanner_WhenCallWithValidParam_InsertSuccessfully()
        //{
        //    // Arrange
        //    string[] bannerFolders = { "abc" };
        //    var bannerValidationService = new Mock<IBannerValidationService>();
        //    bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
        //    bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
        //    var bannerListRepository = new Mock<IBannerListRepository>();
        //    bannerListRepository.Setup(b => b.InsertBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo());
        //    var appSetting = new Mock<IAppSetting>();
        //    appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
        //    var systemFile = new Mock<ISystemFile>();
        //    systemFile.Setup(s => s.UploadFile(It.IsAny<Bitmap>(), It.IsAny<string>()));
        //    var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

        //    // Action
        //    var result = service.InsertBanner(new MockFile(stream: new MemoryStream()), new InsertUpdateBannerParam());

        //    // Assert
        //    Assert.True(result.IsSuccessful);
        //}

        //[Fact]
        //public void InsertBanner_WhenCallWithValidParam_InsertIsFalied()
        //{
        //    // Arrange
        //    string[] bannerFolders = { "abc" };
        //    var bannerValidationService = new Mock<IBannerValidationService>();
        //    bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
        //    bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
        //    var bannerListRepository = new Mock<IBannerListRepository>();
        //    bannerListRepository.Setup(s => s.InsertBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo { Code = 999, Message = "Insert is failed!" });
        //    var appSetting = new Mock<IAppSetting>();
        //    appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
        //    var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, bannerValidationService: bannerValidationService.Object);

        //    // Action
        //    var result = service.InsertBanner(new MockFile(stream: new MemoryStream()), new InsertUpdateBannerParam());

        //    // Assert
        //    Assert.False(result.IsSuccessful);
        //    Assert.Equal("Insert is failed!", result.Message);
        //}

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { InputFileName = "test.pdf" };
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".pdf");
            var service = CreateBannerListService(systemFile: systemFile.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(), param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithExistedFile_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "The file name already exists." });
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(), new InsertUpdateBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The file name already exists.", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");

            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = ErrorMessage});

            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            var service = CreateBannerListService(bannerListRepository.Object, null, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(new MockFile(stream: new MemoryStream()), new InsertUpdateBannerParam { SubCategory = "Slot", Category = "Casino" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        //[Fact]
        //public void UpdateBanner_WhenCallWithUploadNewFile_UpdateSuccessfully()
        //{
        //    // Arrange
        //    string[] bannerFolders = { "abc" };
        //    var bannerValidationService = new Mock<IBannerValidationService>();
        //    bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
        //    bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
        //    var bannerListRepository = new Mock<IBannerListRepository>();
        //    bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
        //    bannerListRepository.Setup(b => b.UpdateBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo());
        //    var appSetting = new Mock<IAppSetting>();
        //    appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
        //    var systemFile = new Mock<ISystemFile>();
        //    systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
        //    systemFile.Setup(s => s.UploadFile(It.IsAny<Bitmap>(), It.IsAny<string>()));
        //    systemFile.Setup(s => s.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
        //    var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

        //    // Action
        //    var result = service.UpdateBanner(new MockFile(), new InsertUpdateBannerParam());

        //    // Assert
        //    Assert.True(result.IsSuccessful);
        //}

        //[Fact]
        //public void UpdateBanner_WhenCallWithUploadNewFile_UpdateIsFailed()
        //{
        //    // Arrange
        //    string[] bannerFolders = { "abc" };
        //    var bannerValidationService = new Mock<IBannerValidationService>();
        //    bannerValidationService.Setup(b => b.ValidateFile(It.IsAny<string>(), It.IsAny<int>())).Returns(new ResultInfo());
        //    bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
        //    var bannerListRepository = new Mock<IBannerListRepository>();
        //    bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
        //    bannerListRepository.Setup(b => b.UpdateBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "Update is failed" });
        //    var appSetting = new Mock<IAppSetting>();
        //    appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
        //    var systemFile = new Mock<ISystemFile>();
        //    systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
        //    systemFile.Setup(s => s.UploadFile(It.IsAny<Bitmap>(), It.IsAny<string>()));
        //    systemFile.Setup(s => s.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
        //    var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

        //    // Action
        //    var result = service.UpdateBanner(new MockFile(), new InsertUpdateBannerParam());

        //    // Assert
        //    Assert.False(result.IsSuccessful);
        //    Assert.Equal("Update is failed", result.Message);
        //}

        [Fact]
        public void UpdateBanner_WhenCallWithMoveFile_UpdateSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(b => b.UpdateBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            systemFile.Setup(s => s.MoveFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(null, new InsertUpdateBannerParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithMoveFile_UpdateIsFailed()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            var bannerValidationService = new Mock<IBannerValidationService>();
            bannerValidationService.Setup(b => b.CheckFileInFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(b => b.UpdateBanner(It.IsAny<InsertUpdateBannerParam>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = "Update is failed" });
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".gif");
            var service = CreateBannerListService(bannerListRepository.Object, appSetting.Object, systemFile.Object, bannerValidationService.Object);

            // Action
            var result = service.UpdateBanner(null, new InsertUpdateBannerParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Update is failed", result.Message);
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
