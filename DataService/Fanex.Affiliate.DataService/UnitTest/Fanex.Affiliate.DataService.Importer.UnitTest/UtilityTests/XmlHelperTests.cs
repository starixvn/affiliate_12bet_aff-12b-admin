using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.DataService.Importer.UnitTest.UtilityTests
{
    public class XmlHelperTests
    {
        [Theory]
        [InlineData(@"{""root"":[{""OS"":""1"",""areaName"":""Beijing""},{""OS"":""2"",""areaName"":""Beijing1""}]")]
        [InlineData(@"{""root"":[{""OS"":""1"",""areaName"":""Beijing""}]")]
        public void ParseToXDocumentFromJson_CallWithJsonFormatHasRoot_ReturnNotNull(string value)
        {
            var result = XmlHelper.ParseToXDocumentFromJson(value);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("test 1")]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""},{""OS"":""1"",""areaName"":""Beijing""}]")]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""}]")]
        public void ParseToXDocumentFromJson_CallWithWrongJsonFormat_ReturnNull(string value)
        {
            var result = XmlHelper.ParseToXDocumentFromJson(value);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(@"{""status"":""FAIL"",""message"":null,""errors"":[{""field"":""brandPassword"",""error"":""INVALID_VALUE""}]}")]
        [InlineData(@"{""status"":""FAIL"",""message"":""CALL_LIMIT_EXCEEDED"",""errors"":[]}")]
        public void ParseToXDocumentFromJson_CallWithJsonFormatNoRoot_ReturnNotNull(string value)
        {
            var result = XmlHelper.ParseToXDocumentFromJson(value, "transaction");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""}]")]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""},{""OS"":""1"",""areaName"":""Beijing""}]")]
        public void JsonConvertToDictionaryArray_WhenCall_ReturnNotNull(string value)
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(value);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(@"<?xml version=""1.0""?><game_transaction_detail></game_transaction_detail>")]
        public void ParseToXDocument_WhenCall_NotThrowException(string xml)
        {
            XmlHelper.ParseToXDocument(xml);
            Assert.True(true);
        }
        [Fact]
        public void ParseToXDocument_WhenCall_ReturnWithRightRootName()
        {
            var rootName = "game_transaction_detai";
            var xmlString = $"<?xml version=\"1.0\"?><{rootName}></{rootName}>";
            var xml = XmlHelper.ParseToXDocument(xmlString);
            var root = xml.Root.Name.LocalName;
            Assert.Contains(rootName, root);
        }
    }
}
