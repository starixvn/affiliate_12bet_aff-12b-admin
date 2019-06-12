using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.Admin.Core.MetaModels.EmailHistory
{
    public class DeleteEmailHistoryParam
    {
        [Required(ErrorMessage = "Please select email to delete")]
        public string EmailIdList { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }
    }
}
