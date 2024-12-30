namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System.Collections.Generic;
    using Criterias;
    using Data.Repository;
    using DTO;
    using Fanex.Affiliate.User.Core.BusinessService;
    using NSubstitute;
    using Xunit;

    public class SecurityQuestionServiceTest
    {
        private readonly IDynamicRepository _repository;

        public SecurityQuestionServiceTest()
        {
            _repository = Substitute.For<IDynamicRepository>();
        }

        [Fact]
        public void GetSecurityQuestions_WhenCall_RepoCall()
        {
            var service = CreateService();
            _repository.Fetch<SecurityQuestion>(Arg.Any<GetSecurityQuestionsCriteria>())
                .Returns(CreateSecurityQuestion());

            var result = service.GetSecurityQuestions("vn");

            _repository.Received().Fetch<SecurityQuestion>(Arg.Any<GetSecurityQuestionsCriteria>());
            Assert.NotEmpty(result);
        }

        private static IEnumerable<SecurityQuestion> CreateSecurityQuestion()
            => new List<SecurityQuestion>
            {
                new SecurityQuestion {Question = "abc", QuestionId = 1 }
            };

        private SecurityQuestionService CreateService()
            => new SecurityQuestionService(_repository);
    }
}
