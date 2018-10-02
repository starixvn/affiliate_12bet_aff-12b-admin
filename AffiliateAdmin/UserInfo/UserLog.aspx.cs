using System;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_UserLog : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/UserLog.htm");
        int affID = AppSettings.Session.AffId;
        int logType = CastAide.AsInt32(Request["sllogtype"], 0);
        int pageIndex = CastAide.AsInt32(Request["page"], 1);
        DateTime dateFrom = CastAide.AsDateTime(Request["fdate"], DateTime.Now.AddMonths(-6));
        DateTime dateTo = CastAide.AsDateTime(Request["tdate"], DateTime.Now);

        int pageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        int pageCount = 0, rowCount = 0;

        TemplateHandler.Assign(ref strTpl, "type", Resources.Lang.type);
        TemplateHandler.Assign(ref strTpl, "sllogtype", GenerateHTML.UserLog.GetLogTypeList(logType, true));
        TemplateHandler.Assign(ref strTpl, "fdate", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "tdate", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTpl, "fdate-val", dateFrom.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "tdate-val", dateTo.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTpl, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref strTpl, "UpdateBy", Resources.Lang.UpdatedBy);

        TemplateHandler.Assign(ref strTpl, "id", Resources.Lang.ID);
        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "action", Resources.Lang.Action);
        TemplateHandler.Assign(ref strTpl, "description", Resources.Lang.Description);
        TemplateHandler.Assign(ref strTpl, "ip", Resources.Lang.IP);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        // Content
        TemplateHandler.ReplaceBlock(ref strTpl, "content", GenerateHTML.UserLog.GetLogList(affID, logType, dateFrom, dateTo, pageIndex, pageSize, ref pageCount, ref rowCount));

        // Paging        
        String pagingTop = "", pagingBottom = "";
        if (pageCount > 0)
        {
            pagingTop = Common.GenPaging(pageCount, pageIndex, pageSize, rowCount, "aPageTop", "txtPageTop", "{100%}", false, true);
            pagingBottom = Common.GenPaging(pageCount, pageIndex, pageSize, rowCount, "aPageBottom", "txtPageBottom", "{100%}", false, false);
            pagingTop = pagingTop.Replace("{100%}px", "100%");
            pagingBottom = pagingBottom.Replace("{100%}px", "100%");
        }

        TemplateHandler.Assign(ref strTpl, "paging-top", pagingTop);
        TemplateHandler.Assign(ref strTpl, "paging-bottom", pagingBottom);

        TemplateHandler.Assign(ref strTpl, "page-val}", pageIndex);

        #region Load SubPage
        TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 7, 0, SubMenu.AffApprovedText[7], "i_customer.png"));
        #endregion

        this.HtmlContent = strTpl;

    }
}
