using System;
using System.Collections;
using System.Data;
using System.Web;
using DNAide.Core;
using AffiliateAdmin.Business.DataAccess;
/// <summary>
/// Summary description for PaymentRequest
/// </summary>
namespace DataHandler
{
    public class PaymentRequest
    {
        public static DbResult PaymentRequestList(string strSearchBy, string strUsername, string strStatus, DateTime dtmFromDate, DateTime dtmToDate, bool isGetAll, int page, int pagesize)
        {
            Hashtable hsParam = new Hashtable();
            if (string.Compare(strSearchBy, "0", true) == 0)//Search by Username
            {
                hsParam.Add("getall", true);
                hsParam.Add("username", strUsername);
            }
            else
            {
                hsParam.Add("getall", isGetAll);
                if (!string.IsNullOrEmpty(strStatus))
                    hsParam.Add("status", strStatus);                
                hsParam.Add("fromdate", dtmFromDate);
                hsParam.Add("todate", dtmToDate.AddDays(1).AddMilliseconds(-3));
            }
            hsParam.Add("pagesize", pagesize);
            hsParam.Add("pageindex", page);
            hsParam.Add("totalpages", 0);
            return DbHelper.Execute("Aff_PaymentRequestSelList", DBType.AFFILIATE_12BET, hsParam);
        }
        public static DataTable PaymentRequestById(Int64 TransId)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("transid", TransId);
            return DbHelper.Execute("Aff_PaymentRequestSelDetail", DBType.AFFILIATE_12BET, hsParam).ResultSet.Tables[0];
        }
        public static int PaymentUpdate(Int64 intTransId, string strStatus, string strLang, string strAdminRemark, string strAffRemark, ref string strPreferredLang, ref bool blMailOption)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("status", HttpContext.Current.Server.HtmlEncode(strStatus));
            hsParam.Add("transid", intTransId);
            hsParam.Add("admindesc", HttpContext.Current.Server.HtmlEncode(strAdminRemark));
            hsParam.Add("userid", AppSettings.Session.CustID);
            hsParam.Add("affdesc", HttpContext.Current.Server.HtmlEncode(strAffRemark));
            hsParam.Add("preferredlanguage", "E");
            hsParam.Add("mailoption", blMailOption);
            hsParam.Add("language", strLang);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            DbResult result=null;
            if (strStatus.ToLower().CompareTo("approved") == 0)
                result = DbHelper.Execute("Aff_PaymentRequestApprove", DBType.MAIN, hsParam);
            else
                result = DbHelper.Execute("Aff_PaymentRequestUpd", DBType.AFFILIATE_12BET, hsParam);

            strPreferredLang = result.Outputs["preferredlanguage"].ToString();
            blMailOption = CastAide.AsBoolean(result.Outputs["mailoption"]);
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
       
    }
}
