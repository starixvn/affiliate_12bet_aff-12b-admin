namespace Fanex.Affiliate.User.Core.BusinessService
{
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using System.Collections.Generic;

    public class SecurityQuestionService : BaseDynamicService, ISecurityQuestionService
    {
        public SecurityQuestionService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        { }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language)
        {
            return FetchTData<SecurityQuestion>(new GetSecurityQuestionsCriteria(language));
        }
    }
}
