using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class NewsLetterServiceTest
    {
        [Fact]
        public void GetNewsLetters_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam { PageIndex = 1, PageSize = 20 };
            PagingParam passedParam = null;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetters(param)).Returns(new EntityResult<NewsLetter> { MainData = new List<NewsLetter> { new NewsLetter() }, TotalItem = 1 }).Callback<PagingParam>(p => passedParam = p);
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
        public void AddNewsLetter_WhenCallWithInvalid_ReturnError()
        {
            // Arrange
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateNewsLetterService(null, htmlFileService.Object);

            // Action
            var result = controller.AddNewsLetter(new MockFile(null, "image/jpeg"), new InsertHtmlFileParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);
        }

        [Fact]
        public void AddNewsLetter_WhenCallWithInsertToDBIsFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertHtmlFileParam>())).Returns(ResultInfo.GenerateErr(message));
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

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
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertHtmlFileParam>())).Throws(new Exception());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

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
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.InsertNewsLetter(It.IsAny<InsertHtmlFileParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.AddNewsLetter(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithInvalidFileName_ReturnError()
        {
            // Arrange
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.GenerateErr(Lang.InvalidFileType));
            var controller = CreateNewsLetterService(null, htmlFileService.Object);

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
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            NewsLetter newsLetter = null;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

            // Action
            var result = controller.UpdateNewsLetter(new MockFile(null, "text/html", "", 1024), new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidId, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCallWithUpdateFileIsFailed_ReturnError()
        {
            // Arrange
            var message = "Update file is failed!";
            NewsLetter newsLetter = new NewsLetter { FileName = "abc123.html" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            htmlFileService.Setup(h => h.HandleUpdateFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr(message));
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

            // Action
            var result = controller.UpdateNewsLetter(null, new UpdateNewsLetterParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateNewsLetter_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            NewsLetter newsLetter = new NewsLetter { FileName = "abc.html" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            htmlFileService.Setup(h => h.HandleUpdateFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(s => s.GetNewsLetterById(It.IsAny<int>())).Returns(newsLetter);
            newsLetterRepository.Setup(s => s.UpdateNewsLetter(It.IsAny<UpdateNewsLetterParam>())).Returns(new ResultInfo());
            var controller = CreateNewsLetterService(newsLetterRepository.Object, htmlFileService.Object);

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
            var service = CreateNewsLetterService(newsLetterRepository.Object);

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
            IHtmlFileService htmlFileService = null)
        {
            var fakeNewsLetterRepository = newsLetterRepository ?? Mock.Of<INewsLetterRepository>();
            var fakeHtmlFileService = htmlFileService ?? Mock.Of<IHtmlFileService>();

            return new NewsLetterService(fakeNewsLetterRepository, fakeHtmlFileService);
        }
    }
}
