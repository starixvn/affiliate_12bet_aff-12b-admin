
/// <summary>
/// Summary description for DBType
/// </summary>
public class DBType
{
    private static string _main = "main";
    private static string _mainView = "main_view";
    private static string _affiliate_12bet = "affiliate_12bet";
    private static string _affiliate_12betView = "affiliate_12bet_view";
    private static string _deposit = "deposit";
    private static string _depositView = "deposit_view";
    
    public static string MAIN
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _mainView : _main;         
        }
    }

    public static string AFFILIATE_12BET
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _affiliate_12betView : _affiliate_12bet;
        }
    }

    public static string DEPOSIT
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _depositView : _deposit;
        }
    }
}
