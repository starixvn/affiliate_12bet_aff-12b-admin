using System;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;
using System.Data;

public partial class ViewEmailDetails : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strContent = TemplateHandler.LoadTemplate("Others/ViewEmail.htm");
            TemplateHandler.Assign(ref strContent, "paththeme", AppSettings.ThemeDir);
            TemplateHandler.Assign(ref strContent, "Delete", Resources.Lang.Delete);
            TemplateHandler.Assign(ref strContent, "Cancel", Resources.Lang.Cancel);
            TemplateHandler.Assign(ref strContent, "Subject", Resources.Lang.Subject);
            TemplateHandler.Assign(ref strContent, "From", Resources.Lang.From);
            TemplateHandler.Assign(ref strContent, "Sent", Resources.Lang.SentDate);
            TemplateHandler.Assign(ref strContent, "Content", Resources.Lang.Content);
            TemplateHandler.Assign(ref strContent, "ConfirmDeleteEmail-val", Resources.Lang.ConfirmDeleteEmail);

            int mid = CastAide.AsInt32(Request["mid"],1);
            DbResult r = DataHandler.EmailHistory.EmailHistorySel(mid, string.Empty, string.Empty, string.Empty, null, null,AppSettings.PageSize, 1);

            if(Convert.ToInt32(r.Outputs["nrows"])>0)
            {
                DataRow row = r.ResultSet.Tables[0].Rows[0];
                TemplateHandler.Assign(ref strContent, "Subject-val", row["subject"].ToString());
                TemplateHandler.Assign(ref strContent, "Sender-val", row["sender"].ToString());
                TemplateHandler.Assign(ref strContent, "Email-val", row["email"].ToString());
                TemplateHandler.Assign(ref strContent, "Subject-val", row["subject"].ToString());
                TemplateHandler.Assign(ref strContent, "Sent-val", Common.IBCDateTimeFormat(row["createdate"],DTFormatType.DT24h));
                TemplateHandler.Assign(ref strContent, "Content-val", row["content"].ToString());
                TemplateHandler.Assign(ref strContent, "mid", row["mid"].ToString());
            }
            this.HtmlContent = strContent;
        }
        catch (Exception ex)
        {
            Fanex.Logging.LogService.Error(ex.Message);
        }
    }
}
