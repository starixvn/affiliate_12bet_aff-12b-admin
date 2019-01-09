using System;
using System.Data;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_AddNote : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/AddNote.htm");
        int action = CastAide.AsInt32(Request["action"], 1);
        var affId = string.IsNullOrEmpty(Request["affId"]) ? AppSettings.Session.AffId : Convert.ToInt32(Request["affId"]);

        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "add", Resources.Lang.Add);
        TemplateHandler.Assign(ref strTpl, "update", Resources.Lang.Update);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "content", Resources.Lang.Content);
        TemplateHandler.Assign(ref strTpl, "custid-val", affId);

        TemplateHandler.Assign(ref strTpl, "requireNoteContent", Resources.Lang.ErrNoteEmpty);
        TemplateHandler.Assign(ref strTpl, "addNoteSuccess", Resources.Lang.ErrNoteAdd);
        TemplateHandler.Assign(ref strTpl, "updateNoteSuccess", Resources.Lang.ErrUpdateSucc);

        if (action == 1)//add
        {
            TemplateHandler.RemoveBlock(ref strTpl, "update");
            TemplateHandler.Assign(ref strTpl, "content-val", string.Empty);
        }
        else if (action == 2)//update
        {
            TemplateHandler.RemoveBlock(ref strTpl, "add");
            int noteId = CastAide.AsInt32(Request["noteid"], 0);
            TemplateHandler.Assign(ref strTpl, "noteid-val", noteId);
            if (noteId == 0)//noteid is not correct
                return;
            //Load note content
            DbResult rs = DataHandler.Affiliate.SelNoteDetail(affId, noteId);
            DataRow dr = rs.ResultSet.Tables[0].Rows[0];
            TemplateHandler.Assign(ref strTpl, "content-val", dr["note"]);
        }
        this.HtmlContent = strTpl;
    }
}
