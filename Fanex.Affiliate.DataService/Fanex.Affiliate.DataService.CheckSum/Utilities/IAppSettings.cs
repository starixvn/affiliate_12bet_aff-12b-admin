namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    public interface IAppSettings
    {
        int CheckingTime { get; }

        bool IsLocalTest { get; }

        int DayToAdd { get; }

        string ToMail { get; }

        string CcMail { get; }

        string BccMail { get; }

        string EmailTemplateFolder { get; }

        string GetValue(string key);
    }
}