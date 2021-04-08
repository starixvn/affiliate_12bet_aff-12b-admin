namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ViewLogParam : AffiliatePagingByDateParam
    {
        public string LogActionIds { get; set; }

        public bool IsNewViewLog { get; set; }
    }
}
