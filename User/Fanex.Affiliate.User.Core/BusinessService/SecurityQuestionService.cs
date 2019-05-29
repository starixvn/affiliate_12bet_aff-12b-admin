using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Repository;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private readonly ISecurityQuestionRepository _securityQuestionRepository;

        public SecurityQuestionService(ISecurityQuestionRepository securityQuestionRepository)
        {
            _securityQuestionRepository = securityQuestionRepository;
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
        {
            return _securityQuestionRepository.GetSecurityQuestions(language, isIncludeOldData);
        }
    }
}
