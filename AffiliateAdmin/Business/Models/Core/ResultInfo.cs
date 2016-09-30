namespace AffiliateAdmin.Business.Models.Core
{
    public class ResultInfo
    {
        public const int UnknownErrorCode = 9999;

        public ResultInfo()
        {
        }

        public ResultInfo(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful
        {
            get { return this.Code == 0; }
        }
    }
}