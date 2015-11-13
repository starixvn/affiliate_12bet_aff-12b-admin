using System.Collections;
using AffiliateUser.Business.DataAccess;


/// <summary>
/// Summary description for CommGroup
/// </summary>
namespace DataHandler
{
    public class CommGroup
    {
        public static DbResult GetCommGroupSettingByAffId(int affid)
        {
            Hashtable param = new Hashtable();
            param.Add("affid", affid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", DBType.AFFILIATE_12BET, param);
        }

        public static DbResult GetCommGroupSettingByGroupId(int groupid)
        {
            Hashtable param = new Hashtable();
            param.Add("groupid", groupid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", DBType.AFFILIATE_12BET, param);
        }
    }
}
