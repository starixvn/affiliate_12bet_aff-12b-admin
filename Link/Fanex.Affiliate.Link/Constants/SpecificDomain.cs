using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Constants
{
    public static class SpecificDomain
    {
        private const string AFF16000_ID = "92043088";
        private const string AFF16186_ID = "92043274";
        private const string AFF16096_ID = "92043184";
        private const string AFF16097_ID = "92043185";
        private const string AFF16105_ID = "92043193";

        public static readonly IReadOnlyDictionary<string, string> FakeSpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_ID] = "aff-12bet.nexdev.net/12BetGo,go.12real.com",
            [AFF16000_ID] = "aff-12bet.nexdev.net/12BetGo,join.best5689.com",
            [AFF16186_ID] = "aff-12bet.nexdev.net/12BetGo,join.best5689.com",
            [AFF16096_ID] = "aff-12bet.nexdev.net/12BetGo,join.best5689.com",
            [AFF16097_ID] = "aff-12bet.nexdev.net/12BetGo,join.best5689.com",
        };
    }
}