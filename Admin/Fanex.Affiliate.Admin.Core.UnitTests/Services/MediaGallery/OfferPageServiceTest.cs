using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
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
using System.Web;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class OfferPageServiceTest
    {
        [Fact]
        public void GetValidEndDateOfferPages_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var DateTimeHelper = new Mock<IDateTimeHelper>();
            DateTimeHelper.Setup(d => d.GetNowDateTime()).Returns(new DateTime(2021, 12, 12));
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageList(It.IsAny<PagingParam>())).Returns(new EntityResult<OfferPage> { MainData = new List<OfferPage> { new OfferPage { EffectiveToDate = new DateTime(2022, 01, 01)} }, TotalItem = 1 });
            var service = CreateOfferPageService(OfferPageRepository.Object, null, DateTimeHelper.Object);

            // Action
            var result = service.GetValidEndDateOfferPages();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetOfferPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new PagingParam { PageIndex = 1, PageSize = 20 };
            PagingParam passedParam = null;
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageList(param)).Returns(new EntityResult<OfferPage> { MainData = new List<OfferPage> { new OfferPage() }, TotalItem = 1 }).Callback<PagingParam>(p => passedParam = p);
            var service = CreateOfferPageService(OfferPageRepository.Object);

            // Action
            var result = service.GetOfferPageList(param);

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetOfferPageById_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var selectedPageId = 123;
            var passedParam = 0;
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(t => t.GetOfferPageById(selectedPageId)).Returns(new OfferPage { PageId = 123 }).Callback<int>(p => passedParam = p);
            var controller = CreateOfferPageService(OfferPageRepository: OfferPageRepository.Object);

            // Action
            var result = controller.GetOfferPageById(selectedPageId);

            // Assert
            Assert.Equal(selectedPageId, result.PageId);
            Assert.Equal(selectedPageId, passedParam);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInvalid_ReturnError()
        {
            // Arrange
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr());
            var controller = CreateOfferPageService(null, htmlFileService.Object);

            // Action
            var result = controller.InsertOfferPage(new MockFile(null, "image/jpeg"), new InsertHtmlFileParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInsertToDBIsFailed_ReturnError()
        {
            // Arrange
            var message = "Upload the file is failed!";
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.InsertOfferPage(It.IsAny<InsertHtmlFileParam>())).Returns(ResultInfo.GenerateErr(message));
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.InsertOfferPage(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(message, result.Message);
            }
        }

        [Fact]
        public void InsertOfferPage_WhenCallWithInsertToDBHasException_ReturnError()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.InsertOfferPage(It.IsAny<InsertHtmlFileParam>())).Throws(new Exception());
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.InsertOfferPage(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

                // Assert
                Assert.False(result.IsSuccessful);
                Assert.Equal(Lang.ErrGeneral, result.Message);
            }
        }

        [Fact]
        public void InsertOfferPage_WhenCall_AddSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "test" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.HandleUploadFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.InsertOfferPage(It.IsAny<InsertHtmlFileParam>())).Returns(new ResultInfo());
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.InsertOfferPage(new MockFile(memoryStream, "text/html", "abc.html", 1024), new InsertHtmlFileParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithInvalidFileName_ReturnError()
        {
            // Arrange
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.GenerateErr(Lang.InvalidFileType));
            var controller = CreateOfferPageService(null, htmlFileService.Object);

            // Action
            var result = controller.UpdateOfferPage(new MockFile(null, "text/html", "", 1024), new UpdateOfferPageParam { FileName = "abc.png" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidFileType, result.Message);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithInvalidOldOfferPage_ReturnError()
        {
            // Arrange
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            OfferPage OfferPage = null;
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageById(It.IsAny<int>())).Returns(OfferPage);
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            var result = controller.UpdateOfferPage(new MockFile(null, "text/html", "", 1024), new UpdateOfferPageParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(Lang.InvalidId, result.Message);
        }

        [Fact]
        public void UpdateOfferPage_WhenCallWithUpdateFileIsFailed_ReturnError()
        {
            // Arrange
            var message = "Update file is failed!";
            OfferPage OfferPage = new OfferPage { FileName = "abc123.html" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            htmlFileService.Setup(h => h.HandleUpdateFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.GenerateErr(message));
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageById(It.IsAny<int>())).Returns(OfferPage);
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            var result = controller.UpdateOfferPage(null, new UpdateOfferPageParam { FileName = "abc.html" });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void UpdateOfferPage_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            OfferPage OfferPage = new OfferPage { FileName = "abc.html" };
            var htmlFileService = new Mock<IHtmlFileService>();
            htmlFileService.Setup(h => h.ValidateFileName(It.IsAny<string>())).Returns(ResultInfo.Success);
            htmlFileService.Setup(h => h.HandleUpdateFile(It.IsAny<HttpPostedFileBase>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(ResultInfo.Success);
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageById(It.IsAny<int>())).Returns(OfferPage);
            OfferPageRepository.Setup(s => s.UpdateOfferPage(It.IsAny<UpdateOfferPageParam>())).Returns(new ResultInfo());
            var controller = CreateOfferPageService(OfferPageRepository.Object, htmlFileService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UpdateOfferPage(new MockFile(null, "text/html", "", 1024), new UpdateOfferPageParam { FileName = "abc.html" });

                // Assert
                Assert.True(result.IsSuccessful);
            }
        }

        [Fact]
        public void DeleteOfferPages_WhenCall_DeleteSuccessfully()
        {
            // Arrange
            string[] bannerFolders = { "abc" };
            var param = new DeleteOfferPageParam { PageIds = "123" };
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageById(It.IsAny<int>())).Returns(new OfferPage { FileName = "abc.html" });
            OfferPageRepository.Setup(s => s.DeleteOfferPage(param)).Returns(new ResultInfo());
            var service = CreateOfferPageService(OfferPageRepository.Object);

            // Action
            var result = service.DeleteOfferPages(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteOfferPages_WhenCall_DeleteIsFailed()
        {
            // Arrange
            var param = new DeleteOfferPageParam { PageIds = "123" };
            var OfferPageRepository = new Mock<IOfferPageRepository>();
            OfferPageRepository.Setup(s => s.GetOfferPageById(It.IsAny<int>())).Returns(new OfferPage { FileName = "abc.html" });
            OfferPageRepository.Setup(s => s.DeleteOfferPage(param)).Returns(new ResultInfo { Code = 999, Message = "Delete is failed" });
            var service = CreateOfferPageService(OfferPageRepository.Object);

            // Action
            var result = service.DeleteOfferPages(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Delete is failed", result.Message);
        }

        private static OfferPageService CreateOfferPageService(
            IOfferPageRepository OfferPageRepository = null,
            IHtmlFileService htmlFileService = null,
            IDateTimeHelper dateTimeHelper = null)
        {
            var fakeOfferPageRepository = OfferPageRepository ?? Mock.Of<IOfferPageRepository>();
            var fakeHtmlFileService = htmlFileService ?? Mock.Of<IHtmlFileService>();
            var fakeDateTimeHelper = dateTimeHelper ?? Mock.Of<IDateTimeHelper>();

            return new OfferPageService(fakeHtmlFileService, fakeOfferPageRepository, fakeDateTimeHelper);
        }
    }
}
