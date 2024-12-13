using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.DTO
{
    public class ResultInfo
    {
        public ResultInfo() { }

        public ResultInfo(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful => Code == 0;

        public static ResultInfo Success => new ResultInfo();

        public static ResultInfo GenerateErr(string message = "") => new ResultInfo
        {
            Code = ErrCode.GeneralErrorCode,
            Message = message
        };
    }
}
