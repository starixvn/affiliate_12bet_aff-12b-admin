using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Constants
{
    public static class SpecificDomain
    {
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

        //goto.2356all.com
        private const string AFF16494_ID = "92043582";

        //goto.elv520.com
        private const string AFF16492_ID = "92043580";

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
        private const string AFF16450_ID = "92043538";

        private const string AFF16472_ID = "92043560";
        private const string AFF16473_ID = "92043561";
        private const string AFF16479_ID = "92043567";
        private const string AFF16478_ID = "92043566";
        private const string AFF16475_ID = "92043563";
        private const string AFF16476_ID = "92043564";
        private const string AFF16477_ID = "92043565";
        private const string AFF16454_ID = "92043542";
        private const string AFF16455_ID = "92043543";
        private const string AFF16456_ID = "92043544";
        private const string AFF16457_ID = "92043545";
        private const string AFF16480_ID = "92043568";
        private const string AFF16481_ID = "92043569";
        private const string AFF16482_ID = "92043570";
        private const string AFF16483_ID = "92043571";

        private const string AFF16496_ID = "92043584";

        private const string AFF16501_ID = "92043589";
        private const string AFF16502_ID = "92043590";
        private const string AFF16503_ID = "92043591";

        //Fake Mapping
        private const string FakeMapping_Join_Best5689_Com = "aff-12b.nexdev.net/Go,join.best5689.com";
        private const string FakeMapping_12aff_Best5689_Com = "aff-12b.nexdev.net/Go,12aff.best5689.com";
        private const string FakeMapping_Go_12real_Com = "aff-12b.nexdev.net/Go,go.12real.com";
        private const string FakeMapping_Goto_12binlink1_Com = "aff-12b.nexdev.net/Go,goto.12binlink1.com";
        private const string FakeMapping_Goto_12bmylink1_Com = "aff-12b.nexdev.net/Go,goto.12bmylink1.com";
        private const string FakeMapping_Goto_12bvnlink1_Com = "aff-12b.nexdev.net/Go,goto.12bvnlink1.com";
        private const string FakeMapping_Goto_12bthlink1_Com = "aff-12b.nexdev.net/Go,goto.12bthlink1.com";

        public static readonly IReadOnlyDictionary<string, string> FakeSpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_ID] = "aff-12b.nexdev.net/Go,go.12fifa18.com",

            [AFF16204_ID] = FakeMapping_Go_12real_Com,
            [AFF16205_ID] = FakeMapping_Go_12real_Com,

            [AFF16000_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16186_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16096_ID] = FakeMapping_Join_Best5689_Com,
            [AFF16097_ID] = FakeMapping_Join_Best5689_Com,

            [AFF16325_ID] = "aff-12b.nexdev.net/Go,go.5689fun.com",
            [AFF16326_ID] = "aff-12b.nexdev.net/Go,go.6789sutzo.com",

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

            [AFF16341_ID] = "aff-12b.nexdev.net/Go,go.12qiuball.com",

            [AFF16342_ID] = "aff-12b.nexdev.net/Go,goto.12bmylink1.com",
            [AFF16343_ID] = "aff-12b.nexdev.net/Go,go.9012ijk.com",
            [AFF16344_ID] = "aff-12b.nexdev.net/Go,go.sbc31.com",

            [AFF16386_ID] = "aff-12b.nexdev.net/Go,go.amo38.com",
            [AFF16007_ID] = "aff-12b.nexdev.net/Go,go.12winball.com",

            [AFF16338_ID] = "aff-12b.nexdev.net/Go,goto.12bsprt1.com",
            [AFF16336_ID] = "aff-12b.nexdev.net/Go,goto.12bsprt2.com",
            [AFF16337_ID] = "aff-12b.nexdev.net/Go,goto.12bsprt3.com",

            [AFF16416_ID] = "aff-12b.nexdev.net/Go,goto.12binrlink1.com",
            [AFF16450_ID] = "aff-12b.nexdev.net/Go,goto.12binrlink2.com",

            [AFF16472_ID] = FakeMapping_Goto_12binlink1_Com,
            [AFF16473_ID] = FakeMapping_Goto_12bvnlink1_Com,
            [AFF16479_ID] = FakeMapping_Goto_12bvnlink1_Com,
            [AFF16478_ID] = FakeMapping_Goto_12bthlink1_Com,
            [AFF16475_ID] = FakeMapping_Goto_12bmylink1_Com,
            [AFF16476_ID] = FakeMapping_Goto_12binlink1_Com,
            [AFF16477_ID] = FakeMapping_Goto_12bthlink1_Com,
            [AFF16454_ID] = FakeMapping_Goto_12binlink1_Com,
            [AFF16455_ID] = FakeMapping_Goto_12bthlink1_Com,
            [AFF16456_ID] = FakeMapping_Goto_12bvnlink1_Com,
            [AFF16457_ID] = FakeMapping_Goto_12bmylink1_Com,
            [AFF16480_ID] = FakeMapping_Goto_12bvnlink1_Com,
            [AFF16481_ID] = FakeMapping_Goto_12bmylink1_Com,
            [AFF16482_ID] = FakeMapping_Goto_12binlink1_Com,
            [AFF16483_ID] = FakeMapping_Goto_12bthlink1_Com,

            [AFF16494_ID] = "aff-12b.nexdev.net/Go,goto.2356all.com",

            [AFF16492_ID] = "aff-12b.nexdev.net/Go,goto.elv520.com",
            
            [AFF16496_ID] = FakeMapping_Goto_12bthlink1_Com,
            [AFF16501_ID] = FakeMapping_Goto_12bvnlink1_Com,
            [AFF16502_ID] = FakeMapping_Goto_12bmylink1_Com,
            [AFF16503_ID] = FakeMapping_Goto_12binlink1_Com
        };
    }
}