namespace Fanex.Affiliate.Link.Models;

public class ValidateResult
{
    public bool IsValid { get; set; }

    public string ErrorMessage { get; set; }

    public string RedirectView { get; set; }

    public bool IsAffMarketing { get; set; }
}
