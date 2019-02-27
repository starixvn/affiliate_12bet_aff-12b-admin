namespace Fanex.Affiliate.Admin.Core.DTO
{
    public class ResultInfo
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful => Code == 0;
    }
}
