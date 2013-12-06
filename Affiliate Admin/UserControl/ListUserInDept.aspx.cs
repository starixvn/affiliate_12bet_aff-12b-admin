using System;
using System.Text;
using System.Data;
using DNAide.Core;
using DNAide.Data;
using DNAide.Web;

public partial class UserControl_ListUserInDept : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("UserControl/ListUserInDept.htm");

        //assigne static annotation
        TemplateHandler.Assign(ref content, "ViewAllAccount", Resources.Lang.ViewAllAccount);
        TemplateHandler.Assign(ref content, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref content, "keyword", Resources.Lang.Keyword);
        TemplateHandler.Assign(ref content, "firstletter", Resources.Lang.FirstLetter);
        TemplateHandler.Assign(ref content, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref content, "UserName", Resources.Lang.Username);
        TemplateHandler.Assign(ref content, "FirstName", Resources.Lang.FirstName);
        TemplateHandler.Assign(ref content, "LastName", Resources.Lang.LastName);
        TemplateHandler.Assign(ref content, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref content, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref content, "CreatedDate", Resources.Lang.CreateDate);
        TemplateHandler.Assign(ref content, "Policy", Resources.Lang.Policy);
        TemplateHandler.Assign(ref content, "Password", Resources.Lang.Password);
        TemplateHandler.Assign(ref content, "Policy_Value", Resources.Lang.RolePolicy);
        TemplateHandler.Assign(ref content, "Keyword-val", Resources.Lang.Keyword);
        TemplateHandler.Assign(ref content, "Update-val", Resources.Lang.Update);
        TemplateHandler.Assign(ref content, "ChangeStatusMess-val", Resources.Lang.ChangeStateMess);
        //TemplateHandler.Assign(ref content, "Reset_Value", Resources.Lang.Reset);


        //retrive params from request
        int depip = CastAide.AsInt32(Request["depid"], 0);
        string depname = CastAide.AsString(Request["depname"], string.Empty);
        string username = CastAide.AsString(Request["keyword"], "Keyword");
        string firstletter = CastAide.AsString(Request["alp"], "All");

        //assign dynamic annotation
        TemplateHandler.Assign(ref content, "fletter-val", firstletter.ToString());
        TemplateHandler.Assign(ref content, "alphabet", DataHandler.UserControl.GetListAlPhabet(firstletter));
        TemplateHandler.Assign(ref content, "_Value", username);
        TemplateHandler.Assign(ref content, "Dep_value", depname);
        TemplateHandler.Assign(ref content, "depid-val", depip.ToString());
        TemplateHandler.Assign(ref content, "isReload", string.IsNullOrEmpty(Request["isReload"]) ? "0" : Request["isReload"]);

        DbResult dbr = DataHandler.UserControl.GetListUser(depip, username, firstletter);

        StringBuilder copycontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            DataTable dtb = dbr.ResultSet.Tables[0];
            string strContent = TemplateHandler.CopyBlock(content, "content");
            TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                counter++;
                string row = strContent;
                //TemplateHandler.Assign(ref row, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref row, "No_Value", CastAide.AsString(i + 1));
                TemplateHandler.Assign(ref row, "UserName_Value", dtb.Rows[i]["username"]);
                TemplateHandler.Assign(ref row, "FirstName_Value", dtb.Rows[i]["firstname"]);
                TemplateHandler.Assign(ref row, "LastName_Value", dtb.Rows[i]["lastname"]);
                TemplateHandler.Assign(ref row, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Resources.Lang.Open : Resources.Lang.Closed);
                TemplateHandler.Assign(ref row, "div-class", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "chk ic-check" : "chk ic-uncheck");

                TemplateHandler.Assign(ref row, "Creator_Value", dtb.Rows[i]["creator"]);
                TemplateHandler.Assign(ref row, "CreatedDate_Value", Common.IBCDateTimeFormat(dtb.Rows[i]["createdate"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref row, "Policy_Value", Resources.Lang.AccountPolicy);

                TemplateHandler.Assign(ref row, "custid", dtb.Rows[i]["custid"]);
                TemplateHandler.Assign(ref row, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                {
                    TemplateHandler.Assign(ref row, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref row, "Reset_Value", Resources.Lang.Reset);
                }
                else
                {
                    TemplateHandler.Assign(ref row, "color", Common.RowBgClosed);
                    TemplateHandler.ReplaceBlock(ref row, "reset", string.Empty);
                }
                TemplateHandler.Assign(ref row, "depid", dtb.Rows[i]["groupid"]);
                TemplateHandler.Assign(ref row, "isAdmin", "1");
                copycontent.Append(row);
            }
            TemplateHandler.ReplaceBlock(ref content, "content", copycontent.ToString());
            TemplateHandler.Assign(ref content, "display-val", string.Empty);
            TemplateHandler.Assign(ref content, "totalAdmin-val", counter);

        }
        else
        {
            TemplateHandler.ReplaceBlock(ref content, "content", Common.NoInfo(10));
            TemplateHandler.Assign(ref content, "display-val", "none");
        }
        this.HtmlContent = content;
    }
}
