using System.Collections;
using System.Data;
using AffiliateUser.Business.DataAccess;

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
            return DbHelper.Execute("Aff_DomainAffClientSel_xtest", DBType.AFFILIATE_12BET, ht).ResultSet.Tables[0];
        }
    }
}