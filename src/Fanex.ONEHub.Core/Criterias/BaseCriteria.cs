using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias
{
    public class BaseCriteria : CriteriaBase
    {
        private readonly string _spName;

        public BaseCriteria(string spName)
        {
            _spName = spName;
        }

        public override string GetSettingKey() => _spName;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(_spName);
    }
}
