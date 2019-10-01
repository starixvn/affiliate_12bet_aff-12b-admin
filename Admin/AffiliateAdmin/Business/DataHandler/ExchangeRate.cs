using System;
using System.Web;
using System.Collections;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
namespace DataHandler
{
    public class ExchangeRate
    {
        public static DataTable Exchange_Sel(DateTime dtmTime)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("time", dtmTime);
            return DbHelper.Execute("Aff_ExchangeSel", DBType.AFFILIATE_12BET, hsParams).ResultSet.Tables[0];
        }
    }
}
	