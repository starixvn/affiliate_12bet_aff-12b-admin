namespace Fanex.Affiliate.Link.Services.Interface
{
    public interface IAffiliateService
    {
        string GetCommGroupByAffId(int affId);

        string GetTargetCountryCodeByLanguage(string shortedCode);
    }
}