using System;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Data;

public partial class ViewMessDetails : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("Others/ViewMess.htm");

        int messId = CastAide.AsInt32(Request["id"], -2);

        TemplateHandler.Assign(ref content, "MessageInformation", Resources.Lang.MessageInformation);
        TemplateHandler.Assign(ref content, "Important", Resources.Lang.Important);
        TemplateHandler.Assign(ref content, "Message_EN", Resources.Lang.English);
        TemplateHandler.Assign(ref content, "Message_CN", Resources.Lang.ChineseTraditional);
        TemplateHandler.Assign(ref content, "Message_TW", Resources.Lang.ChineseSimplified);
        TemplateHandler.Assign(ref content, "Message_TH", Resources.Lang.Thai);
        TemplateHandler.Assign(ref content, "Message_ES", Resources.Lang.Spanish);
        TemplateHandler.Assign(ref content, "Message_VI", Resources.Lang.Vietnamese);
        TemplateHandler.Assign(ref content, "Message_ID", Resources.Lang.Indonesian);
        TemplateHandler.Assign(ref content, "Message_KR", Resources.Lang.Korean);
        TemplateHandler.Assign(ref content, "Message_JA", Resources.Lang.Japanese);
        TemplateHandler.Assign(ref content, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref content, "Delete", Resources.Lang.Delete);
        TemplateHandler.Assign(ref content, "Update", Resources.Lang.Update);
        TemplateHandler.Assign(ref content, "Cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref content, "Add", Resources.Lang.Add);
        TemplateHandler.Assign(ref content, "ConfirmDeleteMess-val", Resources.Lang.ConfirmDeleteMessage);
        TemplateHandler.Assign(ref content, "ConfirmUpdateMess-val", Resources.Lang.ConfirmUpdateMessage);
        TemplateHandler.Assign(ref content, "AddEmptyMessError", Resources.Lang.AddEmptyMessError);

        if (messId != -2)
        {
            TemplateHandler.Assign(ref content, "hdMode-val", "view");
            
            DbResult r = DataHandler.HotNews.GetHotNews(messId);

            if (r.ResultSet.Tables[0].Rows.Count > 0)
            {
                DataRow rw = r.ResultSet.Tables[0].Rows[0];
                if (CastAide.AsBoolean(rw["important"], false))
                    TemplateHandler.Assign(ref content, "hdImportant-val", "on");
                else
                    TemplateHandler.Assign(ref content, "hdImportant-val", "");

                TemplateHandler.Assign(ref content, "MessEn-val", rw["message_en"].ToString());
                TemplateHandler.Assign(ref content, "MessCn-val", rw["message_cn"].ToString());
                TemplateHandler.Assign(ref content, "MessTw-val", rw["message_tw"].ToString());
                TemplateHandler.Assign(ref content, "MessTh-val", rw["message_th"].ToString());
                TemplateHandler.Assign(ref content, "MessEs-val", rw["message_es"].ToString());
                TemplateHandler.Assign(ref content, "MessVi-val", rw["message_vi"].ToString());
                TemplateHandler.Assign(ref content, "MessId-val", rw["message_id"].ToString());
                TemplateHandler.Assign(ref content, "MessKr-val", rw["message_kr"].ToString());
                TemplateHandler.Assign(ref content, "MessJa-val", rw["message_jp"].ToString());
                TemplateHandler.Assign(ref content, "MsgID-val", CastAide.AsInt32(rw["msgid"]));
            }
        }
        else
        {
            TemplateHandler.Assign(ref content, "hdImportant-val", "");
            TemplateHandler.Assign(ref content, "MsgID-val", Resources.Lang.NewMessage);
            TemplateHandler.Assign(ref content, "hdMode-val", "add");
            TemplateHandler.Assign(ref content, "MessEn-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessCn-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessTw-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessTh-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessEs-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessVi-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessId-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessKr-val", string.Empty);
            TemplateHandler.Assign(ref content, "MessJa-val", string.Empty);
        }

        this.HtmlContent = content;
    }
}
