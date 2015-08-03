using System;
using System.Web;
using System.Collections;
using System.Data;
using DNAide.Data;
namespace DataHandler
{
    public class ExchangeRate
    {
        public static DataTable Exchange_Sel(DateTime dtmTime)
        {
            Hashtable hsParams = new Hashtable();
            hsParams.Add("time", dtmTime);
            return DbHelper.Execute("Aff_ExchangeSel", hsParams, DBType.AFFILIATE_12BET).ResultSet.Tables[0];
        }
        public static DbResult Exchange_Update(int exId, decimal Rate, string column = null)
        {
            Hashtable hsParams = new Hashtable();
            //hsParams.Add("affmaster", AppSettings.AffMaster);
            hsParams.Add("custid", AppSettings.Session.CustID);
            hsParams.Add("exchangeid", exId);
            hsParams.Add("rate", Rate);
            hsParams.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParams.Add("errcode", 0);
            hsParams.Add("errmsg", string.Empty);
            if(column == "buyingrate")
                hsParams.Add("ratetype", 1);
            else
                hsParams.Add("ratetype", 0);
                
            return DbHelper.Execute("Aff_ExchangeUpdate", hsParams, DBType.AFFILIATE_12BET);
        }
     
        /*public static DbResult Exchange_Update_RptDepWinlost(int exId)
        {
            Hashtable p = new Hashtable();       
            p.Add("exchangeid", exId);           
            p.Add("errcode", 0);
            p.Add("errmsg", string.Empty);
            return AffSPExecuter.ExecuteCmd("Aff_ExchangeUpdateMemberDaily", p, DBType.AFFILIATE_12BET);
        }*/
    }
}
	