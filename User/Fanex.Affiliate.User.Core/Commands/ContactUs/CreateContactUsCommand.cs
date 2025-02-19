namespace Fanex.Affiliate.User.Core.Commands.ContactUs
{
    public class CreateContactUsCommand : BaseNonQueryCommand
    {
        public int Mid { get; set; }

        public int Creator { get; set; }

        public string Content { get; set; }

        public string Sender { get; set; }

        public string Affiliate { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string SessionId { get; set; }

        public override string GetSettingKey() => "Aff_EmailHistoryIns";

        public override bool IsValid() => true;
    }
}
