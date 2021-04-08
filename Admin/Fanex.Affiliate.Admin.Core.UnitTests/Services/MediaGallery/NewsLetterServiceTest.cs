using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class NewsLetterServiceTest
    {
        [Fact]
        public void GetNewsLetters_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new SelectNewsLetterParam();
            SelectNewsLetterParam passedParam = null;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetters(param)).Returns(new EntityResult<NewsLetter> { MainData = new List<NewsLetter> { new NewsLetter() }, TotalItem = 1 }).Callback<SelectNewsLetterParam>(p => passedParam = p);
            var service = CreateNewsLetterService(newsLetterRepository.Object);

            // Action
            var result = service.GetNewsLetters(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetNewsLetterById_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedNewsLetterId = 123;
            var passedParam = 0;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(t => t.GetNewsLetterById(selectedNewsLetterId)).Returns(new NewsLetter { NewsLetterId = 123 }).Callback<int>(p => passedParam = p);
            var controller = CreateNewsLetterService(newsLetterRepository: newsLetterRepository.Object);

            // Action
            var result = controller.GetNewsLetterById(selectedNewsLetterId);

            // Assert
            Assert.Equal(selectedNewsLetterId, result.NewsLetterId);
            Assert.Equal(selectedNewsLetterId, passedParam);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithInvalidContentType_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "image/jpeg"), new InsertNewsLetterParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithInvalidFileSize_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "", 4 * 1024 * 1024), new InsertNewsLetterParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileSize, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithFileNameHasSpecialCharacter_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "", 1024), new InsertNewsLetterParam { FileName = "ab*?c.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.FileNameCanNotContain, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithFileNameInvalidExtension_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "", 1024), new InsertNewsLetterParam { FileName = "abc.png" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithFileNameInvalidlength_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "", 1024), new InsertNewsLetterParam { FileName = "123test123test123test123test123test123test123test123test.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.MaxLenghtOfFileName, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithCannotCreateFolder_ReturnError()
        {
            // Arrange
            var message = "Can not access the folder!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = message });
            var controller = CreateNewsLetterService(null, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithFileExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode });
            var controller = CreateNewsLetterService(null, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithUploadFileIsFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = message });
            var controller = CreateNewsLetterService(null, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithInsertToDBIsFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertNewsLetterParam>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = message });
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithInsertToDBHasException_ReturnError()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertNewsLetterParam>())).Throws(new Exception());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(Lang.ErrGeneral, result.Message);
            }
        }

        [Fact]
        public void AddNewsLetter_WhenCall_AddSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithInvalidFileName_ReturnError()
        {
            // Arrange
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.png" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithInvalidOldNewsLetter_ReturnError()
        {
            // Arrange
            NewsLetter newsLetter = null;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService();

            // Action
            var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidId, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateOldNewsLetter_WhenCallWithNewFileNameExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode });
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.UpdateNewsLetter(null, new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateOldNewsLetter_WhenCallWithHasMovedFile_UpdateSuccessfully()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html"};
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            newsLetterRepository.Setup(s => s.UpdateNewsLetter(It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.UpdateNewsLetter(null, new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateOldNewsLetter_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            newsLetterRepository.Setup(s => s.UpdateNewsLetter(It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.UpdateNewsLetter(null, new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateNewsLetterWithANewFile_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode });
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.UpdateNewsLetter(new MockFile(null, "image/jpeg", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateNewsLetterWithANewFile_WhenCallWithNewFileNameExisted_ReturnError()
        {
            // Arrange
            var message = "The html file name exist already, please choose another name.";
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode });
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_UpdateNewsLetterWithANewFile_WhenCallWithUploadFileFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo { Code = Code.GeneralErrorCode, Message = message });
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void UpdateNewsLetter_UpdateNewsLetterWithANewFile_WhenCallWithHaveDeleteFile_UpdateSuccessfully()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            newsLetterRepository.Setup(s => s.UpdateNewsLetter(It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateNewsLetter_UpdateNewsLetterWithANewFile_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc.html" };
            string[] bannerFolders = { "test" };
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(s => s.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.CreateFolder(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.CheckExistedFile(It.IsAny<string>(), It.IsAny<string>())).Returns(new ResultInfo());
            systemFile.Setup(s => s.UploadFile(It.IsAny<Stream>(), It.IsAny<string>())).Returns(new ResultInfo());
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            newsLetterRepository.Setup(s => s.UpdateNewsLetter(It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void DeleteNewsLetters_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            var param = new DeleteNewsLetterParam { NewsLetterIds = "123" };
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(new NewsLetter { FileName = "abc.html" });
            newsLetterRepository.Setup(s => s.DeleteNewsLetter(param)).Returns(new ResultInfo());
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.BannerFolders).Returns(bannerFolders);
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));
            var service = CreateNewsLetterService(newsLetterRepository.Object, appSetting.Object, systemFile.Object);

            // Action
            var result = service.DeleteNewsLetters(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteNewsLetters_WhenCall_DeleteIsFailed()
        {
            // Arrange
            var param = new DeleteNewsLetterParam { NewsLetterIds = "123" };
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(new NewsLetter { FileName = "abc.html" });
            newsLetterRepository.Setup(s => s.DeleteNewsLetter(param)).Returns(new ResultInfo { Code = 999, Message = "Delete is failed" });
            var service = CreateNewsLetterService(newsLetterRepository.Object);

            // Action
            var result = service.DeleteNewsLetters(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Delete is failed", result.Message);
        }

        private static NewsLetterService CreateNewsLetterService(
            INewsLetterRepository newsLetterRepository = null,
            IAppSetting appSetting = null,
            ISystemFile systemFile = null)
        {
            var fakeNewsLetterRepository = newsLetterRepository ?? Mock.Of<INewsLetterRepository>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();
            var fakeSystemFile = systemFile ?? Mock.Of<ISystemFile>();

            return new NewsLetterService(fakeNewsLetterRepository, fakeSystemFile, fakeAppSetting);
        }
    }
}
