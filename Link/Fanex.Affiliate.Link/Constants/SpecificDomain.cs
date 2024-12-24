using System.Collections.Generic;

namespace Fanex.Affiliate.Link.Constants
{
    public static class SpecificDomain
    {
        private const string AFF16105_Code = "17709";
        private const string AFF16204_Code = "14867";
        private const string AFF16205_Code = "16122";

        private const string AFF16000_Code = "15015";
        private const string AFF16186_Code = "17973";
        private const string AFF16096_Code = "17336";
        private const string AFF16097_Code = "10792";

        private const string AFF15932_Code = "16480";
        private const string AFF16325_Code = "10809";
        private const string AFF16326_Code = "10303";

        //12aff.best5689.com
        private const string AFF130_Code = "11897";
        private const string AFF16311_Code = "11434";
        private const string AFF16314_Code = "15070";
        private const string AFF16225_Code = "10455";
        private const string AFF14617_Code = "14172";
        private const string AFF13902_Code = "15181";
        private const string AFF16313_Code = "13363";
        private const string AFF15775_Code = "13768";
        private const string AFF16214_Code = "11505";
        private const string AFF13506_Code = "12117";
        private const string AFF7782_Code = "17705";
        private const string AFF16050_Code = "15879";

        private const string AFF16342_Code = "92043430";
        private const string AFF16343_Code = "92043431";
        private const string AFF16344_Code = "92043432";
        private const string AFF13065_Code = "92040153";

        //Fake Mapping
        private const string FakeMapping_Join_Best5689_Com = "aff-12b.nexdev.net/Go,join.best5689.com";
        private const string FakeMapping_12aff_Best5689_Com = "aff-12b.nexdev.net/Go,12aff.best5689.com";
        private const string FakeMapping_Go_12real_Com = "aff-12b.nexdev.net/Go,go.12real.com";

        public static readonly IReadOnlyDictionary<string, string> FakeSpecificDomainByAffOnLocal = new Dictionary<string, string>
        {
            [AFF16105_Code] = "aff-12b.nexdev.net/Go,go.12fifa18.com",

            [AFF16204_Code] = FakeMapping_Go_12real_Com,
            [AFF16205_Code] = FakeMapping_Go_12real_Com,

            [AFF16000_Code] = FakeMapping_Join_Best5689_Com,
            [AFF16186_Code] = FakeMapping_Join_Best5689_Com,
            [AFF16096_Code] = FakeMapping_Join_Best5689_Com,
            [AFF16097_Code] = FakeMapping_Join_Best5689_Com,

            [AFF16325_Code] = "aff-12b.nexdev.net/Go,go.5689fun.com",
            [AFF16326_Code] = "aff-12b.nexdev.net/Go,go.6789sutzo.com",

            //12aff.best5689.com
            [AFF15932_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF130_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16311_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16314_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16225_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF14617_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF13902_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16313_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF15775_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16214_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF13506_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF13065_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF7782_Code] = FakeMapping_12aff_Best5689_Com,
            [AFF16050_Code] = FakeMapping_12aff_Best5689_Com,

            [AFF16342_Code] = "aff-12b.nexdev.net/Go,go.apa56.com",
            [AFF16343_Code] = "aff-12b.nexdev.net/Go,go.9012ijk.com",
            [AFF16344_Code] = "aff-12b.nexdev.net/Go,go.sbc31.com",
        };
    }
}