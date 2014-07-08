using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Text;
public partial class _12BetDomain : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("Domain/12BetDomain.html");

        TemplateHandler.Assign(ref list, "Domain", Resources.Lang.Domain);
        TemplateHandler.Assign(ref list, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref list, "CreatedDate", Resources.Lang.CreatedDate);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref list, "Del", Resources.Lang.Del);
        TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref list, "Device", Resources.Lang.Device);
        TemplateHandler.Assign(ref list, "Submit", Resources.Lang.Submit);

        string deviceType = Request["slDevice"];
        try
        {
            TemplateHandler.Assign(ref list, "slDevice", AffAdmin.GenerateHTML.Domain.GenDeviceList(deviceType));

            DbResult dbr = null;
            dbr = DataHandler.Domain.GetList12BetDomain(deviceType);
            string _content = TemplateHandler.CopyBlock(list, "content");
            TemplateHandler.Assign(ref _content, "over", Common.RowBgOver);
            string _listcontent = "";
            var strcontent = new StringBuilder();
            int counter = 0;
            if (dbr.ResultSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
                {
                    _listcontent = _content;
                    counter++;
                    TemplateHandler.Assign(ref _listcontent, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref _listcontent, "No_Value", counter.ToString());
                    TemplateHandler.Assign(ref _listcontent, "12BetDomain_Value", dr["DomainName"]);
                    TemplateHandler.Assign(ref _listcontent, "Creator_Value", dr["Creator"]);
                    TemplateHandler.Assign(ref _listcontent, "CreatedDate_Value", CastAide.AsDateTime(dr["CreateDate"].ToString()).ToShortDateString());
                    TemplateHandler.Assign(ref _listcontent, "Device_Value", dr["DeviceType"].ToString());
                    TemplateHandler.Assign(ref _listcontent, "Edit", Resources.Lang.Edit);
                    TemplateHandler.Assign(ref _listcontent, "Del", Resources.Lang.Del);
                    TemplateHandler.Assign(ref _listcontent, "12betdomainid", dr["DomainID"]);
                    TemplateHandler.Assign(ref _listcontent, "DeviceType_Value", deviceType);
                    strcontent.Append(_listcontent);
                }
            }
            else
            {
                _listcontent = Common.GenNoInformation("7");
                strcontent.Append(_listcontent);
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
