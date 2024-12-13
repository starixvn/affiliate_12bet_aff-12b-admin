namespace Fanex.Affiliate.User.Core.Repository
{
    using System.Collections.Generic;
    using DTO;

    public interface ISystemRepository
    {
        IEnumerable<Language> GetLanguageList(bool isInternal);
    }
}
