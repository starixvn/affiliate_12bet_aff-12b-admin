using Fanex.Affiliate.User.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Utilities
{
    public class HelperTest
    {
        [Fact]
        public void IsAsianAffType_WhenCall_ReturnCorrect()
        {
            var isAsian = Helper.IsAsianAffType("2");

            Assert.True(isAsian);
        }
    }
}
