using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class SpecificDomainControlServiceTest
    {
        private readonly Mock<IDynamicRepository> _dynamicRepositoryMock;
        private readonly SpecificDomainControlService _service;

        public SpecificDomainControlServiceTest()
        {
            _dynamicRepositoryMock = new Mock<IDynamicRepository>();
            _service = new SpecificDomainControlService(_dynamicRepositoryMock.Object);
        }

        [Fact]
        public void GetSpecificDomainControl_WhenCall_ReturnDomainControlList()
        {
            // Arrange
            var param = new SpecificDomainControlParam();
            var expectedResult = new List<SpecificDomainControlInfo> { new SpecificDomainControlInfo() };

            _dynamicRepositoryMock.Setup(x => x.Fetch<SpecificDomainControlInfo>(It.IsAny<ICriteria>(), It.IsAny<string>()))
                .Returns(expectedResult);

            // Act
            var result = _service.GetSpecificDomainControl(param);

            // Assert
            Assert.Equal(expectedResult, result.MainData);
            _dynamicRepositoryMock.Verify(x => x.Fetch<SpecificDomainControlInfo>(It.IsAny<ICriteria>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetSpecificDomainControlById_WhenCall_ReturnDomainControl()
        {
            // Arrange
            var id = 1;
            var expectedResult = new SpecificDomainControlInfo { SpecificDomainControlId = id };

            _dynamicRepositoryMock.Setup(x => x.Fetch<SpecificDomainControlInfo>(It.IsAny<ICriteria>(), It.IsAny<string>()))
                .Returns(new List<SpecificDomainControlInfo>() { expectedResult });

            // Act
            var result = _service.GetSpecificDomainControlById(id);

            // Assert
            Assert.Equal(expectedResult, result);
            _dynamicRepositoryMock.Verify(x => x.Fetch<SpecificDomainControlInfo>(It.IsAny<ICriteria>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void InsertSpecificDomainControl_WhenAffNamesExceedMaximum_ReturnError()
        {
            // Arrange
            var param = new InsertSpecificDomainControlParam
            {
                AffUsername = string.Join(",", new string[501]) // Create a string with 501 commas
            };

            // Act
            var result = _service.InsertSpecificDomainControl(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("The function can only create assignment for up to 500 affiliates per request!", result.Message);
            _dynamicRepositoryMock.Verify(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void InsertSpecificDomainControl_WhenValidParam_ReturnSuccess()
        {
            // Arrange
            var param = new InsertSpecificDomainControlParam
            {
                AffUsername = "aff1,aff2",
                AffDomain = "test.com",
                AffDomainIsSSL = true,
                RedirectDomain = "redirect.com",
                RedirectDomainIsSSL = true,
                Status = true,
                UserId = 1,
                Ip = "127.0.0.1"
            };

            var expectedResult = new ResultInfo();

            _dynamicRepositoryMock.Setup(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()));

            // Act
            var result = _service.InsertSpecificDomainControl(param);

            // Assert
            Assert.True(result.IsSuccessful);
            _dynamicRepositoryMock.Verify(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void UpdateSpecificDomainControl_WhenCall_ReturnSuccess()
        {
            // Arrange
            var param = new UpdateSpecificDomainControlParam
            {
                SpecificDomainControlId = 1,
                AffUsername = "aff1,aff2",
                AffDomain = "test.com",
                AffDomainIsSSL = true,
                RedirectDomain = "redirect.com",
                RedirectDomainIsSSL = true,
                Status = true,
                UserId = 1,
                Ip = "127.0.0.1"
            };

            var expectedResult = new ResultInfo();

            _dynamicRepositoryMock.Setup(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()));

            // Act
            var result = _service.UpdateSpecificDomainControl(param);

            // Assert
            Assert.True(result.IsSuccessful);
            _dynamicRepositoryMock.Verify(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeleteSpecificDomainControl_WhenCall_ReturnSuccess()
        {
            // Arrange
            var param = new DeleteSpecificDomainControlParam
            {
                SpecificDomainControlId = 1,
                UserId = 1,
                Ip = "127.0.0.1"
            };

            var expectedResult = new ResultInfo();

            _dynamicRepositoryMock.Setup(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()));

            // Act
            var result = _service.DeleteSpecificDomainControl(param);

            // Assert
            Assert.True(result.IsSuccessful);
            _dynamicRepositoryMock.Verify(x => x.Execute(It.IsAny<INonQueryCommand>(), It.IsAny<string>()), Times.Once);
        }
    }
}