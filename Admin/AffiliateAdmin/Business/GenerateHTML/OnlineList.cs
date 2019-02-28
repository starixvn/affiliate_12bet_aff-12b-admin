using System;
using System.Data;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Text;

/// <summary>
/// Summary description for OnlineList
/// </summary>
public class OnlineList
{
    public static string GenerateOnlineListTable(int page, int pagesize)
    {
        string content = TemplateHandler.LoadTemplate("Home.htm");
        string table = TemplateHandler.CopyBlock(content, "tblOnline");
        TemplateHandler.Assign(ref table, "Username", Resources.Lang.Username);
        TemplateHandler.Assign(ref table, "LoggedIn", Resources.Lang.LoggedIn);
        TemplateHandler.Assign(ref table, "LastRequest", Resources.Lang.LastRequest);
        TemplateHandler.Assign(ref table, "Role", Resources.Lang.Role);
        TemplateHandler.Assign(ref table, "IP", Resources.Lang.IP);
        //DbResult ret = DataHandler.Home.GetOnlineList();
        int Nrows = 0, totalpages = 0;
        int NrowOnCurPage = 0;
        DbResult olRs;
        DataTable oldtb = new DataTable();

        while (NrowOnCurPage == 0)
        {
            olRs = null;
            olRs = DataHandler.Home.GetOnlineList(pagesize, page);
            totalpages = CastAide.AsInt32(olRs.Outputs["totalpages"]);
            Nrows = CastAide.AsInt32(olRs.Outputs["NRows"]);
            oldtb = olRs.ResultSet.Tables[0];
            if (page == 1)//Break out if page is equal 1
                NrowOnCurPage = -1;
            else
                NrowOnCurPage = oldtb.Rows.Count;
            if (NrowOnCurPage == 0)
            {
                page = totalpages;
            }
        }
        string olRows = string.Empty;
        string olRow = TemplateHandler.CopyBlock(table, "OLRow");
        int No = 0;
        foreach (DataRow r in oldtb.Rows)
        {
            string temp = olRow;
            TemplateHandler.Assign(ref temp, "No-val", ++No + (page - 1) * pagesize);
            TemplateHandler.Assign(ref temp, "logged-in-val", Common.IBCDateTimeFormat(r["LoggedIn"], DTFormatType.DT24h));
            TemplateHandler.Assign(ref temp, "lastrequest-val", Common.IBCDateTimeFormat(r["LastRequest"], DTFormatType.DT24h));
            TemplateHandler.Assign(ref temp, "IP-val", r["IP"].ToString());
            TemplateHandler.Assign(ref temp, "ipchecker-url", Common.GetIPCheckerURL(r["IP"].ToString()));
            if (r["RoleName"].ToString() == "Admin")
            {
                TemplateHandler.Assign(ref temp, "Username-val", r["username"].ToString());
                TemplateHandler.Assign(ref temp, "Type-val", Resources.Lang.Administrator);
            }
            else
            {
                TemplateHandler.Assign(ref temp, "Username-val", "<a href=\"../Customer/AffDefault.aspx?user=" + r["username"].ToString() + "\"> " + r["username"].ToString() + "</a>");
                TemplateHandler.Assign(ref temp, "Type-val", Resources.Lang.Affiliate);
            }
            if ((No % 2) == 0)
                TemplateHandler.Assign(ref temp, "color", Common.RowBgEven);
            else
                TemplateHandler.Assign(ref temp, "color", Common.RowBgOdd);
            TemplateHandler.Assign(ref temp, "over", Common.RowBgOver);
            TemplateHandler.Assign(ref temp, "themepath", AppSettings.ThemeDir);
            olRows += temp;
        }
        TemplateHandler.ReplaceBlock(ref table, "OLRow", olRows);
        TemplateHandler.Assign(ref table, "paging-top", Common.GenPaging(totalpages, page, pagesize, Nrows, "aPageTop", "txtPageTop", 950, false, true));
        TemplateHandler.Assign(ref table, "paging-bottom", Common.GenPaging(totalpages, page, pagesize, Nrows, "aPageBottom", "txtPageBottom", 950, false, false));
        return table;
    }
}
