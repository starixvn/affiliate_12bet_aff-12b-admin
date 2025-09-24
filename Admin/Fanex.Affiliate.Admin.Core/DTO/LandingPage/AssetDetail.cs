using System;

namespace Fanex.Affiliate.Admin.Core.DTO.LandingPage
{
    public class AssetDetail
    {
        public int MediaId { get; set; }
        public string MediaName { get; set; }
        public string FileName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Dimension { get; set; }
        public byte StatusId { get; set; }
        public string TargetCountryCode { get; set; }
        public string TargetCountryName { get; set; }
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsMediaIdInUse { get; set; }
    }
}
