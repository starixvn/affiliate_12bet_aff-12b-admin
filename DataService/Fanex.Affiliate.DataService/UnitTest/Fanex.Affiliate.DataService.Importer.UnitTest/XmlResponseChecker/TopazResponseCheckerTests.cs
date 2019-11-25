using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.FlowGaming.ResponseChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.XmlResponseChecker
{
    public class TopazResponseCheckerTests
    {
        [Theory]
        [InlineData(@"{""status"":""FAIL"",""message"":null,""errors"":[{""field"":""brandPassword"",""error"":""INVALID_VALUE""}]}")]
        [InlineData(@"{""status"":""FAIL"",""message"":""CALL_LIMIT_EXCEEDED"",""errors"":[]}")]
        public void CheckXml_NotCorrectResponse_ThrowThirdPartyException(string response)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(response);
            var checker = new TopazResponseChecker();
            Assert.Throws<ThirdPartyProductException>(() => checker.CheckXml(xml));
        }

        [Theory]
        [InlineData(@"{""status"":""SUCCESS"",""message"":null,""errors"":[{""field"":""brandPassword"",""error"":""INVALID_VALUE""}]}")]
        public void CheckXml_SucessResponse_NotThrowException(string response)
        {
            var xml = XmlHelper.ParseToXDocumentFromJson(response);
            var checker = new TopazResponseChecker();
            checker.CheckXml(xml);
            Assert.True(true);
        }
    }
}
