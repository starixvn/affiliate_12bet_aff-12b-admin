using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class CustomerMemberServiceTest
    {
        [Fact]
        public void GetCustomerMemberList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<CustomerMember>(It.IsAny<BaseCriteria>())).Returns(new List<CustomerMember> { new CustomerMember { RegisteredDate = new DateTime(2020, 04, 15) } });

            // Action
            var service = new CustomerMemberService(repository.Object);
            var result = service.GetCustomerMember(new CustomerMemberParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Equal("04/15/2020 00:00:00", item.RegisteredDateString);
        }

        [Fact]
        public void GetCustomerMemberList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<CustomerMember>(It.IsAny<BaseCriteria>())).Returns(new List<CustomerMember> { new CustomerMember() });

            // Action
            var service = new CustomerMemberService(repository.Object);
            var result = service.GetCustomerMember(new CustomerMemberParam());
            var item = result.MainData.SingleOrDefault();

            // Assert
            Assert.Single(result.MainData);
            Assert.Null(item.RegisteredDateString);
        }
    }
}
