using System.Xml.Linq;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using DNAide.Utils;
using System.Text;
using System;
using System.Data;
using System.Linq;
using GenerateHTML;

public partial class Report_MediaStatistics_New : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        strTpl = TemplateHandler.LoadTemplate("UserInfo/MediaStatistics_New.htm");

        AssignHeader();
        GenerateHTML();

        this.HtmlContent = strTpl;
    }

    void AssignHeader()
    {
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "over", Common.RowBgOver);
    }

    void GenerateHTML()
    {

        try
        {
            DateTime now = DateTime.Now;
            DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], new DateTime(now.Year, now.Month, 1));
            DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
            var deviceType = Common.GetDeviceType(Request["slDevice"]);

            TemplateHandler.Assign(ref strTpl, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
            TemplateHandler.Assign(ref strTpl, "tdate", dtmTDate.ToString("MM/dd/yyyy"));

            
            TemplateHandler.Assign(ref strTpl, "Device", Resources.Lang.Device);
            TemplateHandler.Assign(ref strTpl, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));

            DbResult rs = DataHandler.TrackingSystem.SelClickStatisticsBySite(dtmFDate, dtmTDate, deviceType);

            DataTable dtb = new DataTable();
            if (rs.ResultSet.Tables.Count > 0)
                dtb = rs.ResultSet.Tables[0];


            if (dtb.Rows.Count > 0)
            {

                int intCount = 0;
                string strRowTpl = TemplateHandler.CopyBlock(strTpl, "content");
                string strListAffId = string.Empty;

                StringBuilder strListRow = new StringBuilder();
                int totalSignups = 0, totalDeposits = 0;
                //Hashtable hsTotal = new Hashtable();
                foreach (DataRow dr in dtb.Rows)
                {
                    string strTmp = strRowTpl;

                    intCount++;
                    totalSignups += CastAide.AsInt32(dr["Signups"]);
                    totalDeposits += CastAide.AsInt32(dr["FDM"]);
                    TemplateHandler.Assign(ref strTmp, "color", (intCount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    if (string.Compare(dr["DomainReferrer"].ToString(), "none", true) == 0)
                        TemplateHandler.ReplaceBlock(ref strTmp, "page", dr["DomainReferrer"].ToString());

                    TemplateHandler.Assign(ref strTmp, "Site-val", dr["DomainReferrer"]);
                    TemplateHandler.Assign(ref strTmp, "TotalClick-val", dr["totalclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "UniqueClick-val", dr["uniqueclick"].ToString());
                    TemplateHandler.Assign(ref strTmp, "Signups-val", dr["Signups"].ToString());
                    TemplateHandler.Assign(ref strTmp, "FDM-val", dr["FDM"].ToString());
                    TemplateHandler.Assign(ref strTmp, "DeviceType-val", deviceType);

                    strListRow.Append(strTmp);
                }

                TemplateHandler.Assign(ref strRowTpl, "color", (intCount % 2 == 0) ? Common.RowBgOdd : Common.RowBgEven);
                //Assign total signups for Old URL
                strListRow.Append(TrackingSystem.GenRowOldURL(strRowTpl, totalSignups, totalDeposits, dtmFDate, dtmTDate));

                TemplateHandler.ReplaceBlock(ref strTpl, "content", strListRow.ToString());
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "nodata", Common.NoInfo(4));
            }
            //Assign Paging template
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 8, 0, SubMenu.AffApprovedText[7], "i_report.png"));            
        }
        catch (Exception ex)
        {
            DNAideLog.Write(ex.Message);
#if DEBUG
                throw;
#endif
        }
    }
}