using System.Data;
using System.Collections;

namespace DataHandler
{
    /// <summary>
    /// Summary description for MediaGallery
    /// </summary>
    public class MediaGallery
    {
        public static DataTable SelAffDomains(string deviceType)
        {            
            var ht = new Hashtable();
            ht.Add("affid", AppSettings.Session.CustID);
            ht.Add("devicetype", byte.Parse(deviceType));
            return DbHelper.Execute("Aff_DomainAffClientSel", ht, DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        }
    }
}