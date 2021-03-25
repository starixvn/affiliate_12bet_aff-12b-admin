using Fanex.Affiliate.Admin.Core.Controllers.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MassAdjustment;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Affiliate
{
    public class MassAdjustmentControllerTest
    {
        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateAdjustmentController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Affiliate/MassAdjustment.cshtml", viewName);
        }

        [Fact]
        public void UploadFile_WhenCallWithFileIsNull_ReturnError()
        {
            // Arrange
            var controller = CreateAdjustmentController();

            // Action
            var result = controller.UploadFile(null) as JsonNetResult;
            var resultInfo = result.Data as AdjustmentResultInfo;

            // Assert
            Assert.False(resultInfo.Result.IsSuccessful);
            Assert.Equal("You must browse file to upload!", resultInfo.Result.Message);
        }

        [Fact]
        public void UploadFile_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var massAdjustmentService = new Mock<IMassAdjustmentService>();
            massAdjustmentService.Setup(s => s.UpdateMassAdjusment(It.IsAny<HttpPostedFileBase>(), It.IsAny<MassAdjustmentParam>())).Returns(new AdjustmentResultInfo { Result = new ResultInfo() });
            var controller = CreateAdjustmentController(null, massAdjustmentService.Object);

            // Action
            using (var memoryStream = new MemoryStream(new byte[3] { 1, 2, 3 }))
            {
                var result = controller.UploadFile(new MockFile(memoryStream)) as JsonNetResult;
                var resultInfo = result.Data as AdjustmentResultInfo;

                // Assert
                Assert.True(resultInfo.Result.IsSuccessful);
            }
        }

        private AdjustmentController CreateAdjustmentController(
            ISessionService sessionService = null,
            IMassAdjustmentService massAdjustmentService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeMassAdjustmentService = massAdjustmentService ?? Mock.Of<IMassAdjustmentService>();

            var controller = new AdjustmentController(fakeSessionService, fakeMassAdjustmentService);

            var request = MockingHelper.GetHttpRequest();

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}
