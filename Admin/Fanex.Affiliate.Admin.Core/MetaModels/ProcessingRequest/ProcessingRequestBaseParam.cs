using System;

namespace Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest
{
    public class ProcessingRequestBaseParam : UserInfoParam
    {
        public DateTime DeadLine { get; set; }

        public string MarketId { get; set; }

        public string PreferredPayment { get; set; }

        public string Player_Username { get; set; }

        public string PromotionMethod { get; set; }

        public string WebRank { get; set; }

        public string WebSMLanguage { get; set; }

        public string WebRegDate { get; set; }

        public string ServerCountry { get; set; }

        public string WebAdvAff { get; set; }

        public string WebSMQuality { get; set; }

        public string WebSMType { get; set; }

        public string Status { get; set; }

        public string TargetMarket { get; set; }

        public string ContactModeTime { get; set; }

        public string AgentName { get; set; }

        public string Remark { get; set; }

        public string Language { get; set; }

        public string BankName { get; set; }

        public string BankAccountName { get; set; }

        public string BankAddress { get; set; }

        public string BankAccountNo { get; set; }

        public string BankSwiftCode { get; set; }
    }
}
