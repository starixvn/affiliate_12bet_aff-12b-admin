
/// <summary>
/// Summary description for DBType
/// </summary>
public class DBType
{
    private static string _main = "main";
    private static string _mainView = "main_view";
    private static string _archive = "archive";
    private static string _archiveView = "archive_view";
    private static string _affiliate = "affiliate";
    private static string _affiliateView = "affiliate_view";
    private static string _affiliate_12bet = "affiliate_12bet";
    private static string _affiliate_12betView = "affiliate_12bet_view";
    private static string _onUser = "on_user";
    private static string _onUserView = "on_user_view";
    private static string _wareHouse = "warehouse";
    private static string _wareHouseView = "warehouse_view";
    private static string _deposit = "deposit";
    private static string _depositView = "deposit_view";
    
    public static string MAIN
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _mainView : _main;         
        }
    }

    public static string ARCHIVE
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _archiveView : _archive;            
        }
    }

    public static string AFFILIATE
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _affiliateView : _affiliate;            
        }
    }
    public static string AFFILIATE_12BET
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _affiliate_12betView : _affiliate_12bet;
        }
    }

    public static string ON_USER
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _onUserView : _onUser;            
        }
    }

    public static string WAREHOUSE
    {
        get
        {
            return (AppSettings.Session.IsInternalSite) ? _wareHouseView : _wareHouse;            
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
