using System.Xml;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.MemberInfoWebService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class MemberInfoServiceWrapperTest
    {
        const string UserName = Constant.DefaultUserName;
        const string TransDate = "7/3/2018";

        [Fact]
        public void GetDepositInfo_WhenCall_ReturnCorrectResult()
        {
            // Arrange
            var expectedResult = CreateXmlNode();

            var serviceSoap = new Mock<ServiceSoap>();
            serviceSoap.Setup(s => s.DepositInfo(UserName, TransDate)).Returns(expectedResult);

            var serviceWrapper = new MemberInfoServiceWrapper(serviceSoap.Object);

            // Action
            var result = serviceWrapper.GetDepositInfo(UserName, TransDate);

            // Assert
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public void GetWithdrawalInfo_WhenCall_ReturnCorrectResult()
        {
            // Arrange
            var expectedResult = CreateXmlNode();

            var serviceSoap = new Mock<ServiceSoap>();
            serviceSoap.Setup(s => s.WithdrawalInfo(UserName, TransDate)).Returns(expectedResult);

            var serviceWrapper = new MemberInfoServiceWrapper(serviceSoap.Object);

            // Action
            var result = serviceWrapper.GetWithdrawalInfo(UserName, TransDate);

            // Assert
            Assert.Equal(expectedResult, result);

        }

        private static XmlNode CreateXmlNode()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Root></Root>");
            return doc.DocumentElement;
        }
    }
}
