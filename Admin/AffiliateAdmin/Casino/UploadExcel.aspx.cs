using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;

public partial class Casino_UploadExcel : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime date = CastAide.AsDateTime(Request["date"], DateTime.Now);
        string temp = TemplateHandler.LoadTemplate("Casino/UploadExcel.htm");
        TemplateHandler.Assign(ref temp, "File", Resources.Lang.ExcelTranslationTitle);
        TemplateHandler.Assign(ref temp, "ErrFileExtension", Resources.Lang.ErrExcelFileExtension);
        TemplateHandler.Assign(ref temp, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref temp, "fdate", date.ToString("MM/dd/yyyy"));


        TemplateHandler.Assign(ref temp, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref temp, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref temp, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref temp, "turnover", Resources.Lang.Turnover);
        TemplateHandler.Assign(ref temp, "winlost", Resources.Lang.Winlost);
        TemplateHandler.Assign(ref temp, "deposit", Resources.Lang.Deposit);
        TemplateHandler.Assign(ref temp, "bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref temp, "listtranstext", Resources.Lang.ListTransText + " " + date.ToString("dd-MMMM-yyyy"));
        TemplateHandler.Assign(ref temp, "hasdata", "1");
        TemplateHandler.Assign(ref temp, "cssmsg", "tbl_htext");

        //Check whether Data Exists or not
        //bool hasData = DataHandler.Casino.CasinoHasData(date);
        //Show list Old Transaction if Data imported already
        DbResult rs = DataHandler.Casino.CasinoSel(date);
        //if (hasData && rs.ResultSet.Tables.Count > 0)
        if (rs.ResultSet.Tables[0].Rows.Count > 0)
        {
            //Show Message has data
            StringBuilder strBuild = new StringBuilder();
            string rowTpl = TemplateHandler.CopyBlock(temp, "content");
            
            //Generate list trans
            DataTable dtb = rs.ResultSet.Tables[0];
            int i = 0;
            foreach (DataRow dr in dtb.Rows)
            {
                string rowHTML = rowTpl;
                i++;
                TemplateHandler.Assign(ref rowHTML, "no", i);
                TemplateHandler.Assign(ref rowHTML, "color", (i % 2 == 0 ? Common.RowBgEven : Common.RowBgOdd));
                TemplateHandler.Assign(ref rowHTML, "date-val", CastAide.AsDateTime(dr["DateImport"]).ToString("MM/dd/yyyy"));
                TemplateHandler.Assign(ref rowHTML, "username-val", dr["username"].ToString().Replace("12bet$", string.Empty));
                TemplateHandler.Assign(ref rowHTML, "currency-val", dr["currency"]);
                TemplateHandler.Assign(ref rowHTML, "turnover-val", Common.FormatNumber(dr["turnover"]));
                TemplateHandler.Assign(ref rowHTML, "winlost-val", Common.FormatNumber(dr["winlost"]));
                TemplateHandler.Assign(ref rowHTML, "deposit-val", Common.FormatNumber(dr["deposit"]));
                TemplateHandler.Assign(ref rowHTML, "bonus-val", Common.FormatNumber(dr["bonus"]));
                strBuild.Append(rowHTML);
            }
            TemplateHandler.ReplaceBlock(ref temp, "content", strBuild.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref temp, "content", Common.NoInfo(8));
            //TemplateHandler.Assign(ref temp, "hasdata", "0");
            //if (string.IsNullOrEmpty(Request["date"]))
            //    TemplateHandler.RemoveBlock(ref temp, "hasdata");
            //else
            //    TemplateHandler.ReplaceBlock(ref temp, "hasdata", "<b class='tbl_htext'>" + Resources.Lang.NoInfo + " in " + date.ToString("dd MMM yyyy") + "</b>");
        }

        this.HtmlContent = temp;
    }
}
