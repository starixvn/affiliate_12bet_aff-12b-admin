namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using DTO.UserPanel;

    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetBaseCurrencyList();
    }
}
