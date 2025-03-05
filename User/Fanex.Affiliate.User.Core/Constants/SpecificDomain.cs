using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Constants
{
    public static class SpecificDomain
    {
        private const string DOMAIN_GO_LOCAL = "aff-12b.nexdev.net/Go";
        private const string DOMAIN_GOV2_LOCAL = "aff-12b.nexdev.net/Go1";
        private const string DOMAIN_GO_12REAL_PRO = "go.12real.com";
        private const string DOMAIN_GO_JOIN_BEST5689_PRO = "join.best5689.com";
        private const string DOMAIN_GO_5689FUN_PRO = "go.5689fun.com";
        private const string DOMAIN_GO_6789SUTZO_PRO = "go.6789sutzo.com";
        private const string DOMAIN_GO_12FIFA18_PRO = "go.12fifa18.com";

        private const string DOMAIN_GO_12AFF_BEST5689_PRO = "12aff.best5689.com";

        private const string DOMAIN_GO_9012IJK_PRO = "go.9012ijk.com";
        private const string DOMAIN_GO_SBC31_PRO = "go.sbc31.com";

        private const string DOMAIN_GO_THA88_PRO = "go.tha88.com";
        private const string DOMAIN_GO_APA56_PRO = "go.apa56.com";

        private const string DOMAIN_GO_12QIUBALL_PRO = "go.12qiuball.com";
        private const string DOMAIN_GO_ALL2356_PRO = "go.all2356.com";
        private const string DOMAIN_GO_AMO38_PRO = "go.amo38.com";
        private const string DOMAIN_GO_12WINBALL_PRO = "go.12winball.com";

        private const string DOMAIN_GO_12BLINRLINK1_PRO = "goto.12binrlink1.com";

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

        //12aff.best5689.com
        private const string AFF130_ID = "1257975";
        private const string AFF16311_ID = "92043399";
        private const string AFF16314_ID = "92043402";
        private const string AFF16225_ID = "92043313";
        private const string AFF14617_ID = "92041705";
        private const string AFF13902_ID = "92040990";
        private const string AFF16313_ID = "92043401";
        private const string AFF15775_ID = "92042863";
        private const string AFF16214_ID = "92043302";
        private const string AFF13506_ID = "92040594";
        private const string AFF13065_ID = "92040153";
        private const string AFF7782_ID = "92034870";
        private const string AFF16050_ID = "92043138";

        private const string AFF16342_ID = "92043430";
        private const string AFF16343_ID = "92043431";
        private const string AFF16344_ID = "92043432";

        private const string AFF16341_ID = "92043429";
        private const string AFF16386_ID = "92043474";

        private const string AFF16007_ID = "92043095";

        private const string AFF16338_ID = "92043426";
        private const string AFF16336_ID = "92043424";
        private const string AFF16337_ID = "92043425";

        private const string AFF16416_ID = "92043504";

        public static readonly string[] NotSSLDomains = { DOMAIN_GO_6789SUTZO_PRO, DOMAIN_GO_APA56_PRO };

        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnPro = new Dictionary<string, string>
        {
            [AFF16105_ID] = $"{DOMAIN_GO_12REAL_PRO},{DOMAIN_GO_12FIFA18_PRO}",

            [AFF16204_ID] = DOMAIN_GO_12REAL_PRO,
            [AFF16205_ID] = DOMAIN_GO_12REAL_PRO,

            [AFF16000_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16186_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16096_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,
            [AFF16097_ID] = DOMAIN_GO_JOIN_BEST5689_PRO,

            [AFF15932_ID] = $"{DOMAIN_GO_5689FUN_PRO},{DOMAIN_GO_12AFF_BEST5689_PRO}",
            [AFF16325_ID] = DOMAIN_GO_5689FUN_PRO,

            [AFF16326_ID] = DOMAIN_GO_6789SUTZO_PRO,

            //12aff.best5689.com
            [AFF130_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16311_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16314_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16225_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF14617_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF13902_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16313_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF15775_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16214_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF13506_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF13065_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF7782_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,
            [AFF16050_ID] = DOMAIN_GO_12AFF_BEST5689_PRO,

            [AFF16342_ID] = $"{DOMAIN_GO_THA88_PRO},{DOMAIN_GO_APA56_PRO}",
            [AFF16343_ID] = DOMAIN_GO_9012IJK_PRO,
            [AFF16344_ID] = DOMAIN_GO_SBC31_PRO,

            [AFF16341_ID] = DOMAIN_GO_12QIUBALL_PRO,
            [AFF16386_ID] = $"{DOMAIN_GO_ALL2356_PRO},{DOMAIN_GO_AMO38_PRO}",

            [AFF16007_ID] = DOMAIN_GO_12WINBALL_PRO,

            [AFF16338_ID] = "goto.12bsprt1.com",
            [AFF16336_ID] = "goto.12bsprt2.com",
            [AFF16337_ID] = "goto.12bsprt3.com",

            [AFF16416_ID] = $"{DOMAIN_GO_12BLINRLINK1_PRO}",
        };

        public static readonly IReadOnlyDictionary<string, string> SpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_ID] = $"{DOMAIN_GO_LOCAL},{DOMAIN_GOV2_LOCAL}",

            [AFF16204_ID] = DOMAIN_GO_LOCAL,
            [AFF16205_ID] = DOMAIN_GO_LOCAL,

            [AFF16000_ID] = DOMAIN_GO_LOCAL,
            [AFF16186_ID] = DOMAIN_GO_LOCAL,
            [AFF16096_ID] = DOMAIN_GO_LOCAL,
            [AFF16097_ID] = DOMAIN_GO_LOCAL,

            [AFF15932_ID] = DOMAIN_GO_LOCAL,
            [AFF16325_ID] = DOMAIN_GO_LOCAL,

            [AFF16326_ID] = DOMAIN_GO_LOCAL,

            //12aff.best5689.com
            [AFF130_ID] = DOMAIN_GO_LOCAL,
            [AFF16311_ID] = DOMAIN_GO_LOCAL,
            [AFF16314_ID] = DOMAIN_GO_LOCAL,
            [AFF16225_ID] = DOMAIN_GO_LOCAL,
            [AFF14617_ID] = DOMAIN_GO_LOCAL,
            [AFF13902_ID] = DOMAIN_GO_LOCAL,
            [AFF16313_ID] = DOMAIN_GO_LOCAL,
            [AFF15775_ID] = DOMAIN_GO_LOCAL,
            [AFF16214_ID] = DOMAIN_GO_LOCAL,
            [AFF13506_ID] = DOMAIN_GO_LOCAL,
            [AFF13065_ID] = DOMAIN_GO_LOCAL,
            [AFF7782_ID] = DOMAIN_GO_LOCAL,
            [AFF16050_ID] = DOMAIN_GO_LOCAL,

            [AFF16342_ID] = DOMAIN_GO_LOCAL,
            [AFF16343_ID] = DOMAIN_GO_LOCAL,
            [AFF16344_ID] = DOMAIN_GO_LOCAL,

            [AFF16341_ID] = DOMAIN_GO_LOCAL,
            [AFF16386_ID] = $"{DOMAIN_GO_LOCAL},{DOMAIN_GOV2_LOCAL}",

            [AFF16007_ID] = DOMAIN_GO_LOCAL,

            [AFF16338_ID] = DOMAIN_GO_LOCAL,
            [AFF16336_ID] = DOMAIN_GO_LOCAL,
            [AFF16337_ID] = DOMAIN_GO_LOCAL,
            [AFF16416_ID] = $"{DOMAIN_GO_LOCAL}",
        };
    }
}
