using Fanex.Affiliate.Admin.Core.DTO.MassAdjustment;
using Fanex.Affiliate.Admin.Core.MetaModels.MassAdjustment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.MassAdjustment;
using Fanex.Affiliate.Admin.Core.UnitTests.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System;
using System.IO;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class MassAdjustmentServiceTest
    {
        [Fact]
        public void UpdateMassAdjusment_WhenCallWithValidFile_CallRepository()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".xlsx");

            var repository = new Mock<IMassAdjustmentRepository>();
            repository.Setup(s => s.UpdateMassAdjusment(It.IsAny<MassAdjustmentParam>())).Returns(new AdjustmentResultInfo());
            var service = CreateMassAdjustmentService(repository.Object, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.ValidFileXlsxBase64)))
            {
                var result = service.UpdateMassAdjusment(new MockFile(memoryStream), new MassAdjustmentParam());


                // Assert
                Assert.True(result.Result.IsSuccessful);
            }
        }

        [Fact]
        public void UpdateMassAdjusment_WhenCallWithInvalidFile_ReturnError()
        {
            // Arrange
            var systemFile = new Mock<ISystemFile>();
            systemFile.Setup(s => s.GetExtension(It.IsAny<string>())).Returns(".xlsx");

            var service = CreateMassAdjustmentService(null, systemFile.Object);

            // Action
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(Constant.InvalidFileXlsxBase64)))
            {
                var result = service.UpdateMassAdjusment(new MockFile(memoryStream), new MassAdjustmentParam());


                // Assert
                Assert.False(result.Result.IsSuccessful);
            }
        }

        private static MassAdjustmentService CreateMassAdjustmentService(
            IMassAdjustmentRepository massAdjustmentRepository = null,
            ISystemFile systemFile = null)
        {
            var fakeMassAdjustmentRepository = massAdjustmentRepository ?? Mock.Of<IMassAdjustmentRepository>();
            var fakeSystemFile = systemFile ?? Mock.Of<ISystemFile>();

            return new MassAdjustmentService(fakeMassAdjustmentRepository, fakeSystemFile);
        }
    }
}
