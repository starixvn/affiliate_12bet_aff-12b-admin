using System;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

public partial class UserInfo_Adjustment : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/Adjustment.htm");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "balforward", Resources.Lang.BalanceForward);
        TemplateHandler.Assign(ref strTpl, "adjust", Resources.Lang.Amount);
        TemplateHandler.Assign(ref strTpl, "descript", Resources.Lang.Description);
        TemplateHandler.Assign(ref strTpl, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "adjustment", Resources.Lang.Adjustment);

        string strContent = TemplateHandler.CopyBlock(strTpl, "content");
        try
        {
            var affId = string.IsNullOrEmpty(Request["affId"]) ? AppSettings.Session.AffId : Convert.ToInt32(Request["affId"]);
            var affName = string.IsNullOrEmpty(Request["affName"]) ? AppSettings.Session.AffUsername : Request["affName"];

            TemplateHandler.Assign(ref strTpl, "affName-val", affName);
            TemplateHandler.Assign(ref strTpl, "affId-val", affId);

            DbResult rs = DataHandler.Affiliate.SelBalanceBF(affId);

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
            AffiliateAdmin.Business.Helpers.LogHelper.WriteError(ex);
            Response.Write(ex.Message);
        }
    }
}
