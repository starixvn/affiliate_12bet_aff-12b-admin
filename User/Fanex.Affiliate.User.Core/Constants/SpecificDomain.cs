using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SpecificDomain
    {
        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnPro = new Dictionary<string, string>
        {
            ["92043088"] = "join.best5689.com", //12BETAFF16000
            ["92043193"] = "go.12real.com", //12BETAFF16105
            ["92043274"] = "go.12real.com", //12BETAFF16186
        };

        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            ["927841"] = "aff-12bet.nexdev.net/12BetGo", //12BETAFF001
            ["968222"] = "aff-12bet.nexdev.net/12BetGo", //12BETAFF002
            ["989900"] = "aff-12bet.nexdev.net/12BetGo", //12BETAFF003
        };
    }
}
