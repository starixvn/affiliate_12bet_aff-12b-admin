using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.MetaModels.LandingPage
{
    public class FormManagementParam : BaseLandingPageParam
    {
        public string ModuleIds { get; } = LandingPageConstants.ModuleIds.RegistrationForm.ToString();
    }
}
