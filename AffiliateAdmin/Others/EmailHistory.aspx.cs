using System;
using AffiliateAdmin.Portal;
using DNAide.Core;
using DNAide.Web;

public partial class Others_EmailHistory : BasePage
{
    protected string _htmlContent = string.Empty;
    protected string _htmlSubmenuContent = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("Others/EmailHistory.htm");
        TemplateHandler.Assign(ref content, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref content, "EmailHistory", Resources.Lang.EmailHistory);
        TemplateHandler.Assign(ref content, "Search", Resources.Lang.SearchBy);
        TemplateHandler.Assign(ref content, "All", Resources.Lang.All);
        TemplateHandler.Assign(ref content, "Sender", Resources.Lang.Sender);
        TemplateHandler.Assign(ref content, "Email", Resources.Lang.Email);
        TemplateHandler.Assign(ref content, "Affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref content, "UnknownAffiliate", Resources.Lang.UnknownAffiliate);
        TemplateHandler.Assign(ref content, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref content, "To", Resources.Lang.To);
        TemplateHandler.Assign(ref content, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref content, "ServerDateTime", DateTime.Now.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref content, "Delete", Resources.Lang.Delete);
        TemplateHandler.Assign(ref content, "ID", Resources.Lang.ID);
        TemplateHandler.Assign(ref content, "View", Resources.Lang.View);
        TemplateHandler.Assign(ref content, "Subject", Resources.Lang.Subject);
        TemplateHandler.Assign(ref content, "SentDate", Resources.Lang.SentDate);
        TemplateHandler.Assign(ref content, "IP", Resources.Lang.IP);
        TemplateHandler.Assign(ref content, "Browser", Resources.Lang.Browser);
        TemplateHandler.Assign(ref content, "EmailDetail", Resources.Lang.EmailDetail);
        TemplateHandler.Assign(ref content, "Sent", Resources.Lang.Sent);
        TemplateHandler.Assign(ref content, "Content", Resources.Lang.Content);
        TemplateHandler.Assign(ref content, "ConfirmDeleteEmail", Resources.Lang.ConfirmDeleteEmail);
        TemplateHandler.Assign(ref content, "NoEmailSelected", Resources.Lang.NoEmailSelected);
        TemplateHandler.Assign(ref content, "Cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref content, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

        string name = string.Empty;
        string email = string.Empty;
        string affiliateName = string.Empty;
        int minNo = 0;
        int maxNo = 0;
        int totalEmail = 0;
        int totalpage = 0;
        int currPage = CastAide.AsInt32(Request["page"], 1);//get the current page from hidden field in form
        int currPageSize = CastAide.AsInt32(Request["pagesize"], 20);//get the current page from hidden field in form
        int searchOption = CastAide.AsInt32(Request["slSearchOption"], -1);
        object msgId = null;
        string strSearchText = CastAide.AsString(Request["txtSearchText"], string.Empty);

        switch (searchOption)
        {
            case -1:
                break;

            case 0:
                name = strSearchText;
                break;

            case 1:
                email = strSearchText;
                break;

            case 2:
                affiliateName = strSearchText;
                break;

            case -2:
                msgId = -1;
                break;
        }
        DateTime fdate = DateTime.Now;
        DateTime tdate = DateTime.Now;
        string hasDate = CastAide.AsString(Request["hdchkDate"], string.Empty);

        if (hasDate == "on")
        {
            fdate = CastAide.AsDateTime(Request["fdate"], null);
            tdate = CastAide.AsDateTime(Request["tdate"], null);
            TemplateHandler.ReplaceBlock(ref content, "Content", GenerateHTML.EmailHistory.GetEmailHistoryList(msgId, name, email, affiliateName, fdate, tdate, currPage, ref minNo, ref maxNo, ref totalEmail, currPageSize, ref totalpage));
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref content, "Content", GenerateHTML.EmailHistory.GetEmailHistoryList(msgId, name, email, affiliateName, null, null, currPage, ref minNo, ref maxNo, ref totalEmail, currPageSize, ref totalpage));
        }

        TemplateHandler.Assign(ref content, "hdSearchOption-val", searchOption.ToString());
        TemplateHandler.Assign(ref content, "hdSearchText-val", strSearchText);
        TemplateHandler.Assign(ref content, "hdchkDate-val", hasDate);
        TemplateHandler.Assign(ref content, "hdfDate-val", fdate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref content, "hdtDate-val", tdate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref content, "hdTotalEmail-val", totalEmail.ToString());

        TemplateHandler.Assign(ref content, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref content, "maxNo", maxNo.ToString());
        TemplateHandler.Assign(ref content, "page", currPage);
        if (totalEmail != 0)
        {
            TemplateHandler.Assign(ref content, "paging-top", Common.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageTop", "txtPageTop", new string[] { Resources.Lang.Delete }, new string[] { "DeleteSelectedMessage();" }, 1020, true, true, true, false));
            TemplateHandler.Assign(ref content, "paging-bottom", Common.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageBottom", "txtPageBottom", 1020, true, false));
        }
        else
        {
            TemplateHandler.Assign(ref content, "paging-top", "");
            TemplateHandler.Assign(ref content, "paging-bottom", "");
        }

        _htmlSubmenuContent = SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 7, totalEmail, Resources.Lang.EmailHistory, "i_email_history.png");
        _htmlContent = content;
    }
}