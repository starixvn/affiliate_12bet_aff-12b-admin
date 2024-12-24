namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;

    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IDynamicRepository _repository;

        public CurrencyRepository(IDynamicRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Currency> GetBaseCurrencyList()
        {
            return _repository.Fetch<Currency>(new GetBaseCurrencyListCriteria());
        }
    }
}
