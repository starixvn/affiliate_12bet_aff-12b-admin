using System;
using System.Data;
using System.Collections;
using DNAide.Data;


/// <summary>
/// Summary description for CommGroup
/// </summary>
namespace DataHandler
{
    public class Common
    {
        public static bool IsNeedRedirect(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(AppSettings.RedirectionEuCountryList))
            {
                return false;
            }

            Hashtable param = new Hashtable();
            param.Add("ip", ipAddress);
            var result = DbHelper.Execute("GetIPInfo", param, DBType.IPDB);

            if (result.ResultSet.Tables.Count > 0)
            {
                var table = result.ResultSet.Tables[0];
                var rows = table.Select("Code in (" + AppSettings.RedirectionEuCountryList + ")");
                return rows.Length > 0;
            }

            return false;
        }
    }
}
