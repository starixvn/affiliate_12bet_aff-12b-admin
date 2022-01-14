using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class HtmlFileServiceTest
    {
        [Fact]
        public void HandleUploadFile_WhenCallWithInvalidContentType_ReturnError()
        {
            // Arrange
            var controller = CreateHtmlFileService();

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "image/jpeg"), "abc.jpg", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithInvalidFileSize_ReturnError()
        {
            // Arrange
            var controller = CreateHtmlFileService();

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "", 4 * 1024 * 1024), "abc.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileSize, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithFileNameHasSpecialCharacter_ReturnError()
        {
            // Arrange
            var controller = CreateHtmlFileService();

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "", 1024), "ab*?c.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.FileNameCanNotContain, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithFileNameInvalidExtension_ReturnError()
        {
            // Arrange
            var controller = CreateHtmlFileService();

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "", 1024), "abc.png", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithFileNameInvalidlength_ReturnError()
        {
            // Arrange
            var controller = CreateHtmlFileService();

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "", 1024), "123test123test123test123test123test123test123test123test.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.MaxLenghtOfFileName, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithCannotCreateFolder_ReturnError()
        {
            // Arrange
            var message = "Can not access the folder!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr(message));
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "abc.html", 1024), "abc.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithFileExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUploadFile(new MockFile(null, "text/html", "abc.html", 1024), "abc.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void HandleUploadFile_WhenCallWithUploadFileIsFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr(message));
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[] { 1, 2, 3 }))
            {
                var result = controller.HandleUploadFile(new MockFile(memoryStream, "text/html", "abc.html", 1024), "abc.html", "test");

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void HandleUploadFile_WhenCall_UploadSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.HandleUploadFile(new MockFile(memoryStream, "text/html", "abc.html", 1024), "abc.html", "test");

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void HandleUpdateFile_HandleRenameOldFile_WhenCallWithNewFileNameExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUpdateFile(null, "abc.html", "index.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void HandleUpdateFile_HandleRenameOldFile_WhenCallWithHasMovedFile_UpdateSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUpdateFile(null, "abc.html", "index.html", "test");

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void HandleUpdateFile_HandleReplaceFileByNewFile_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUpdateFile(new MockFile(null, "image/jpeg", "", 1024), "abc.html", "abc.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void HandleUpdateFile_HandleReplaceFileByNewFile_WhenCallWithNewFileNameExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            var result = controller.HandleUpdateFile(new MockFile(null, "text/html", "", 1024), "abc.html", "index.html", "test");

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void HandleUpdateFile_HandleReplaceFileByNewFile_WhenCallWithUploadFileFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr(message));
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.HandleUpdateFile(new MockFile(null, "text/html", "", 1024), "abc.html", "abc.html", "test");

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void HandleUpdateFile_HandleReplaceFileByNewFile_WhenCallWithHaveDeleteFile_UpdateSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.HandleUpdateFile(new MockFile(null, "text/html", "", 1024), "abc.html", "index.html", "test");

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void HandleUpdateFile_HandleReplaceFileByNewFile_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var controller = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.HandleUpdateFile(new MockFile(null, "text/html", "", 1024), "abc.html", "abc.html", "test");

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void DeleteFiles_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            string[] fileNames = { "index.html" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
            var service = CreateHtmlFileService(appSetting.Object, systemFile.Object);

            // Action
            service.DeleteFiles(fileNames, "test");

            // Assert
            Assert.True(true);
        }

        private static HtmlFileService CreateHtmlFileService(
            IAppSetting appSetting = null,
            ISystemFile systemFile = null)
        {
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();
            var fakeSystemFile = systemFile ?? Mock.Of<ISystemFile>();

            return new HtmlFileService(fakeSystemFile, fakeAppSetting);
        }
    }
}
