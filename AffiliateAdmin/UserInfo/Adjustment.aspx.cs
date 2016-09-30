using System;
using DNAide.Web;
using DNAide.Core;
using DNAide.Utils;
using AffiliateAdmin.Business.DataAccess;

public partial class UserInfo_Adjustment : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/Adjustment.htm");
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "balforward", Resources.Lang.BalanceForward);
        TemplateHandler.Assign(ref strTpl, "adjust", Resources.Lang.Amount);
        TemplateHandler.Assign(ref strTpl, "descript", Resources.Lang.Description);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "adjustment", Resources.Lang.Adjustment);

        string strContent = TemplateHandler.CopyBlock(strTpl, "content");
        try
        {
            int custid = AppSettings.Session.AffId;
            TemplateHandler.Assign(ref strTpl, "affiliate-val", AppSettings.Session.AffUsername);
            TemplateHandler.Assign(ref strTpl, "custid", custid);

            DbResult rs = DataHandler.Affiliate.SelBalanceBF(custid);

            decimal dcBF = CastAide.AsDecimal(rs.Outputs["balbroughtforward"]);
            TemplateHandler.Assign(ref strTpl, "balforward-val", Common.FormatNumber(dcBF));
            TemplateHandler.Assign(ref strTpl, "bf-val", NumberFormat.DecFormat(dcBF, 2));
            #region Load SubPage
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 4, 0, Resources.Lang.Adjustment, "i_customer.png"));
            #endregion

            this.HtmlContent = strTpl;

        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
            Response.Write(ex.Message);
        }
    }
}
