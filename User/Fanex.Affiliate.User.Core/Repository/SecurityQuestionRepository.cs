namespace Fanex.Affiliate.User.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Criterias;
    using Data.Repository;
    using DTO;

    public class SecurityQuestionRepository : ISecurityQuestionRepository
    {
        private readonly IDynamicRepository _repository;

        public SecurityQuestionRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language)
        {
            var result = _repository.Fetch<SecurityQuestion>(new GetSecurityQuestionsCriteria(language));

            return result ?? Enumerable.Empty<SecurityQuestion>();
        }
    }
}
