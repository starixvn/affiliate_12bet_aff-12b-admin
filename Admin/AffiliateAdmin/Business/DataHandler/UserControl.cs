using AffiliateAdmin.Business.DataAccess;

namespace DataHandler
{
    public static class UserControl
    {
        public static DbResult GetFunctionInfo()
        {
            return DbHelper.Execute("Aff_Con_FuncInfoSel", DBType.AFFILIATE_12BET);
        }
    }
}
