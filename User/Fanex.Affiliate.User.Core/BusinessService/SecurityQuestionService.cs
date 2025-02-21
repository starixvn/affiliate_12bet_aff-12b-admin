namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;

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
