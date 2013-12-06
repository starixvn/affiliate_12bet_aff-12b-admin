using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;


namespace DataHandler
{
    /// <summary>
    /// Summary description for MediaGallery
    /// </summary>
    public class MediaGallery
    {
        public static DataTable SelAffDomains()
        {            
            Hashtable ht = new Hashtable();
            ht.Add("affid", AppSettings.Session.CustID);
            return DbHelper.Execute("Aff_DomainAffClientSel", ht, DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        }
    }
}