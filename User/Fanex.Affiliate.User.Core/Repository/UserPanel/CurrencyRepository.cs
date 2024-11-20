namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using DTO.UserPanel;

    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly IObjectDBBuilder _builder;

        public CurrencyRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<Currency> GetBaseCurrencyList()
        {
            const string uSpKey = "Aff_BaseCurrency_Sel";

            using (var objectDb = _builder.Build(uSpKey))
            {
                return objectDb.Query<Currency>();
            }
        }
    }
}
