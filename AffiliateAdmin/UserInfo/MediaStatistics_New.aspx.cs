using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

public partial class Report_MediaStatistics_New : IBCPage
{
    private string strTpl = string.Empty;
    private StringBuilder strListRow = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_New.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    private void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    private void GenerateHTML()
    {
        DateTime now = DateTime.Now;
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], new DateTime(now.Year, now.Month, 1));
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        if (dtmTDate > DateTime.Now)
        {
            dtmTDate = DateTime.Now;
        }

        var deviceType = Common.GetDeviceType(Request["slDevice"]);

        TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

        TemplateHandler.Assign(ref strTpl, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref strTpl, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

        DbResult rs = DataHandler.TrackingSystem.SelClickStatisticsBySite(dtmFDate, dtmTDate, deviceType);
        int totalSignups = CastAide.AsInt32(rs.Outputs["totalsignups"]);
        int totalDeposits = CastAide.AsInt32(rs.Outputs["totalfdms"]);

        DataTable dtb = new DataTable();
        if (rs.ResultSet.Tables.Count > 0)
            dtb = rs.ResultSet.Tables[0];

        if (dtb.Rows.Count > 0)
        {
            int intCount = 0;
            string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            string strListAffId = string.Empty;
            var strListRow = new StringBuilder();

            foreach (DataRow dr in dtb.Rows)
            {
                string strTmp = strRowTpl;

                intCount++;
                TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                var domain = dr["DomainReferrer"].ToString();
                if (domain.Equals("none", StringComparison.OrdinalIgnoreCase) ||
                    domain.Equals("old url", StringComparison.OrdinalIgnoreCase))
                {
                    TemplateHandler.ReplaceBlock(ref strTmp, "page", domain);
                }
                else
                {
                    TemplateHandler.Assign(ref strTmp, "Site-val", domain);
                }

                TemplateHandler.Assign(ref strTmp, "TotalClick-val", CastAide.AsInt32(dr["totalclick"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "UniqueClick-val", CastAide.AsInt32(dr["uniqueclick"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "Signups-val", CastAide.AsInt32(dr["Signups"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "FDM-val", CastAide.AsInt32(dr["FDM"]).ToString(Common.NumberFormatPlaceholder));
                TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                strListRow.Append(strTmp);
            }

            TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(4));
        }
        //Assign Paging template
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 7, 0, SubMenu.AffApprovedText[7], "i_report.png"));
    }
}