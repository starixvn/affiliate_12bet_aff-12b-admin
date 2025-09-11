namespace Fanex.Affiliate.User.Core.DTO.Systems
{
    public class TargetCountryLanguage : TargetCountry
    {
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public string NativeName { get; set; }

        public string ShortedCode { get; set; }
    }
}
