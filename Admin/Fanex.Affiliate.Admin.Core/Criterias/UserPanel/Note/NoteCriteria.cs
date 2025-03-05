
namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel.Note
{
    public class NoteCriteria : AffPagingCriteria
    {
        private readonly string _spPrefix;

        public NoteCriteria(string spPrefix)
        {
            _spPrefix = spPrefix;
        }

        public override string GetSettingKey() => $"{_spPrefix}_Sel";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(_spPrefix) && AffId > 0;
    }
}
