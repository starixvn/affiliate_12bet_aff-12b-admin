using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Affiliate.DataService.Importer.LBKeno;
using System.Xml.Linq;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.XmlResponseChecker
{
    public class LBKenoXmlCheckerTests
    {
        [Theory]
        [InlineData("40.01", "status_code")]
        [InlineData("1111", "status_code")]
        [InlineData("0", "status")]
        public void CheckXml_WhenCallWithWrongStatusCode_ThrowException(string code, string statusEle)
        {
            var xml = new XDocument();
            xml.Add(new XElement("root", new XElement(statusEle, code)));
            var checker = new LBKenoXmlChecker();
            Assert.Throws<ThirdPartyProductException>(() => checker.CheckXml(xml));
        }

        [Fact]
        public void ChechXml_WhenCallWithRightStatusCode_NotThrowException()
        {
            var xml = new XDocument();
            xml.Add(new XElement("root", new XElement("status_code", "00")));
            var checker = new LBKenoXmlChecker();
            checker.CheckXml(xml);
            Assert.True(true);
        }
    }
}
