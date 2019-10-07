using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class BannerListServiceTest
    {
        private const string SpKeyBannerSel = "Aff_BannerSel_1";
        private const string ErrorMessage = "Sorry! We just support these file types : .jpg, .gif, .png, .jpeg";

        [Fact]
        public void GetBannerList_WhenCallWithNotSubCategory_ReturnCorrectData()
        {
            // Arrange
            var param = new BannerParam();
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(b => b.GetBannerList(param, SpKeyBannerSel)).Returns(new EntityResult<BannerInfo> { MainData = new List<BannerInfo> { new BannerInfo() }, TotalItem = 1 });
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
            bannerListRepository.Setup(b => b.GetBannerList(param, SpKeyBannerSel))
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
            bannerListRepository.Setup(b => b.GetBannerList(It.IsAny<BannerParam>(), It.IsAny<string>()))
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
            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.GetBannerDetail(selectedBannerId);

            // Assert
            Assert.Equal(selectedBannerId, result.BannerId);
            Assert.Equal(selectedBannerId, passedParam);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { FileName = "test.abc"};
            var controller = CreateBannerListService();

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { FileName = "test.png", InputFileName = "test.abc" };
            var controller = CreateBannerListService();

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithFileNull_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { FileName = "test.png", InputFileName = "test.png", Category = "Casino", SubCategory = "Slot" };
            var controller = CreateBannerListService();

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("You must browse banner to upload.", result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidExtensionFile_ReturnError()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/BannerListServiceTest.cs");
            byte[] buffer = new byte[16];
            var stream = new Mock<FileStream>(path, FileMode.Open);
            stream.Setup(s => s.Read(buffer, 0, buffer.Length)).Returns(0);
            file.Setup(f => f.InputStream).Returns(stream.Object);
            var param = new InsertUpdateBannerParam { FileName = "test.png", InputFileName = "test.png", File = file.Object };
            var controller = CreateBannerListService();

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithInvalidFileSize_ReturnError()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner.jpg");
            file.Setup(f => f.InputStream).Returns(new FileStream(path, FileMode.Open));
            file.Setup(f => f.ContentLength).Returns(30001);
            var param = new InsertUpdateBannerParam { FileName = "banner.jpg", File = file.Object };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.ImageSize).Returns(300);
            var controller = CreateBannerListService(appSetting: appSetting.Object);

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Your upload file size must be <= 3 MB.", result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithExistPath_ReturnError()
        {
            // Arrange
            string[] bannerFolders = { "../../Services/MediaGallery/TestImages/" };
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/SBNG.gif");
            file.Setup(f => f.InputStream).Returns(new FileStream(path, FileMode.Open));
            var param = new InsertUpdateBannerParam { FileName = "SBNG.gif", File = file.Object };

            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);

            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.InsertBanner(param)).Returns(new ResultInfo());
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The file name already exists.", result.Message);
        }

        [Fact]
        public void InsertBanner_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arran
            string[] bannerFolders = { "../../Services/MediaGallery/TestImages/" };
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner_1.jpg");
            file.Setup(f => f.InputStream).Returns(new FileStream(path, FileMode.Open));
            var param = new InsertUpdateBannerParam { FileName = "banner_1.jpg", File = file.Object };

            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.InsertBanner(param)).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertBanner_WhenCallWithValidParam_InsertIsFalied()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner_2.jpg");
            file.Setup(h => h.InputStream).Returns(new FileStream(path, FileMode.Open));
            var param = new InsertUpdateBannerParam { FileName = "banner_2.jpg", File = file.Object };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.InsertBanner(param)).Returns(new ResultInfo { Code = 999, Message = "Insert is failed!" });

            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.InsertBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Insert is failed!", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidInputFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { InputFileName = "SBNG.abc" };
            var controller = CreateBannerListService();

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithInvalidFileName_ReturnError()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { FileName = "SBNG.abc", InputFileName = "SBNG.gif", Category = "Casino", BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo());

            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(ErrorMessage, result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithUploadNewFile_UpdateSuccessfully()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner_3.jpg");
            file.Setup(h => h.InputStream).Returns(new FileStream(path, FileMode.Open));
            var param = new InsertUpdateBannerParam { InputFileName = "banner_3.jpg", Category = "Casino", File = file.Object, BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo());
            bannerListRepository.Setup(s => s.UpdateBanner(param)).Returns(new ResultInfo());

            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithUploadNewFile_UpdateIsFailed()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner_4.jpg");
            file.Setup(h => h.InputStream).Returns(new FileStream(path, FileMode.Open));
            var param = new InsertUpdateBannerParam { InputFileName = "banner_4.jpg", Category = "Casino", File = file.Object, BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo());
            bannerListRepository.Setup(s => s.UpdateBanner(param)).Returns(new ResultInfo { Code = 999, Message = "Update is failed" });

            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Update is failed", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithUploadNewFile_UploadIsFailed()
        {
            // Arrange
            var file = new Mock<HttpPostedFileBase>();
            var path = Path.GetFullPath("../../Services/MediaGallery/TestImages/banner_5.jpg");
            file.Setup(f => f.InputStream).Returns(new FileStream(path, FileMode.Open));
            file.Setup(f => f.ContentLength).Returns(30001);
            var param = new InsertUpdateBannerParam { InputFileName = "banner_5.jpg", Category = "Casino", File = file.Object, BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo());

            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.ImageSize).Returns(300);
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Your upload file size must be <= 3 MB.", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithMoveFile_UpdateSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "../../Services/MediaGallery/TestImages/" };
            var param = new InsertUpdateBannerParam { InputFileName = "SBNG.gif", Category = "Casino", BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif", Size = "300x250" });
            bannerListRepository.Setup(s => s.UpdateBanner(param)).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithMoveFile_UpdateIsFailed()
        {
            // Arrange
            var param = new InsertUpdateBannerParam { InputFileName = "SBNG.gif", Category = "Casino", BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif", Size = "300x250" });
            bannerListRepository.Setup(s => s.UpdateBanner(param)).Returns(new ResultInfo { Code = 999, Message = "Update is failed" });
            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Update is failed", result.Message);
        }

        [Fact]
        public void UpdateBanner_WhenCallWithMoveFile_ReturnInvalidFilePath()
        {
            // Arrange
            string[] bannerFolders = { "../../Services/MediaGallery/TestImages/" };
            var param = new InsertUpdateBannerParam { InputFileName = "banner_6.jpg", BannerId = 123 };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "CategoryMedia/banner_6.jpg", Size = "300x250" });

            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.UpdateBanner(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The file name already exists.", result.Message);
        }

        [Fact]
        public void DeleteBanners_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "../../Services/MediaGallery/TestImages/" };
            var param = new DeleteBannerParam { BannerIdList = "123" };
            var bannerListRepository = new Mock<IBannerListRepository>();
            bannerListRepository.Setup(s => s.GetBannerDetail(It.IsAny<int>())).Returns(new BannerInfo { FileName = "abc.gif" });
            bannerListRepository.Setup(s => s.DeleteBanners(param)).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var controller = CreateBannerListService(bannerListRepository.Object, appSetting.Object);

            // Action
            var result = controller.DeleteBanners(param);

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
            var controller = CreateBannerListService(bannerListRepository.Object);

            // Action
            var result = controller.DeleteBanners(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Delete is failed", result.Message);
        }

        private static BannerListService CreateBannerListService(
            IBannerListRepository bannerListRepository = null,
            IAppSetting appSetting = null)
        {
            var fakeBannerListRepository = bannerListRepository ?? Mock.Of<IBannerListRepository>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            return new BannerListService(fakeBannerListRepository, fakeAppSetting);
        }
    }
}
