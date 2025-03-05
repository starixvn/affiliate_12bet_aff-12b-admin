namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;

    public abstract class BaseCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }
    }
}
