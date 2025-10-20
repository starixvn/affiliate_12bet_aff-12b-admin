using Fanex.Affiliate.Admin.Core.Commands.Member;
using Fanex.Affiliate.Admin.Core.Criterias.Member;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Member
{
    public class MemberServiceTest
    {
        [Fact]
        public void GetMemberList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new MemberParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Fetch<MemberInfo>(It.IsAny<MemberCriteria>(), It.IsAny<string>())).Returns(new List<MemberInfo> { new MemberInfo() });
            var service = CreateMemberService(dynamicRepository.Object);

            // Action
            var result = service.GetMemberList(param);

            // Assert
            dynamicRepository.Verify(d => d.Fetch<MemberInfo>(It.Is<MemberCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetMemberDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Get<PlayerActionInfo>(It.IsAny<MemberDetailCriteria>(), It.IsAny<string>())).Returns(new PlayerActionInfo());
            var service = CreateMemberService(dynamicRepository.Object);

            // Action
            var result = service.GetMemberDetail(123);

            // Assert
            dynamicRepository.Verify(d => d.Get<PlayerActionInfo>(It.Is<MemberDetailCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateMember_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var param = new UpdateMemberParam
            {
                FromDate = DateTimeHelper.CreateDateTime(2025, 5, 14)
            };
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<UpdateMemberCommand>(), It.IsAny<string>()));
            dynamicRepository.Setup(comm => comm.Get<LatestBillingDate>(It.IsAny<LatestBillingDateCriteria>(), It.IsAny<string>())).Returns(new LatestBillingDate { SValue = DateTimeHelper.CreateDateTime(2025, 4, 30) });
            var service = CreateMemberService(dynamicRepository.Object);

            // Action
            var result = service.UpdateMember(param);

            // Assert
            dynamicRepository.Verify(d => d.Execute(It.Is<UpdateMemberCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        private static MemberService CreateMemberService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new MemberService(fakedynamicRepository);
        }
    }
}
