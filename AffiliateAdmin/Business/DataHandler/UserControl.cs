using System.Text;
using DNAide.Data;
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
                htb.Add("username","%" + username + "%");
            }
          //  htb.Add("system", "AFFILIATE");
            dbr = DbHelper.Execute("Aff_Con_AdminSelByGroupID", htb, DBType.AFFILIATE_12BET);
            return dbr;
            
        }
        public static int UpdateStatusAccount(int custid, bool closed,ref string errmsg)
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
            try
            {

                DbResult dbr = DbHelper.Execute("Aff_Con_AdminUpdClosed", htb, DBType.AFFILIATE_12BET);
                errcode=CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
            }
            catch 
            {
                
                throw;
            }
            return errcode;


        }
      
        public static DbResult GetSelectDepartment()
        {

            Hashtable htb = new Hashtable();
           // htb.Add("system", "Affiliate");
            DbResult dbr = DbHelper.Execute("Aff_Con_GroupInfoSel", htb, DBType.AFFILIATE_12BET);
            return dbr;
        }
        public static int AddNewUser(string firstname, string lastname, string username, string password, int groupid, ref string errmsg)
        {
            Hashtable htb = new Hashtable();
            int errcode = -1;
            htb.Add("firstname", HttpContext.Current.Server.HtmlEncode(firstname));
            htb.Add("lastname", HttpContext.Current.Server.HtmlEncode(lastname));
            htb.Add("username", HttpContext.Current.Server.HtmlEncode(username.Trim()));
            htb.Add("userpwd",Encryption.CfsEnCode(password.Trim()));
            htb.Add("custid", 0);
            htb.Add("userid", AppSettings.Session.CustID);
            htb.Add("groupid", groupid);
            //htb.Add("system", "AFFILIATE");
            htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
            htb.Add("language", "E");
            htb.Add("errcode", 0);
            htb.Add("errmsg", "");
            try
            {
                DbResult dbr = DbHelper.Execute("Aff_Con_AdminIns", htb, DBType.AFFILIATE_12BET);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
            }
            catch 
            {
                throw;
            }
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
            try
            {
                DbResult dbr = DbHelper.Execute("Aff_Con_AdminInsFromExisted", htb, DBType.AFFILIATE_12BET);
                errcode = CastAide.AsInt32(dbr.Outputs["errcode"]);
                errmsg = dbr.Outputs["errmsg"].ToString();
            }
            catch
            {
                throw;
            }
            return errcode;
        }
        public static DbResult GetListDepartment(string groupid)
        {
            Hashtable htb = new Hashtable();
            htb.Add("groupid", groupid);
            htb.Add("system", "AFFILIATE");
            DbResult dbr=null;
            try
            {

                dbr = DbHelper.Execute("Aff_Con_GroupInfoSel", htb, DBType.AFFILIATE_12BET); 
            }
            catch {

                throw;
            }
            return dbr;
        }
        public static DbResult UpdateDepartment(int groupID, string groupName, string groupDesc, int userID)
        {

            DbResult dr = null;
            try
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
                dr = DbHelper.Execute("Aff_Con_GroupInfoUpd", htb, DBType.AFFILIATE_12BET);               
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult AddDepartment(string groupName, string groupDesc, int userID)
        {
            DbResult dr = null;
            try
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
                dr = DbHelper.Execute("Aff_Con_GroupInfoIns", htb, DBType.AFFILIATE_12BET);
                
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult DelDepartment(int groupID, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("groupid", groupID);
                htb.Add("language", "E");
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
              //  htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Con_GroupInfoDel", htb, DBType.AFFILIATE_12BET);                
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult GetOnlyUser(int custid)
        { 
            
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("custid", custid);
               // htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Con_AdminSelByCustID", htb, DBType.AFFILIATE_12BET);                
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult UpdateUser(int custid, string firstname, string lastname,int groupid)
        {
            //Acc_Con_AdminUpd_1
            Hashtable htb = new Hashtable();
            DbResult dr = null;
            try
            {
                htb.Add("custid", custid);
                htb.Add("groupid", groupid);
                htb.Add("firstname", HttpContext.Current.Server.HtmlEncode(firstname));
                htb.Add("lastname", HttpContext.Current.Server.HtmlEncode(lastname));
                htb.Add("userid", AppSettings.Session.CustID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
           //     htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Con_AdminUpd", htb, DBType.AFFILIATE_12BET);
            }
            catch 
            {
                
                throw;
            }
            return dr;
        }
        public static DbResult GetGroupPolicy(int groupId)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("groupid", groupId);
                return DbHelper.Execute("Aff_Con_GroupPolicySel", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
        }
        public static DbResult GetUserPolicy(int custId)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("custid", custId);
                htb.Add("groupid", 0);
               // htb.Add("system", "AFFILIATE");
                return DbHelper.Execute("Aff_Con_AdminPolicySelByCustID", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
        }
        public static DbResult UpdateUserPolicy(string memberPolicies, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("mempolicys", HttpContext.Current.Server.HtmlEncode(memberPolicies));
                htb.Add("userid", userID);
                htb.Add("language", "E");
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
               // htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Con_AdminPolicyUpd", htb, DBType.AFFILIATE_12BET);                
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult UpdateGroupPolicy(string groupPolicies, int userID)
        {
            DbResult dr = null;
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("grouppolicys", HttpContext.Current.Server.HtmlEncode(groupPolicies));
                htb.Add("userid", userID);
                htb.Add("ip", HttpContext.Current.Request.UserHostAddress);
                htb.Add("language", "E");
                htb.Add("errcode", 0);
                htb.Add("errmsg", string.Empty);
                //htb.Add("system", "AFFILIATE");
                dr = DbHelper.Execute("Aff_Con_GroupPolicyUpd", htb, DBType.AFFILIATE_12BET);                
            }
            catch
            {
                throw;
            }
            return dr;
        }
        public static DbResult GetMultUpdPolicy(string custId)
        {
            try
            {
                Hashtable htb = new Hashtable();
                htb.Add("custids", custId);
                htb.Add("groupid", 0);
                //htb.Add("system", "AFFILIATE");
                return DbHelper.Execute("Aff_Con_Batch_AdminPolicySel", htb, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
        }
        public static DbResult GetFunctionInfo()
        {
            try
            {
                Hashtable param = new Hashtable();
                //param.Add("system", "AFFILIATE");
                return DbHelper.Execute("Aff_Con_FuncInfoSel", param, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
        }
        public static DbResult GetOnlyFunctionInfo(int funcid)
        { 
            
            try
            {
                Hashtable param = new Hashtable();                
                param.Add("funcid", funcid);
                return DbHelper.Execute("Aff_Con_FuncInfoSelByFuncID", param, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
        }
        public static DbResult FunctionParent()
        {
            try
            {
                Hashtable param = new Hashtable();
                //param.Add("system", "AFFILIATE");
                return DbHelper.Execute("Aff_Con_FuncInfoSelParent", param, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
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
            DbResult dr = null;
            try
            {
                dr = DbHelper.Execute("Aff_Con_FuncInfoIns", param, DBType.AFFILIATE_12BET);
            }
            catch 
            {
                throw;
            }
            return dr;
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
            DbResult dr = null;
            try
            {
                dr = DbHelper.Execute("Aff_Con_FuncInfoUpd", param, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
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
            DbResult dr = null;
            try
            {
                dr = DbHelper.Execute("Aff_Con_FuncInfoDel", param, DBType.AFFILIATE_12BET);
            }
            catch
            {
                throw;
            }
            return dr;
        }
        

    }
}
