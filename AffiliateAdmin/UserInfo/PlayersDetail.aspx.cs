using System;
using System.Text;
using System.Data;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DataHandler;

public partial class PlayersDetail : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/PlayersDetail.htm");
        #region Load language
        TemplateHandler.Assign(ref strTpl, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "ibcusername", Resources.Lang.IBCUsername);
        TemplateHandler.Assign(ref strTpl, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref strTpl, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref strTpl, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "playersdetail", Resources.Lang.PlayersDetail);
        TemplateHandler.Assign(ref strTpl, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref strTpl, "adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref strTpl, "psp", Resources.Lang.PSP);
        #endregion        

        //Get data
        int intCustId = AppSettings.Session.AffId;
        int sporttype = CastAide.AsInt32(Request["slSportType"], 0);
        int width = 800;
        if (sporttype == 2)
        {
            TemplateHandler.RemoveBlock(ref strTpl, "sportbook");
            width = 650;
        }
        string strMonth = Request["month"];
        TemplateHandler.Assign(ref strTpl, "month-val", strMonth);
        TemplateHandler.Assign(ref strTpl, "slSports", Common.GenListSportType(sporttype));

        int pageIndex = 1, totalPages = 0, pageSize = 0;
        pageIndex = CastAide.AsInt32(Request["page"], 1);
        pageSize = CastAide.AsInt32(Request["pagesize"], 200);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        TemplateHandler.Assign(ref strTpl, "pagesize", pageSize);
        TemplateHandler.Assign(ref strTpl, "width", width);

        DbResult result = DataHandler.Affiliate.SelPlayersDetail(intCustId, sporttype, strMonth, pageIndex, pageSize, ref totalPages);
        
        StringBuilder strBuild = new StringBuilder();
        if (result.ResultSet.Tables.Count>0 && result.ResultSet.Tables[0].Rows.Count > 0)
        {
	    DataTable dtb = result.ResultSet.Tables[0];	
            //Assign row content
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //  decimal dcTurnover = 0, dcWinlost = 0, dcDeposit = 0;
            int intcount = (pageIndex - 1) * pageSize;// hardcode pagesize for member/player list
            TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
            foreach (DataRow dr in dtb.Rows)
            {
                intcount++;
                string strTmp = strContent;
                TemplateHandler.Assign(ref strTmp, "color", (intcount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "no-val", intcount);
                //make sure remove 12bet$ in the first position of strUsername2
                string ibcUsername = dr["ibcusername"].ToString();
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "ibcusername-val", ibcUsername);
                TemplateHandler.Assign(ref strTmp, "registerdate-val", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dr["RegisteredDate"]));
                TemplateHandler.Assign(ref strTmp, "turnover-val", Common.FormatNumber(dr["turnover"]));
                TemplateHandler.Assign(ref strTmp, "winlost-val", Common.FormatNumber(dr["winlost"]));
                TemplateHandler.Assign(ref strTmp, "deposit-val", Common.FormatNumber(dr["deposit"]));
                TemplateHandler.Assign(ref strTmp, "bonus-val", Common.FormatNumber(dr["bonus"]));
                TemplateHandler.Assign(ref strTmp, "adjustment-val", Common.FormatNumber(dr["adjustment"]));
                TemplateHandler.Assign(ref strTmp, "psp-val", Common.FormatNumber(dr["psp"]));
                strBuild.Append(strTmp);
            }
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageTop", "txtPageTop", width, false, true));
            //TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageTop", "txtPageTop", new string[] { "[ALL]", "Sportsbook", "Casino" }, new string[] { "all", "sportsbook", "casino" }, 662, false, true, true, false));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPages, pageIndex, pageSize, dtb.Rows.Count, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTpl, "pagingcss", "hide");
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
            strBuild.Append(Common.NoInfo(6));
        }

        TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());       
       
        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 3, string.IsNullOrEmpty(Request["month"]) ? Resources.Lang.All : CastAide.AsDateTime(Request["month"]).ToString("MMMM yyyy"), Resources.Lang.PlayersDetail, "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;
    }
}
