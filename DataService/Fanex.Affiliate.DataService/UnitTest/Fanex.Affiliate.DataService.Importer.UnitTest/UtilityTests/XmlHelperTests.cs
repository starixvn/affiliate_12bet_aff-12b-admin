using Fanex.Affiliate.DataService.Importer.Utilities;
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
        [InlineData(@"{""root"":[{""OS"":""1"",""areaName"":""Beijing""},{""OS"":""1"",""areaName"":""Beijing""}]")]
        [InlineData(@"{""root"":[{""OS"":""1"",""areaName"":""Beijing""}]")]
        public void ParseToXDocumentFromJson_CallWithJsonFormatHasRoot_ReturnNotNull(string value)
        {
            var result = XmlHelper.ParseToXDocumentFromJson(value);
            Assert.NotNull(result);
        }

        [Fact]
        public void ParseToXDocumentFromJson_CallWithJsonFormatHasNoRoot_ReturnNull()
        {
            var json = @"[{""OS"":""1"",""areaName"":""Beijing""}]";
            var result = XmlHelper.ParseToXDocumentFromJson(json);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("test 1")]
        public void ParseToXDocumentFromJson_CallWithWrongJsonFormat_ReturnNull(string value)
        {
            var result = XmlHelper.ParseToXDocumentFromJson(value);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""}]")]
        [InlineData(@"[{""OS"":""1"",""areaName"":""Beijing""},{""OS"":""1"",""areaName"":""Beijing""}]")]
        public void ParseToXDocumentFromJson_CallWithJsonFormatNoRoot_ReturnNotNull(string value)
        {
            var json = @"[{""OS"":""1"",""areaName"":""Beijing""}]";
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

        [Fact]
        public void ParseToXDocument_WhenCallLBKenoXmlString_ReturnNotNull()
        {
            //var filePath = Path.Combine(Directory.GetCurrentDirectory)
        }
    }
}
