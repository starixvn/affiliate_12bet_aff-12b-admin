namespace Fanex.Affiliate.User.Core.DTO
{
    public class SendCodeResult : ResultInfo
    {
        public int NewCounter { get; set; }

        public long NewSendCodeTime { get; set; }
    }
}
