namespace Fanex.Affiliate.User.Core.DTO.Apis
{
    public class LicenseeCustDbApiResult
    {
        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public CustInfo Result { get; set; }

        public bool IsSuccessful => ErrorCode == 0;
    }

    public class CustInfo
    {
        public int CustID { get; set; }

        public string Username { get; set; }
    }
}
