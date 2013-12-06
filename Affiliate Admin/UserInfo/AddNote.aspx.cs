using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DNAide.Web;
using DNAide.Core;
using DNAide.Data;

public partial class UserInfo_AddNote : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("UserInfo/AddNote.htm");
        int action = CastAide.AsInt32(Request["action"], 1);
        TemplateHandler.Assign(ref strTpl, "add", Resources.Lang.Add);
        TemplateHandler.Assign(ref strTpl, "update", Resources.Lang.Update);
        TemplateHandler.Assign(ref strTpl, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref strTpl, "content", Resources.Lang.Content);
        TemplateHandler.Assign(ref strTpl, "custid-val", AppSettings.Session.AffId);
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
            DbResult rs = DataHandler.Affiliate.SelNoteDetail(AppSettings.Session.AffId, noteId);
            DataRow dr = rs.ResultSet.Tables[0].Rows[0];
            TemplateHandler.Assign(ref strTpl, "content-val", dr["note"]);
        }
        this.HtmlContent = strTpl;
    }
}
