/// <summary>
/// Summary description for DBType
/// </summary>
public class DBType
{
    private static string _main = "main";
    private static string _custdb = "custdb";
    private static string _deposit = "deposit";
    private static string _affiliate_12bet = "affiliate_12bet";

    public static string MAIN
    {
        get
        {
            return _main;
        }
    }

    public static string CustDB
    {
        get
        {
            return _custdb;
        }
    }
   
    public static string AFFILIATE_12BET
    {
        get
        {
            return _affiliate_12bet;
        }
    }

    public static string DEPOSIT
    {
        get
        {
            return _deposit;
        }
    }
}
