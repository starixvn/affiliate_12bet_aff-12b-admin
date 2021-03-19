namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ViewLogParam : AffiliatePagingByDateParam
    {
        public int LogAction { get; set; }

        public bool IsNewViewLog { get; set; }
    }
}
