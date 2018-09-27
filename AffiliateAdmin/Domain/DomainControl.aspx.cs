using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

public partial class Domain_DomainControl : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Domain/DomainControl.htm");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "AffDomain", Resources.Lang.AffDomain);
        TemplateHandler.Assign(ref strTpl, "RedirectDomain", Resources.Lang.RedirectDomain);
        TemplateHandler.Assign(ref strTpl, "Market", Resources.Lang.Market);
        TemplateHandler.Assign(ref strTpl, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref strTpl, "CreatedDate", Resources.Lang.CreatedDate);
        TemplateHandler.Assign(ref strTpl, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "AddNew", Resources.Lang.AddNew);
        TemplateHandler.Assign(ref strTpl, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref strTpl, "Del", Resources.Lang.Del);

        string market = Common.GetMarket(Request["slMarket"]);

        try
        {
            TemplateHandler.Assign(ref strTpl, "slMarket", AffAdmin.GenerateHTML.Domain.GenMarketList(market, true));
            TemplateHandler.Assign(ref strTpl, "Market-value", market);

            DbResult rs = DataHandler.Domain.DomainControlSel(market);
            StringBuilder strBuild = new StringBuilder();

            string rowTpl = TemplateHandler.CopyBlock(strTpl, "content");
            TemplateHandler.Assign(ref rowTpl, "over", Common.RowBgOver);
            if (rs.ResultSet.Tables[0].Rows.Count > 0)
            {
                int noRow = 0;
                foreach (DataRow dr in rs.ResultSet.Tables[0].Rows)
                {
                    string rowContent = rowTpl;
                    noRow++;
                    TemplateHandler.Assign(ref rowContent, "color", noRow % 2 == 0 ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref rowContent, "no", noRow);
                    TemplateHandler.Assign(ref rowContent, "ID-val", dr["ID"]);
                    TemplateHandler.Assign(ref rowContent, "AffDomainId-val", dr["AffDomainId"]);
                    TemplateHandler.Assign(ref rowContent, "AffDomain-val", dr["AffDomain"]);
                    TemplateHandler.Assign(ref rowContent, "RedirectDomainId-val", dr["RedirectDomainId"]);
                    TemplateHandler.Assign(ref rowContent, "RedirectDomain-val", dr["RedirectDomain"]);
                    TemplateHandler.Assign(ref rowContent, "MarketId-val", dr["MarketId"]);
                    TemplateHandler.Assign(ref rowContent, "Market-val", dr["MarketName"]);
                    TemplateHandler.Assign(ref rowContent, "Creator-val", dr["Creator"]);
                    TemplateHandler.Assign(ref rowContent, "CreatedDate-val", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());
                    TemplateHandler.Assign(ref rowContent, "Status-value", dr["Status"]);
                    TemplateHandler.Assign(ref rowContent, "Status-val", CastAide.AsBoolean(dr["Status"]) ? "ic-check" : "ic-uncheck");

                    strBuild.Append(rowContent);
                }
            }
            else
            {
                strBuild.Append(Common.NoInfo(9));
            }
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
        }
        catch (Exception ex)
        {
            AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
#if DEBUG
            throw;
#endif
        }

        this.HtmlContent = strTpl;
    }
}