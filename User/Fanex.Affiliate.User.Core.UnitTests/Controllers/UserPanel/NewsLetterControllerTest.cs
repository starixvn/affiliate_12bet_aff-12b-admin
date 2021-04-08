using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class NewsLetterControllerTest
    {
        private readonly INewsLetterService newsLetterService;

        public NewsLetterControllerTest()
        {
            newsLetterService = Substitute.For<INewsLetterService>();
        }

        [Fact]
        public async Task Index_WhenCall_ReturnCorrectView()
        {
            newsLetterService.GetCurrentContent().Returns(Task.FromResult("USD"));

            var controller = new NewsLetterController(newsLetterService, null, null);
            var result = await controller.Index() as ViewResult;
            Assert.Equal("USD", result.ViewBag.Content);
            Assert.Equal("~/Views/UserPanel/NewsLetter/Index.cshtml", result.ViewName);
        }

        [Fact]
        public async Task Preview_WhenCall_ReturnCorrectView()
        {
            const int newsLetterId = 123;
            newsLetterService.GetContentById(newsLetterId).Returns(Task.FromResult("abc.html"));

            var controller = new NewsLetterController(newsLetterService, null, null);
            var result = await controller.Preview(newsLetterId) as ViewResult;
            Assert.Equal("abc.html", result.ViewBag.Content);
            Assert.Equal("~/Views/UserPanel/NewsLetter/Preview.cshtml", result.ViewName);
        }
    }
}
