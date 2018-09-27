using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;
public partial class AffiliateDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("Domain/AffiliateDomain.html");
        TemplateHandler.Assign(ref list, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref list, "Domain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "SSL", Resources.Lang.SSL);
        TemplateHandler.Assign(ref list, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref list, "CreatedDate", Resources.Lang.CreatedDate);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref list, "Del", Resources.Lang.Del);
        TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);

        DbResult dbr = null;
        dbr = DataHandler.Domain.GetListAffDomain();
        string _content = TemplateHandler.CopyBlock(list, "content");
        TemplateHandler.Assign(ref _content, "over", Common.RowBgOver);
        string _listcontent = "";
        StringBuilder strcontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
            {
                _listcontent = _content;
                counter++;
                TemplateHandler.Assign(ref _listcontent, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref _listcontent, "No_Value", counter.ToString());
                TemplateHandler.Assign(ref _listcontent, "AffDomain_Value", dr["DomainName"]);
                TemplateHandler.Assign(ref _listcontent, "SSL-class", CastAide.AsBoolean(dr["IsSSL"]) ? "ic-check" : "ic-uncheck");
                TemplateHandler.Assign(ref _listcontent, "SSL-Value", CastAide.AsBoolean(dr["IsSSL"]));
                TemplateHandler.Assign(ref _listcontent, "Creator_Value", dr["Creator"]);
                TemplateHandler.Assign(ref _listcontent, "CreatedDate_Value", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());
                TemplateHandler.Assign(ref _listcontent, "Edit", Resources.Lang.Edit);
                TemplateHandler.Assign(ref _listcontent, "Del", Resources.Lang.Del);
                TemplateHandler.Assign(ref _listcontent, "affdomainid", dr["DomainID"]);
                strcontent.Append(_listcontent);
            }
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref _listcontent, "content", Common.GenNoInformation("7"));
        }

        TemplateHandler.ReplaceBlock(ref list, "content", strcontent.ToString());
        TemplateHandler.Assign(ref list, "ChooseConfirm", Resources.Lang.ConfirmDelAffDomain);
        TemplateHandler.Assign(ref list, "AddAffDomain-val", Resources.Lang.AddAffDomain);

        this.HtmlContent = list;
    }
}
