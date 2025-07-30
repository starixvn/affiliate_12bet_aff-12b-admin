using Fanex.Affiliate.Link.Commands;

namespace Fanex.Affiliate.Link.Criterias;

public class GetRedirectPageBySettingCriteria : BaseNonQueryCommand
{
    public string AffCode { get; set; }

    public string AffDomain { get; set; }

    public string PageCode { get; set; }

    public string LanguageCode { get; set; }

    public string RedirectPage { get; set; }

    public override string GetSettingKey() => "Aff_ClickStatistics_RedirectPage_Sel";

    public override bool IsValid() => true;
}
