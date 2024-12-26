using Fanex.Affiliate.Admin.Core.Commands.Member;
using Fanex.Affiliate.Admin.Core.Criterias.Member;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Services.Member;
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
            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateMember_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var param = new UpdateMemberParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(comm => comm.Execute(It.IsAny<UpdateMemberCommand>(), It.IsAny<string>()));
            var service = CreateMemberService(dynamicRepository.Object);

            // Action
            var result = service.UpdateMember(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private static MemberService CreateMemberService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new MemberService(fakedynamicRepository);
        }
    }
}
