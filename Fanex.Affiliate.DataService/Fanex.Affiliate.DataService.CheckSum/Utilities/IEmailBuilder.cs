namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using ViewModel;

    public interface IEmailBuilder
    {
        string GenerateDailyMemberContent(DailyMemberViewModel data, string emailTemplatePath);

        string GenerateDailyMemberIncomeAccessContent(DailyMemberIncomeAccessViewModel data, string emailTemplatePath);
    }
}