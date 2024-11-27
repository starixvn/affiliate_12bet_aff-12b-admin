using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SpecificDomain
    {
        private const string DOMAIN_GO_LOCAL = "aff-12bet.nexdev.net/12BetGo";
        private const string DOMAIN_GO1_LOCAL = "aff-12bet.nexdev.net/12BetGo1";
        private const string DOMAIN_GO_12REAL_PRO = "go.12real.com";
        private const string DOMAIN_GO_JOIN_BEST5689_PRO = "join.best5689.com";
        private const string DOMAIN_GO_5689FUN_PRO = "go.5689fun.com";
        private const string DOMAIN_GO_6789SUTZO_PRO = "go.6789sutzo.com";
        private const string DOMAIN_GO_12FIFA18_PRO = "go.12fifa18.com";

        //go.12real.com, go.12fifa18.com
        private const string AFF16105_ID = "92043193";

        //go.12real.com
        private const string AFF16204_ID = "92043292";
        private const string AFF16205_ID = "92043293";

        //join.best5689.com
        private const string AFF16000_ID = "92043088";
        private const string AFF16186_ID = "92043274";
        private const string AFF16096_ID = "92043184";
        private const string AFF16097_ID = "92043185";

        //go.5689fun.com
        private const string AFF15932_ID = "92043020";
        private const string AFF16325_ID = "92043413";

        //go.6789sutzo.com
        private const string AFF16326_ID = "92043414";

        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnPro = new Dictionary<string, string>
        {
            [AFF16105_ID] = $"{DOMAIN_GO_12REAL_PRO},{DOMAIN_GO_12FIFA18_PRO}",
            
            [AFF16204_ID] = DOMAIN_GO_12REAL_PRO,
            [AFF16205_ID] = DOMAIN_GO_12REAL_PRO,
            
            [AFF16000_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16186_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16096_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16097_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,

            [AFF15932_ID] = DOMAIN_GO_5689FUN_PRO,
            [AFF16325_ID] = DOMAIN_GO_5689FUN_PRO,

            [AFF16326_ID] = DOMAIN_GO_6789SUTZO_PRO,
        };

        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_ID] = $"{DOMAIN_GO_LOCAL},{DOMAIN_GO1_LOCAL}",
            
            [AFF16204_ID] = DOMAIN_GO_LOCAL,
            [AFF16205_ID] = DOMAIN_GO_LOCAL,
            
            [AFF16000_ID] = DOMAIN_GO_LOCAL,
            [AFF16186_ID] = DOMAIN_GO_LOCAL,
            [AFF16096_ID] = DOMAIN_GO_LOCAL,
            [AFF16097_ID] = DOMAIN_GO_LOCAL,

            [AFF15932_ID] = DOMAIN_GO_LOCAL,
            [AFF16325_ID] = DOMAIN_GO_LOCAL,

            [AFF16326_ID] = DOMAIN_GO_LOCAL,
        };
    }
}
