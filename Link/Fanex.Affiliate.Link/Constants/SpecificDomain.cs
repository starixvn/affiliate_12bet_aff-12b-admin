using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Constants
{
    public static class SpecificDomain
    {
    	private const string AFF16105_ID = "92043193";
        private const string AFF16204_ID = "92043292";
        private const string AFF16205_ID = "92043293";
        
        private const string AFF16000_ID = "92043088";
        private const string AFF16186_ID = "92043274";
        private const string AFF16096_ID = "92043184";
        private const string AFF16097_ID = "92043185";

        private const string AFF15932_ID = "92043020";
        private const string AFF16325_ID = "92043413";
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

        //Fake Mapping best5689
        private const string FakeMapping_Join_Best5689_Com = "aff-12bet.nexdev.net/12BetGo,join.best5689.com";
        private const string FakeMapping_12aff_Best5689_Com = "aff-12bet.nexdev.net/12BetGo,12aff.best5689.com";
        private const string FakeMapping_Go_12real_Com = "aff-12bet.nexdev.net/12BetGo,go.12real.com";

        public static readonly IReadOnlyDictionary<string, string> FakeSpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_ID] = "aff-12bet.nexdev.net/12BetGo,go.12fifa18.com",

            [AFF16204_ID] = FakeMapping_Go_12real_Com,
            [AFF16205_ID] = FakeMapping_Go_12real_Com,
            
            [AFF16000_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16186_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16096_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16097_ID] = FakeMapping_Join_Best5689_Com,

            [AFF16325_ID] = "aff-12bet.nexdev.net/12BetGo,go.5689fun.com",
            [AFF16326_ID] = "aff-12bet.nexdev.net/12BetGo,go.6789sutzo.com",

            //12aff.best5689.com
            [AFF15932_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF130_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16311_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16314_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16225_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF14617_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF13902_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16313_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF15775_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16214_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF13506_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF13065_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF7782_ID] = FakeMapping_12aff_Best5689_Com,
            [AFF16050_ID] = FakeMapping_12aff_Best5689_Com,
            
            [AFF16342_ID] = "aff-12bet.nexdev.net/12BetGo,go.apa56.com",
            [AFF16343_ID] = "aff-12bet.nexdev.net/12BetGo,go.9012ijk.com",
            [AFF16344_ID] = "aff-12bet.nexdev.net/12BetGo,go.sbc31.com",

            [AFF16341_ID] = "aff-12bet.nexdev.net/12BetGo,go.12qiuball.com",
            [AFF16386_ID] = "aff-12bet.nexdev.net/12BetGo,go.all2356.com",
        };
    }
}