namespace Fanex.Affiliate.User.Core.DTO.VerificationAccount
{
    public class SendCodeResult : ResultInfo
    {
        public int NewCounter { get; set; }

        public long NewSendCodeTime { get; set; }

        public bool IsCountDown { get; set; } = true;
    }
}
