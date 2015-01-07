using System.Text;
using System.Data;
using DNAide.Web;
using DNAide.Data;

/// <summary>
/// Summary description for Note
/// </summary>
/// 
namespace GenerateHTML
{
    public class Note
    {
        public static string GenNoteList(int custid, int pageIndex, int pageSize, ref bool hasData, ref int rows)
        {
            string strTemplate = TemplateHandler.LoadTemplate("UserInfo/NoteList.htm");
            string strTpl = TemplateHandler.CopyBlock(strTemplate, "notelist");
            TemplateHandler.Assign(ref strTpl, "content", Resources.Lang.Content);
            TemplateHandler.Assign(ref strTpl, "createdby", Resources.Lang.CreatedBy);
            TemplateHandler.Assign(ref strTpl, "lastmodify", Resources.Lang.LastModify);
            TemplateHandler.Assign(ref strTpl, "del", Resources.Lang.Delete);
            TemplateHandler.Assign(ref strTpl, "upd", Resources.Lang.Update);
            TemplateHandler.Assign(ref strTpl, "edit", Resources.Lang.Edit);
            int totalPages = 0;

            DbResult result = DataHandler.Affiliate.SelNoteList(custid, pageIndex, ref rows, ref totalPages, pageSize);
            DataTable dtb = new DataTable();
            if (result.ResultSet.Tables.Count > 0 )
                dtb = result.ResultSet.Tables[0];
            if (dtb.Rows.Count > 0)
            {
                hasData = true;
                string strContent = TemplateHandler.CopyBlock(strTpl, "content");
                StringBuilder strBuild = new StringBuilder();
                TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
                int i = 0;
                foreach (DataRow dr in dtb.Rows)
                {
                    i++;
                    string strTmp = strContent;
                    TemplateHandler.Assign(ref strTmp, "color", (i % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref strTmp, "no-val", (pageIndex-1)*pageSize + i);
                    TemplateHandler.Assign(ref strTmp, "content-val", Common.CutLongText(dr["Note"].ToString(), 60).Replace("\r\n", "<br>").Replace("\n", "<br>"));
                    TemplateHandler.Assign(ref strTmp, "contentorg-val", dr["Note"].ToString().Replace("\r\n", "<br>").Replace("\n", "<br>"));
                    TemplateHandler.Assign(ref strTmp, "createdby-val", dr["UpdateName"]);
                    TemplateHandler.Assign(ref strTmp, "lastmodify-val", Common.IBCDateTimeFormat(dr["Updateddate"], DTFormatType.DT24h));
                    TemplateHandler.Assign(ref strTmp, "noteid", dr["noteid"]);
                    TemplateHandler.Assign(ref strTmp, "custid", custid);
                    strBuild.Append(strTmp);
                }
                //strBuild.Append(Common.GenPagingFooter("Note.aspx", totalPages, pageIndex, 8));
                TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
                TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(totalPages, pageIndex, pageSize, rows, "aPageTop", "txtPageTop", new string[] {Resources.Lang.Add }, new string[] { "AddNote();" }, 682, false, true, true, false));
                TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(totalPages, pageIndex, pageSize, rows, "aPageBottom", "txtPageBottom", 682, false, false));
            }
            else
            {
                hasData = false;
                TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(5));
                TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
                TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
            }

            
            return strTpl;
        }
    }
}
