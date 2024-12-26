namespace Fanex.Affiliate.User.Core.Criterias
{
    public abstract class PagingCriteria : BaseCriteria
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public int NRows { get; set; }
    }
}
