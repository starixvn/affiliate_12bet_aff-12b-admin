using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest
{
    public class ProcessingRequestDetailInfo : ProcessingRequestBasicInfo
    {
        public string MarketId { get; set; }

        public string MarketName { get; set; }

        public string PreferredPayment { get; set; }

        public string Player_UserName { get; set; }

        public string PromotionMethod { get; set; }

        public string WebRank { get; set; }

        public string WebRegDate { get; set; }

        public string WebSMLanguage { get; set; }

        public string ServerCountry { get; set; }

        public string WebSMQuality { get; set; }

        public string WebSMType { get; set; }

        public string WebAdvAff { get; set; }

        public string TargetMarket { get; set; }

        public string AgentName { get; set; }

        public string ContactModeTime { get; set; }

        public string Remark { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string SwiftCode { get; set; }

        public string AccountName { get; set; }

        public string AccountNo { get; set; }
    }
}
