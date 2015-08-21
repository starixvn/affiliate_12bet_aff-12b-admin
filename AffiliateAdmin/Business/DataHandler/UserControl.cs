using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using System.Collections;
using System.Web;
/// <summary>
/// Summary description for UserControl
/// </summary>
/// 
namespace DataHandler
{
    public class UserControl
    {
        public static string GetListAlPhabet(string currSearch)
        {
            StringBuilder html = new StringBuilder();
            string[] arrAlpha ={"All", "0-9","A","B","C","D","E","F","G","H","I","J","K","L","M", 
            "N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };
            foreach (string str in arrAlpha)
            {
                if (string.Compare(str, currSearch, true) == 0)
                {
                    html.Append("<option selected=\"true\" value=\"" + str + "\">" + str + "</option>");
                }
                else
                {
                    html.Append("<option value=\"" + str + "\">" + str + "</option>");
                }
            }
            return html.ToString();
        }
        public static DbResult GetListUser(int groupid, string username, string firstchar)
        {
            DbResult dbr = null;
            Hashtable htb = new Hashtable();
            if (groupid != 0)
            {
                htb.Add("groupid", groupid);
            }
            if (firstchar != "All")
            {
                if (firstchar == "0-9")
                    firstchar = "[" + firstchar + "]";
                htb.Add("firstchar", firstchar + "%");
            }
            if (username != "Keyword")
            {
                htb.Add("username", "%" + username + "%");
            }
            //  htb.Add("system", "AFFILIATE");
            dbr = DbHelper.Execute("Aff_Con_AdminSelByGroupID", DBType.AFFILIATE_12BET, htb);
            return dbr;

        }
        public static int UpdateStatusAccount(int custid, bool closed, ref string errmsg)
        {
            //Acc_Con_AdminUpdClosed
            int errcode = -1;
            Hashtable htb = new Hashtable();
            htb.Add("custid", custid);
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("closed", closed);
            htb.Add("language", "E");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            //htb.Add("system", "AFFILIATE");

            DbResult dbr = DbHelper.Execute("Aff_Con_AdminUpdClosed", DBType.AFFILIATE_12BET, htb);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();

            return errcode;
        }

        public static DbResult GetSelectDepartment()
        {
            Hashtable htb = new Hashtable();
            // htb.Add("system", "Affiliate");
            DbResult dbr = DbHelper.Execute("Aff_Con_GroupInfoSel", DBType.AFFILIATE_12BET, htb);
            return dbr;
        }
        public static int AddNewUser(string firstname, string lastname, string username, string password, int groupid, ref string errmsg)
        {
            Hashtable htb = new Hashtable();
            int errcode = -1;
            htb.Add("firstname", HttpContext.Current.Server.HtmlEncode(firstname));
            htb.Add("lastname", HttpContext.Current.Server.HtmlEncode(lastname));
            htb.Add("username", HttpContext.Current.Server.HtmlEncode(username.Trim()));
            htb.Add("userpwd", Encryption.CfsEnCode(password.Trim()));
            htb.Add("custid", 0);
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("groupid", groupid);
            //htb.Add("system", "AFFILIATE");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            DbResult dbr = DbHelper.Execute("Aff_Con_AdminIns", DBType.AFFILIATE_12BET, htb);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();

            return errcode;
        }
        public static int AddNewUserExist(string username, int groupid, ref string errmsg)
        {
            Hashtable htb = new Hashtable();
            int errcode = -1;

            htb.Add("username", HttpContext.Current.Server.HtmlEncode(username.Trim()));
            htb.Add("creator", AppSettings.Session.CustID);
            htb.Add("groupid", groupid);
            //htb.Add("system", "AFFILIATE");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");

            DbResult dbr = DbHelper.Execute("Aff_Con_AdminInsFromExisted", DBType.AFFILIATE_12BET, htb);
            errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
            errmsg = dbr.Outputs["errmsg"].ToString();
            return errcode;
        }
        public static DbResult GetListDepartment(string groupid)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", groupid);

            return DbHelper.Execute("Aff_Con_GroupInfoSel", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult UpdateDepartment(int groupID, string groupName, string groupDesc, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", groupID);
            htb.Add("groupname", HttpContext.Current.Server.HtmlEncode(groupName));
            htb.Add("groupdesc", HttpContext.Current.Server.HtmlEncode(groupDesc));
            htb.Add("language", "E");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            htb.Add("userid", userID);
            // htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_GroupInfoUpd", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult AddDepartment(string groupName, string groupDesc, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", 0);
            htb.Add("groupname", HttpContext.Current.Server.HtmlEncode(groupName));
            htb.Add("groupdesc", HttpContext.Current.Server.HtmlEncode(groupDesc));
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            //htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_GroupInfoIns", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult DelDepartment(int groupID, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", groupID);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            //  htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_GroupInfoDel", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult GetOnlyUser(int custid)
        {
            Hashtable htb = new Hashtable();
            htb.Add("custid", custid);
            // htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_AdminSelByCustID", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult UpdateUser(int custid, string firstname, string lastname, int groupid)
        {
            //Acc_Con_AdminUpd_1
            Hashtable htb = new Hashtable();
            htb.Add("custid", custid);
            htb.Add("groupid", groupid);
            htb.Add("firstname", HttpContext.Current.Server.HtmlEncode(firstname));
            htb.Add("lastname", HttpContext.Current.Server.HtmlEncode(lastname));
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            //     htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_AdminUpd", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult GetGroupPolicy(int groupId)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", groupId);
            return DbHelper.Execute("Aff_Con_GroupPolicySel", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult GetUserPolicy(int custId)
        {
            Hashtable htb = new Hashtable();
            htb.Add("custid", custId);
            htb.Add("groupid", 0);
            // htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_AdminPolicySelByCustID", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult UpdateUserPolicy(string memberPolicies, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("mempolicys", HttpContext.Current.Server.HtmlEncode(memberPolicies));
            htb.Add("userid", userID);
            htb.Add("language", "E");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            // htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_AdminPolicyUpd", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult UpdateGroupPolicy(string groupPolicies, int userID)
        {
            Hashtable htb = new Hashtable();
            htb.Add("grouppolicys", HttpContext.Current.Server.HtmlEncode(groupPolicies));
            htb.Add("userid", userID);
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", string.Empty);
            //htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_GroupPolicyUpd", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult GetMultUpdPolicy(string custId)
        {
            Hashtable htb = new Hashtable();
            htb.Add("custids", custId);
            htb.Add("groupid", 0);
            //htb.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_Batch_AdminPolicySel", DBType.AFFILIATE_12BET, htb);
        }
        public static DbResult GetFunctionInfo()
        {
            Hashtable param = new Hashtable();
            //param.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_FuncInfoSel", DBType.AFFILIATE_12BET, param);
        }
        public static DbResult GetOnlyFunctionInfo(int funcid)
        {
            Hashtable param = new Hashtable();
            param.Add("funcid", funcid);
            return DbHelper.Execute("Aff_Con_FuncInfoSelByFuncID", DBType.AFFILIATE_12BET, param);
        }
        public static DbResult FunctionParent()
        {
            Hashtable param = new Hashtable();
            //param.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_FuncInfoSelParent", DBType.AFFILIATE_12BET, param);
        }

        public static DbResult InsertFunction(string strName, string strDesc, string strFileNames, string strParent, int intUserId)
        {
            Hashtable param = new Hashtable();
            param.Add("funcid", 0);
            param.Add("funcname", HttpContext.Current.Server.HtmlEncode(strName));
            param.Add("funcnote", "");
            param.Add("funcdesc", HttpContext.Current.Server.HtmlEncode(strDesc));
            param.Add("displayorder", 1);
            if (!string.IsNullOrEmpty(strParent))
                param.Add("parentid", HttpContext.Current.Server.HtmlEncode(strParent));
            param.Add("filenames", HttpContext.Current.Server.HtmlEncode(strFileNames));
            param.Add("userid", intUserId);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            param.Add("language", "E");
            param.Add("errcode", 0);
            param.Add("errmsg", "");
            // param.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_FuncInfoIns", DBType.AFFILIATE_12BET, param);
        }
        public static DbResult UpdateFunction(int intFuncId, int intUserId, string strName, string strDesc, string strFileNames)
        {
            Hashtable param = new Hashtable();
            param.Add("funcid", intFuncId);
            param.Add("funcname", HttpContext.Current.Server.HtmlEncode(strName));
            param.Add("funcnote", "");
            param.Add("funcdesc", HttpContext.Current.Server.HtmlEncode(strDesc));
            param.Add("displayorder", 1);
            param.Add("filenames", HttpContext.Current.Server.HtmlEncode(strFileNames));
            param.Add("userid", intUserId);
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            param.Add("language", "E");
            param.Add("errcode", 0);
            param.Add("errmsg", "");
            // param.Add("system", "AFFILIATE");
            return DbHelper.Execute("Aff_Con_FuncInfoUpd", DBType.AFFILIATE_12BET, param);
        }

        public static DbResult DeleteFunction(int intFuncId, int intUserId)
        {
            Hashtable param = new Hashtable();
            param.Add("funcid", intFuncId);
            param.Add("language", "E");
            param.Add("errcode", 0);
            param.Add("errmsg", "");
            param.Add("userid", intUserId);
            //param.Add("system", "AFFILIATE");
            param.Add("ip", HttpContext.Current.Request.UserHostAddress);
            return DbHelper.Execute("Aff_Con_FuncInfoDel", DBType.AFFILIATE_12BET, param);
        }
    }
}
