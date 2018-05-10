using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;
public partial class _12BetDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("Domain/12BetDomain.html");
        TemplateHandler.Assign(ref list, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref list, "Domain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref list, "CreatedDate", Resources.Lang.CreatedDate);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref list, "Del", Resources.Lang.Del);
        TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref list, "SSL", Resources.Lang.SSL);
        TemplateHandler.Assign(ref list, "Submit", Resources.Lang.Submit);

        try
        {
            DbResult dbr = DataHandler.Domain.GetList12BetDomain();
            string _content = TemplateHandler.CopyBlock(list, "content");
            TemplateHandler.Assign(ref _content, "over", Common.RowBgOver);
            var strcontent = new StringBuilder();
            int counter = 0;
            if (dbr.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
                {
                    var rowItem = _content;
                    counter++;
                    TemplateHandler.Assign(ref rowItem, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref rowItem, "No_Value", counter.ToString());
                    TemplateHandler.Assign(ref rowItem, "12BetDomain_Value", dr["DomainName"]);
                    TemplateHandler.Assign(ref rowItem, "Creator_Value", dr["Creator"]);
                    TemplateHandler.Assign(ref rowItem, "CreatedDate_Value", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());
                    TemplateHandler.Assign(ref rowItem, "Edit", Resources.Lang.Edit);
                    TemplateHandler.Assign(ref rowItem, "Del", Resources.Lang.Del);
                    TemplateHandler.Assign(ref rowItem, "12betdomainid", dr["DomainID"]);
                    TemplateHandler.Assign(ref rowItem, "SSL-val", CastAide.AsBoolean(dr["IsSSL"]) ? "ic-check" : "ic-uncheck");
                    TemplateHandler.Assign(ref rowItem, "SSL-Value", CastAide.AsBoolean(dr["IsSSL"]));
                    strcontent.Append(rowItem);
                }
            }
            else
            {
                var noInfoContent = Common.GenNoInformation("7");
                strcontent.Append(noInfoContent);
            }

            TemplateHandler.ReplaceBlock(ref list, "content", strcontent.ToString());
            TemplateHandler.Assign(ref list, "submenu", SubMenu.GenSubMenu(SubMenu.DomainLink,
                                                                            SubMenu.DomainText,
                                                                            1, counter, Resources.Lang.Administrator,
                                                                            "i_domain.jpg"));
            TemplateHandler.Assign(ref list, "ChooseConfirm", Resources.Lang.ConfirmDel12BetDomain);
            TemplateHandler.Assign(ref list, "Add12BetDomain-val", Resources.Lang.Add12BetDomain);


            this.HtmlContent = list;
        }
        catch (Exception)
        {


        }
    }
}
