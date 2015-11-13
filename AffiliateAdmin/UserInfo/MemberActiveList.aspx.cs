using System;
using System.Data;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.Constants;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_MemberActiveList : IBCPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MemActiveList.htm");


        if (!Common.HasHostPermission(AppSettings.Session.CustID, PermissionNames.ActiveMembers))
        {
            TemplateHandler.RemoveBlock(ref strTpl, "active-button");
        }

        TemplateHandler.Assign(ref strTpl, "username-text", Resources.Lang.Username);        
        TemplateHandler.Assign(ref strTpl, "ibcusername-text", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTpl, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTpl, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTpl, "ActivateAll", Resources.Lang.ActivateAll);
        TemplateHandler.Assign(ref strTpl, "Activate", Resources.Lang.Active);        
        TemplateHandler.Assign(ref strTpl, "ibccustid-text", Resources.Lang.IBCCustID);        
        TemplateHandler.Assign(ref strTpl, "CreateDate", Resources.Lang.CreateDate);
        TemplateHandler.Assign(ref strTpl, "search", "Check Status");
        TemplateHandler.Assign(ref strTpl, "pagepadd", "padl0");
        //submenu      
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.MemberLink, SubMenu.MemberText, 1, 0, string.Empty, "i_member.png"));

        try
        {                        
            string search = string.Empty;
            string username = string.Empty, lstNotActivatedUser = string.Empty;

            username = string.IsNullOrEmpty(Request["txtUsername"]) ? string.Empty : Request["txtUsername"].Trim();
            search = username.ToLower().Replace(" ", string.Empty);

            TemplateHandler.Assign(ref strTpl, "username", string.IsNullOrEmpty(username) ? string.Empty : username);
            if (string.IsNullOrEmpty(username))
            {
                TemplateHandler.RemoveBlock(ref strTpl, "noone");
                this.HtmlContent = strTpl;
                return;
            }


            #region activated already

            string strContent2 = TemplateHandler.CopyBlock(strTpl, "content2");            
            
            StringBuilder strBuild2 = new StringBuilder();

            if (!string.IsNullOrEmpty(username))
            {
                DataTable dtbResult2 = DataHandler.Member.SelActiveMemberInfo(search);

                if (dtbResult2.Rows.Count > 0)
                {
                    int i = 0;
                    TemplateHandler.Assign(ref strContent2, "over", Common.RowBgOver);
                    foreach (DataRow dr in dtbResult2.Rows)
                    {
                        i++;
                        string strTmp = strContent2;
                        TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                        TemplateHandler.Assign(ref strTmp, "number", i.ToString());

                        TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                        TemplateHandler.Assign(ref strTmp, "ibcusername-val", dr["ibcusername"].ToString());

                        TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"].ToString());
                        TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"].ToString());

                        TemplateHandler.Assign(ref strTmp, "ibccustid-val", dr["ibccustid"].ToString());
                        TemplateHandler.Assign(ref strTmp, "CreateDate-val", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dr["createddate"]));

                        strBuild2.Append(strTmp);

                        search = search.Replace(dr["username"].ToString(), string.Empty);
                    }

                }
            }

            TemplateHandler.ReplaceBlock(ref strTpl, "content2", strBuild2.ToString());
            #endregion
           
            #region not activated        
    
            string strContent1 = TemplateHandler.CopyBlock(strTpl, "content1");

            StringBuilder strBuild1 = new StringBuilder();

            if (!string.IsNullOrEmpty(username))
            {
                DataTable dtbResult1 = DataHandler.Member.SelNotActiveMemberInfo(search);

                if (dtbResult1.Rows.Count > 0)
                {
                    int i = 0;
                    TemplateHandler.Assign(ref strContent1, "over", Common.RowBgOver);
                    foreach (DataRow dr in dtbResult1.Rows)
                    {
                        i++;
                        string strTmp = strContent1;
                        TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                        TemplateHandler.Assign(ref strTmp, "number", i.ToString());

                        TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                        TemplateHandler.Assign(ref strTmp, "username-val-err", dr["username"].ToString() + "-err");

                        TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"].ToString());
                        TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"].ToString());

                        strBuild1.Append(strTmp);

                        lstNotActivatedUser += dr["username"].ToString() + ",";
                        search = search.Replace(dr["username"].ToString(), string.Empty);
                    }
                }
            }
            TemplateHandler.Assign(ref strTpl, "notActivatedMembers", lstNotActivatedUser);                        
            TemplateHandler.ReplaceBlock(ref strTpl, "content1", strBuild1.ToString());            
            #endregion

            #region not exists

            string str = string.Empty;
            string[] arr = search.Split(',');
            foreach (string item in arr)
            {                
                if (!string.IsNullOrEmpty(item.Trim()))
                {
                    str += item.Trim() + ", ";
                }
            }
            if (str.Length > 2)
            {
                TemplateHandler.Assign(ref strTpl, "members", str.Remove(str.Length - 2));
            }
            else
            {
                TemplateHandler.RemoveBlock(ref strTpl, "notexistsmember");
            }

            #endregion

            
            
            //Add search bar            
            string strUserName = CastAide.AsString(Request["txtUsername"], string.Empty);
            strUserName = ((string.Compare(strUserName, Resources.Lang.Username, true) == 0 ? string.Empty : strUserName));
            

            this.HtmlContent = strTpl;
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            Response.Write(ex.Message);
        }
    }
}
