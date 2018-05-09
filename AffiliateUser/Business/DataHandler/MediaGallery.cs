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
        public static DataTable SelAffDomains()
        {
            var ht = new Hashtable();
            ht.Add("affid", AppSettings.Session.CustID);
            return DbHelper.Execute("Aff_DomainAffClientSel", DBType.AFFILIATE_12BET, ht).ResultSet.Tables[0];
        }
    }
}