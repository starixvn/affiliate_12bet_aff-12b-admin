using System.Collections;
using System.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;

namespace DataHandler
{
    public class Account
    {
        public static int ChangePass(int intCustId, string strOldPass, string strNewPass, string strLang)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("custid", intCustId);
            hsParam.Add("userpwd", Encryption.CfsEnCode(strNewPass));
            hsParam.Add("oldpwd", Encryption.CfsEnCode(strOldPass));
            hsParam.Add("userid", intCustId);
            hsParam.Add("language", strLang);
            hsParam.Add("ip", HttpContext.Current.Request.UserHostAddress);
            hsParam.Add("errcode", "0");
            DbResult result = DbHelper.Execute("Aff_AdminUpdPwd", hsParam, DBType.AFFILIATE_12BET);
            return CastAide.AsInt32(result.Outputs["errcode"]);
        }
    }
}
