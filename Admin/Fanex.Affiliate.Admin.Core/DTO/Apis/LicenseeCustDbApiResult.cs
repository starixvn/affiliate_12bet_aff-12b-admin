using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO.Apis
{
    public class LicenseeCustDbApiResult
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public CustInfo Result { get; set; }

        public bool IsSuccessful => ErrorCode == Code.Success;
    }

    public class CustInfo
    {
        public int CustID { get; set; }

        public string Username { get; set; }
    }
}
