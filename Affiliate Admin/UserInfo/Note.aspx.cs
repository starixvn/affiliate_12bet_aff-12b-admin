using System;
using DNAide.Web;
using DNAide.Core;

public partial class UserInfo_Note : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int intCustId = AppSettings.Session.AffId;
        
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/NoteList.htm");
        if (string.Compare(Request["ischild"], "1", true) == 0)
            TemplateHandler.ReplaceBlock(ref strTpl, "accoverview", string.Empty);

        #region Load language
        TemplateHandler.Assign(ref strTpl, "note", Resources.Lang.Note);
        TemplateHandler.Assign(ref strTpl, "accoverview", Resources.Lang.AccOverview);
        TemplateHandler.Assign(ref strTpl, "earning", Resources.Lang.EarningsHistory);
        TemplateHandler.Assign(ref strTpl, "upd", Resources.Lang.Update);
        TemplateHandler.Assign(ref strTpl, "addnote", Resources.Lang.Add);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "Delete", Resources.Lang.Delete);
        #endregion

        TemplateHandler.Assign(ref strTpl, "ischild", Request["ischild"]);
        TemplateHandler.Assign(ref strTpl, "custid", intCustId.ToString());
        int pageIndex = CastAide.AsInt32(Request["page"], 1);
        TemplateHandler.Assign(ref strTpl, "page-val", pageIndex);
        bool hasData = false;
        int rows = 0;
        TemplateHandler.ReplaceBlock(ref strTpl, "notelist", GenerateHTML.Note.GenNoteList(intCustId, pageIndex, CastAide.AsInt32(Request["slPageSize"], 20), ref hasData, ref rows));
        if(!hasData)
            TemplateHandler.RemoveBlock(ref strTpl, "nocontent");
        string strUrlParam = "?approved=" + Request["approved"];
        TemplateHandler.Assign(ref strTpl, "approved-val", Request["approved"]);
        
        #region Load SubPage
        string subPage = string.Empty;
        if (string.Compare(Request["approved"], "0", true) == 0)
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffPendingLink, SubMenu.AffPendingText, 1, rows, Resources.Lang.Note, "i_customer.png"));
        else
            TemplateHandler.Assign(ref strTpl, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 2, rows, Resources.Lang.Note, "i_customer.png"));

        #endregion

        this.HtmlContent = strTpl;    

    }
}
