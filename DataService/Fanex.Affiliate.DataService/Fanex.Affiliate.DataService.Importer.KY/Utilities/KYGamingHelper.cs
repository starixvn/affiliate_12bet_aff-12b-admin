namespace Fanex.Affiliate.DataService.Importer.KYGaming.Utilities
{
    public static class KYGamingHelper
    {
        public static int GetAgent(string Currency)
        {
            switch (Currency)
            {
                case "MYR":
                    return 120213;

                case "IDR":
                    return 120216;

                case "THB":
                    return 120214;

                case "VND":
                    return 120215;

                case "KRW":
                    return 120217;

                default: //RMB
                    return 120127;
            }
        }

        public static string GetDESKey(string Currency)
        {
            switch (Currency)
            {
                case "MYR":
                    return "767B94CF179B84ED";

                case "IDR":
                    return "3A54CC398BB76550";

                case "THB":
                    return "7DB30FEE35568429";

                case "VND":
                    return "5FD52CC82392C450";

                case "KRW":
                    return "503F4DBE2B316E09";

                default: //RMB
                    return "CD90BDB64D035297";
            }
        }

        public static string GetMd5Key(string Currency)
        {
            switch (Currency)
            {
                case "MYR":
                    return "AD085B09D9CC8880";

                case "IDR":
                    return "E7E5D15B73245B8D";

                case "THB":
                    return "A5726BAED0EC4224";

                case "VND":
                    return "59D4A696F9A4E86B";

                case "KRW":
                    return "816A74DC3EB47E38";

                default: //RMB
                    return "628EA93FDD1035F3";
            }
        }
    }
}
