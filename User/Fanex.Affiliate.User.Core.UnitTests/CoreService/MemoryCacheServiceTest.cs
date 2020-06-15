using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Caching;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.CoreService
{
    public class MemoryCacheServiceTest
    {
        private readonly ICache Cache;

        public MemoryCacheServiceTest()
        {
            Cache = Substitute.For<ICache>();
        }

        [Fact]
        public void Contains_WhenCall_CallCache()
        {
            var service = CreateService();

            service.Contains("abc");

            Cache.Received().Contains("abc");
        }

        [Fact]
        public void Get_WhenCall_ReturnDeepClone()
        {
            var service = CreateService();
            var model = new TestModel
            {
                PropNumber = 1,
                PropString = "abc"
            };
            Cache.Get<TestModel>(null).ReturnsForAnyArgs(model);

            var result = service.Get<TestModel>("aabc");

            Assert.NotNull(result);
            Assert.NotEqual(model, result);
        }

        [Fact]
        public void Set_WhenCall_CacheCallWithDeepCloneValue()
        {
            var service = CreateService();
            TestModel cacheValue = null;
            var model = new TestModel
            {
                PropNumber = 1,
                PropString = "abc"
            };
            Cache.When(c => c.Set<TestModel>(Arg.Compat.Any<string>(), Arg.Compat.Any<TestModel>(), Arg.Compat.Any<int>()))
                .Do(c =>
                {
                    cacheValue = c.Args()[1] as TestModel;
                });

            service.Set("abc", model, 120);

            Assert.NotNull(cacheValue);
            Assert.NotEqual(cacheValue, model);
        }

        [Fact]
        public void Clear_WhenCall_CacheCallClear()
        {
            var service = CreateService();

            service.Clear();

            Cache.Received().Clear();
        }

        [Fact]
        public void Delete_WhenCall_CacheCallRemove()
        {
            var service = CreateService();

            service.Delete("abc");

            Cache.Received().Remove("abc");
        }

        private MemoryCacheService CreateService()
            => new MemoryCacheService(Cache);

        private class TestModel
        {
            public int PropNumber { get; set; }

            public string PropString { get; set; }
        }
    }
}
