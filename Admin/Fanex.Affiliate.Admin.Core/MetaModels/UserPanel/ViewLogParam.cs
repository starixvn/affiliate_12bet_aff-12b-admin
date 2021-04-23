using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.UserPanel
{
    public class ViewLogParam : AffiliatePagingByDateParam
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LogActionIds { get; set; }
    }
}
