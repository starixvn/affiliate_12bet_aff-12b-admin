namespace Fanex.Affiliate.User.Core.DTO.Apis
{
    public class AccountApiResult
    {
        public bool Data { get; set; }

        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public string RawErrorCode { get; set; }

        public string RawMessage { get; set; }
    }
}
