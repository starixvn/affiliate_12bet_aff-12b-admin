namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class UpdateStatusParam
    {
        public string Status { get; set; }

        public string PreferredPayment { get; set; }

        public int? GroupId { get; set; }

        public bool MailOption { get; set; }

        public string FirstName { get; set; }

        public string PreferredLanguage { get; set; }

        public string UserName { get; set; }
    }
}
