using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Attributes
{
    public class ResetValueNotHavePermissionAttributeTest
    {
        const string PERMISSION_NAME = "[Permission]";
        const string permissionSessionKey = "__UserPermissionSessionKey";

        [Fact]
        public void OnResultExecuting_WhenResultIsNotViewOrJsonResult_ThrowException()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.Result = new HttpNotFoundResult();

            Assert.Throws<InvalidOperationException>(() => attribute.OnResultExecuting(filtercontext));
        }

        [Fact]
        public void OnResultExecuting_WhenNotHavePermission_ResetDefaultStringData()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission>());
            var data = new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            Assert.Null(data.ResetProterty);
            Assert.Equal("123", data.NotResetPropety);
        }

        [Fact]
        public void OnResultExecuting_WhenNotHavePermission_ResetDefaultDecimalData()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission>());
            var data = new TestResetDecimalPropertyClass { ResetProterty =  10, NotResetPropety = 10 };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            Assert.Equal(default(decimal), data.ResetProterty);
            Assert.Equal((decimal) 10, data.NotResetPropety);
        }

        [Fact]
        public void OnResultExecuting_WhenNotHavePermission_ResetDefaultDateTimeData()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission>());
            var now = DateTime.Now;
            var data = new TestResetDateTimePropertyClass { ResetProterty = now, NotResetPropety = now };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            Assert.Equal(default(DateTime), data.ResetProterty);
            Assert.Equal(now, data.NotResetPropety);
        }

        [Fact]
        public void OnResultExecuting_WhenHavePermission_NotResetData()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission> { new UserPermission {FunctionName = PERMISSION_NAME } });
            var data = new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            Assert.Equal("abc" ,data.ResetProterty);
            Assert.Equal("123", data.NotResetPropety);
        }

        [Fact]
        public void OnResultExecuting_WhenNotHavePermission_ClearDataInArray()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission>());
            var data = new TestResetStringPropertyClass[]
            {
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" },
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" },
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" }
            };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            foreach (var item in data)
            {
                Assert.Null(item.ResetProterty);
                Assert.Equal("123", item.NotResetPropety);
            }
        }

        [Fact]
        public void OnResultExecuting_WhenNotHavePermission_ResetToDefaultDataInNestedClass()
        {
            var attribute = CreateAttribute();
            var filtercontext = new ResultExecutingContext();
            SetUpContext(filtercontext);
            filtercontext.HttpContext.Session[permissionSessionKey].Returns(new List<UserPermission>());
            var data = new TestResetPropertyInNestedClass();
            data.ResetProperty = "abc";
            data.ResetArrayProperty = new TestResetStringPropertyClass[]
            {
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" },
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" },
                new TestResetStringPropertyClass { ResetProterty = "abc", NotResetPropety = "123" }
            };
            filtercontext.Result = new JsonResult() { Data = data };

            attribute.OnResultExecuting(filtercontext);

            Assert.Null(data.ResetProperty);
            foreach (var item in data.ResetArrayProperty)
            {
                Assert.Null(item.ResetProterty);
                Assert.Equal("123", item.NotResetPropety);
            }
        }

        private void SetUpContext(ResultExecutingContext context)
        {
            var httpContext = Substitute.For<HttpContextBase>();
            context.HttpContext = httpContext;
            var session = Substitute.For<HttpSessionStateBase>();
            httpContext.Session.Returns(session);
        }

        private ResetValueNotHavePermissionAttribute CreateAttribute(string permissionName = PERMISSION_NAME)
            => new ResetValueNotHavePermissionAttribute(permissionName);

        private class TestResetStringPropertyClass
        {
            [PermissionCheck(PERMISSION_NAME)]
            public string ResetProterty { get; set; }

            public string NotResetPropety { get; set; }
        }

        private class TestResetDecimalPropertyClass
        {
            [PermissionCheck(PERMISSION_NAME)]
            public decimal ResetProterty { get; set; }

            public decimal NotResetPropety { get; set; }
        }

        private class TestResetDateTimePropertyClass
        {
            [PermissionCheck(PERMISSION_NAME)]
            public DateTime ResetProterty { get; set; }

            public DateTime NotResetPropety { get; set; }
        }

        private class TestResetPropertyInNestedClass
        {
            [PermissionCheck(PERMISSION_NAME)]
            public string ResetProperty { get; set; }

            public IEnumerable<TestResetStringPropertyClass> ResetArrayProperty { get; set; }
        }

    }
}
