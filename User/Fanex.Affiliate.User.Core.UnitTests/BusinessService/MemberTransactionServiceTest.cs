using System.Collections.Generic;
using System.Text;
using System.Xml;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class MemberTransactionServiceTest
    {
        [Fact]
        public void GetTransactionInfo_HasError_ReturnEmptyTransactionList()
        {
            // Arrange
            var xmlContent = CreateXmlContent(1);
            var xmlNode = CreateXmlData(xmlContent);

            var param = new MemberTransactionParam
            {
                UserName = Constant.DefaultUserName,
                Date = new System.DateTime(2018, 7, 3),
                IsDeposit = true
            };
            var serviceWrapper = new Mock<IMemberInfoServiceWrapper>();
            serviceWrapper.Setup(s => s.GetDepositInfo(param.UserName, param.Date.Value.ToShortDateString())).Returns(xmlNode);

            var service = new MemberTransactionService(serviceWrapper.Object);

            // Action
            var result = service.GetTransactionInfo(param);

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetTransactionInfo_WhenCallSuccessfullyWithDeposit_ReturnDepositTransactionList(bool isDeposit)
        {
            // Arrange
            var xmlContent = CreateXmlContent(0, isDeposit);
            var xmlNode = CreateXmlData(xmlContent);

            var param = new MemberTransactionParam
            {
                UserName = Constant.DefaultUserName,
                Date = new System.DateTime(2018, 7, 3),
                IsDeposit = isDeposit
            };
            var serviceWrapper = new Mock<IMemberInfoServiceWrapper>();
            if (isDeposit)
            {
                serviceWrapper.Setup(s => s.GetDepositInfo(param.UserName, param.Date.Value.ToShortDateString())).Returns(xmlNode);
            }
            else
            {
                serviceWrapper.Setup(s => s.GetWithdrawalInfo(param.UserName, param.Date.Value.ToShortDateString())).Returns(xmlNode);
            }

            var service = new MemberTransactionService(serviceWrapper.Object);

            var transactionList = CreateExpectedTransactionList();
            var expectedResult = JsonConvert.SerializeObject(transactionList);

            // Action
            var result = JsonConvert.SerializeObject(service.GetTransactionInfo(param));

            // Assert
            Assert.Equal(expectedResult, result);
        }

        private static XmlNode CreateXmlData(string xmlContent = null)
        {
            xmlContent = xmlContent ?? CreateXmlContent();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            return doc.DocumentElement;
        }

        private static string CreateXmlContent(int returnCode = 0, bool isDeposit = true)
        {
            var elementName = isDeposit ? "DepositInfo" : "WithdrawalInfo";
            var builder = new StringBuilder();
            builder.Append("<Root>");
            builder.Append($"<ReturnCode>{returnCode}</ReturnCode>");
            builder.Append($"<{elementName}>");
            builder.Append("<TransactionID>123</TransactionID>");
            builder.Append("<Amount>100.25</Amount>");
            builder.Append("<TransactionDate>7/3/2018</TransactionDate>");
            builder.Append("<Status>Status</Status>");
            builder.Append("<AccountNO>567</AccountNO>");
            builder.Append("<NameOfBank>ACB</NameOfBank>");
            builder.Append("<BankAddress>QTSC</BankAddress>");
            builder.Append("<BankCountry>VN</BankCountry>");
            builder.Append("<BankCityTown>HCM</BankCityTown>");
            builder.Append("<Currency>USD</Currency>");
            builder.Append($"</{elementName}>");
            builder.Append("</Root>");

            return builder.ToString();
        }

        private IEnumerable<Transaction> CreateExpectedTransactionList()
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = "123",
                    OriginalAmount = "100.25",
                    OriginalTransactionDate = "7/3/2018",
                    Status = "Status",
                    AccountNo = "567",
                    NameOfBank = "ACB",
                    BankAddress = "QTSC",
                    BankCountry = "VN",
                    BankCityTown = "HCM",
                    Currency = "USD"
                }
            };
        }
    }
}
