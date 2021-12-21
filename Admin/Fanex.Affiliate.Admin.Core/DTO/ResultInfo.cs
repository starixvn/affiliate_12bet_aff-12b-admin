
namespace Fanex.Affiliate.Admin.Core.DTO
{
    public class ResultInfo
    {
        public static ResultInfo Success => new ResultInfo { Code = 0 };

        public static ResultInfo GenerateErr(string message = "") => new ResultInfo
        {
            Code = Constants.Code.GeneralErrorCode,
            Message = message,
        };

        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful => Code == 0;
    }
}
