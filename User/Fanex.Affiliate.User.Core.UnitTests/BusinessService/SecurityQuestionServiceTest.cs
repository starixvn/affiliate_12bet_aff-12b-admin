using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Repository;
using NSubstitute;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class SecurityQuestionServiceTest
    {
        private readonly ISecurityQuestionRepository SecurityQuestionRepo;

        public SecurityQuestionServiceTest()
        {
            SecurityQuestionRepo = Substitute.For<ISecurityQuestionRepository>();
        }

        [Fact]
        public void GetSecurityQuestions_WhenCall_RepoCall()
        {
            var service = CreateService();
            SecurityQuestionRepo.GetSecurityQuestions(null, false).ReturnsForAnyArgs(CreateSecurityQuestion());

            var result = service.GetSecurityQuestions("vn", false);

            SecurityQuestionRepo.Received().GetSecurityQuestions("vn", false);
            Assert.NotEmpty(result);
        }

        private IEnumerable<SecurityQuestion> CreateSecurityQuestion()
            => new List<SecurityQuestion>
            {
                new SecurityQuestion {Question = "abc", QuestionId = 1 }
            };

        private SecurityQuestionService CreateService()
            => new SecurityQuestionService(SecurityQuestionRepo);
    }
}
