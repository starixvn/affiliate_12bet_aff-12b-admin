using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Member;
using Fanex.Affiliate.Admin.Core.MetaModels.Member;
using Fanex.Affiliate.Admin.Core.Repository.Member;
using Fanex.Affiliate.Admin.Core.Services.Member;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Member
{
    public class ActiveMemberServiceTest
    {
        [Fact]
        public void GetAllMembers_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var userNames = "abc";
            string passedParam = null;
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.GetAllMembers(userNames)).Returns(new AllMemberEntities
            {
                ActivedMemeberList = new List<ActiveMemberInfo> { new ActiveMemberInfo() },
                InactiveMemberList = new List<InactiveMemberInfo> { new InactiveMemberInfo() },
                MemberNotExist = ""
            }).Callback<string>(p => passedParam = p);
            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.GetAllMembers(userNames);

            // Assert
            Assert.Single(result.ActivedMemeberList);
            Assert.Single(result.InactiveMemberList);
            Assert.Equal("", result.MemberNotExist);
            Assert.Equal(userNames, passedParam);
        }

        [Fact]
        public void ActiveMember_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.Get12BetPlayersDepositInfo(It.IsAny<string>())).Returns(new List<DepositCustomerInfo> { new DepositCustomerInfo() });
            activeMemberRepository.Setup(t => t.Add12BetPlayerAccountToIBC(It.IsAny<DepositCustomerInfo>())).Returns(new ResultInfo());
            activeMemberRepository.Setup(t => t.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(It.IsAny<ActiveMemberParam>(), It.IsAny<int>()));
            activeMemberRepository.Setup(t => t.Update12BetPlayerStatusToDeposit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new ResultInfo());
            activeMemberRepository.Setup(t => t.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(It.IsAny<ActiveMemberParam>(), It.IsAny<int>()));
            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.ActiveMember(new ActiveMemberParam());

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void ActiveMember_WhenCall_ReturnErrorCode102()
        {
            // Arrange
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.Get12BetPlayersDepositInfo(It.IsAny<string>())).Returns(new List<DepositCustomerInfo> { new DepositCustomerInfo { DepositUserName = "abc" } });
            activeMemberRepository.Setup(t => t.Add12BetPlayerAccountToIBC(It.IsAny<DepositCustomerInfo>())).Returns(new ResultInfo());
            activeMemberRepository.Setup(t => t.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(It.IsAny<ActiveMemberParam>(), It.IsAny<int>()));
            activeMemberRepository.Setup(t => t.Update12BetPlayerStatusToDeposit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new ResultInfo { Code = 102 });
            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.ActiveMember(new ActiveMemberParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("\nabc user already has IBC account", result.Message);
        }

        [Fact]
        public void ActiveMember_WhenCall_ReturnErrorCode101()
        {
            // Arrange
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.Get12BetPlayersDepositInfo(It.IsAny<string>())).Returns(new List<DepositCustomerInfo> { new DepositCustomerInfo { DepositUserName = "abc" } });
            activeMemberRepository.Setup(t => t.Add12BetPlayerAccountToIBC(It.IsAny<DepositCustomerInfo>())).Returns(new ResultInfo());
            activeMemberRepository.Setup(t => t.WriteLogAfterAdd12BetPlayerAccountToIBCSuccefully(It.IsAny<ActiveMemberParam>(), It.IsAny<int>()));
            activeMemberRepository.Setup(t => t.Update12BetPlayerStatusToDeposit(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new ResultInfo { Code = 101 });
            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.ActiveMember(new ActiveMemberParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("\nabc user not found", result.Message);
        }

        [Fact]
        public void ActiveMember_WhenCall_UpdateStatusIsFailed()
        {
            // Arrange
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.Get12BetPlayersDepositInfo(It.IsAny<string>())).Returns(new List<DepositCustomerInfo> { new DepositCustomerInfo { DepositUserName = "abc" } });
            activeMemberRepository.Setup(t => t.Add12BetPlayerAccountToIBC(It.IsAny<DepositCustomerInfo>())).Returns(new ResultInfo { Code = -999, Message = "user not exist" });

            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.ActiveMember(new ActiveMemberParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("\nabc user not exist", result.Message);
        }

        [Fact]
        public void ActiveMember_WhenCall_ReturnEmpty()
        {
            // Arrange
            var activeMemberRepository = new Mock<IActiveMemberRepository>();
            activeMemberRepository.Setup(t => t.Get12BetPlayersDepositInfo(It.IsAny<string>())).Returns(new List<DepositCustomerInfo>());

            var service = CreateActiveMemberService(activeMemberRepository.Object);

            // Action
            var result = service.ActiveMember(new ActiveMemberParam());

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Deposit User Name(s) is incorrect", result.Message);
        }

        private static ActiveMemberService CreateActiveMemberService(IActiveMemberRepository activeMemberRepository = null)
        {
            var fakeActiveMemberRepository = activeMemberRepository ?? Mock.Of<IActiveMemberRepository>();

            return new ActiveMemberService(fakeActiveMemberRepository);
        }
    }
}
