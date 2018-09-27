using System;
using System.Data;
using System.Text;
using System.Web;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_MemberListAll : IBCPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MemListAll.htm");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "action", Resources.Lang.Action);
        TemplateHandler.Assign(ref strTpl, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref strTpl, "username-text", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "ibcusername-text", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTpl, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTpl, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTpl, "Reset", Resources.Lang.Reset);
        TemplateHandler.Assign(ref strTpl, "CurrentBalanceMem", Resources.Lang.CurrentBalanceMem);
        TemplateHandler.Assign(ref strTpl, "Reset", Resources.Lang.Reset);
        TemplateHandler.Assign(ref strTpl, "password", Resources.Lang.Password);
        TemplateHandler.Assign(ref strTpl, "Back", Resources.Lang.Back);
        TemplateHandler.Assign(ref strTpl, "CreateDate", Resources.Lang.CreateDate);
        TemplateHandler.Assign(ref strTpl, "LastLogging", Resources.Lang.LastLogging);
        TemplateHandler.Assign(ref strTpl, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "remove", Resources.Lang.Remove);

        try
        {
            string username = string.Empty, ibcUsername = string.Empty, affUsername = string.Empty;
            int totalPages = 1, pageIndex = 0, NRows = 0;//affid = -1
            int width = 900;

            TemplateHandler.Assign(ref strTpl, "pagepadd", "padl0");
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");

            username = string.IsNullOrEmpty(Request["txtUsername"]) ? string.Empty : Request["txtUsername"].Trim();
            ibcUsername = string.IsNullOrEmpty(Request["txtIBCUsername"]) ? string.Empty : Request["txtIBCUsername"].Trim();
            affUsername = string.IsNullOrEmpty(Request["txtAffUsername"]) ? string.Empty : Request["txtAffUsername"].Trim();

            pageIndex = CastAide.AsInt32(Request["page"], 1);

            TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
            TemplateHandler.Assign(ref strTpl, "width", width);

            //check sample data of search criteria
            username = (String.Compare(username, string.Empty, true) == 0) ? null : username;
            ibcUsername = (String.Compare(ibcUsername, string.Empty, true) == 0) ? null : ibcUsername;

            int intPage = CastAide.AsInt32(Request["page"], 1);
            int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
            DataTable dtbResult = DataHandler.Member.SelMemInfoAll(affUsername, username, ibcUsername, ref pageIndex, intPageSize, ref totalPages, ref NRows);

            StringBuilder strBuild = new StringBuilder();

            if (dtbResult.Rows.Count > 0)
            {
                int i = (pageIndex - 1) * intPageSize;// hardcode pagesize for member/player list
                TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
                foreach (DataRow dr in dtbResult.Rows)
                {
                    i++;
                    string strTmp = strContent;
                    TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref strTmp, "number", i.ToString());
                    TemplateHandler.Assign(ref strTmp, "username-val", dr["username"].ToString());
                    TemplateHandler.Assign(ref strTmp, "IBCCustID-val", dr["ibccustid"].ToString());

                    TemplateHandler.Assign(ref strTmp, "ibcusername-val", dr["ibcusername"].ToString());
                    TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"].ToString());
                    TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"].ToString());
                    TemplateHandler.Assign(ref strTmp, "CurrentBalanceMem-val", Common.FormatNumber(dr["CurrBalance"]));
                    TemplateHandler.Assign(ref strTmp, "CreateDate-val", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dr["createddate"]));
                    TemplateHandler.Assign(ref strTmp, "affiliate-val", dr["affusername"]);
                    strBuild.Append(strTmp);
                }

            }
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPages, pageIndex, intPageSize, NRows, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPages, pageIndex, intPageSize, NRows, "aPageBottom", "txtPageBottom", width, false, false));
            if (NRows == 0)
            {
                TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
                strBuild.Append(Common.NoInfo(10));
            }

            //Add search bar
            string strUserName = CastAide.AsString(Request["txtUsername"], string.Empty);
            string strIBCUsername = CastAide.AsString(Request["txtIBCUsername"], string.Empty);

            TemplateHandler.Assign(ref strTpl, "username", string.IsNullOrEmpty(username) ? string.Empty : username);
            TemplateHandler.Assign(ref strTpl, "ibcusername", string.IsNullOrEmpty(strIBCUsername) ? string.Empty : strIBCUsername);

            strUserName = ((string.Compare(strUserName, Resources.Lang.Username, true) == 0 ? string.Empty : strUserName));
            strIBCUsername = ((string.Compare(strIBCUsername, Resources.Lang.Email, true) == 0 ? string.Empty : strIBCUsername));

            TemplateHandler.Assign(ref strTpl, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
            TemplateHandler.Assign(ref strTpl, "affusername-val", affUsername);
            TemplateHandler.Assign(ref strTpl, "page", pageIndex);
            TemplateHandler.Assign(ref strTpl, "pagesize", intPageSize);
            TemplateHandler.Assign(ref strTpl, "nummem-val", NRows);

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            this.HtmlContent = strTpl;
        }
        catch (Exception ex)
        {
            AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
            Response.Write(ex.Message);
        }
    }
}
