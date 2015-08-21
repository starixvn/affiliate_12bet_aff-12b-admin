using System;
using System.Data;
using System.Collections;
using DNAide.Data;


/// <summary>
/// Summary description for CommGroup
/// </summary>
namespace DataHandler
{
    public class CommGroup
    {
        public CommGroup()
        {
        }

        public static DbResult GetCommGroupSettingByAffId(int affid)
        {
            Hashtable param = new Hashtable();
            param.Add("affid", affid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", param, DBType.AFFILIATE_12BET);
        }

        public static DbResult GetCommGroupSettingByGroupId(int groupid)
        {
            Hashtable param = new Hashtable();
            param.Add("groupid", groupid);
            return DbHelper.Execute("Aff_CommGroupSelDetail", param, DBType.AFFILIATE_12BET);
        }
    }
}
