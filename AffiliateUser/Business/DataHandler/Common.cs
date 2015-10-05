using System;
using System.Linq;
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
            var result = DbHelper.Execute("Aff_GetIPInfo", param, DBType.IPDB);

            if (result.ResultSet.Tables.Count == 0)
            {
                return false;
            }

            var table = result.ResultSet.Tables[0];
            if(table.Rows.Count > 0)
            {
                var countryCodes = AppSettings.RedirectionEuCountryList.Split(',');
                var countryCodesString = string.Join(",", countryCodes.Select(x => string.Format("'{0}'", x)));

                var rows = table.Select("Code in (" + countryCodesString + ")");
                return rows.Length > 0;
            }

            return false;
        }
    }
}
