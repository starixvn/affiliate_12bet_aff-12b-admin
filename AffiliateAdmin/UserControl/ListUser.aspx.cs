using System;
using System.Data;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;
public partial class ListUser : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("UserControl/ListUser.html");
        TemplateHandler.Assign(ref list, "UserList", Resources.Lang.AdministratorList);
        TemplateHandler.Assign(ref list, "AddUser", Resources.Lang.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddUserExist", Resources.Lang.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "ListDepartment", Resources.Lang.DepartmentList);
        TemplateHandler.Assign(ref list, "AddDepartment", Resources.Lang.AddDepartment);
        TemplateHandler.Assign(ref list, "ViewAllAccount", Resources.Lang.ViewAllAccount);

        TemplateHandler.Assign(ref list, "keyword-text", Resources.Lang.Keyword);
        TemplateHandler.Assign(ref list, "firstletter", Resources.Lang.FirstLetter);

        TemplateHandler.Assign(ref list, "fletter-text", Resources.Lang.FirstLetter);
        TemplateHandler.Assign(ref list, "Department-val", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "ChangeStatusMess-val", Resources.Lang.ChangeStateMess);

        int dep = 0;

        string username = (string.IsNullOrEmpty(Request["txtKeyword"])) ? "Keyword" : Request["txtKeyword"];
        string firstletter = (string.IsNullOrEmpty(Request["alp"])) ? "All" : Request["alp"];

        TemplateHandler.Assign(ref list, "keyword", username);

        dep = (string.IsNullOrEmpty(Request["depid"]) ? 0 : CastAide.AsInt32(Request["depid"]));
        TemplateHandler.Assign(ref list, "search", Resources.Lang.Search);
        TemplateHandler.Assign(ref list, "alphabet", DataHandler.UserControl.GetListAlPhabet(firstletter));
        TemplateHandler.RemoveBlock(ref list, "lmt_chk");
        TemplateHandler.Assign(ref list, "numcol", "9");


        DbResult dbr = DataHandler.UserControl.GetListUser(dep, username, firstletter);

        TemplateHandler.Assign(ref list, "_Value", username);
        TemplateHandler.Assign(ref list, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "No", "#");
        TemplateHandler.Assign(ref list, "UserName", Resources.Lang.Username);
        TemplateHandler.Assign(ref list, "FirstName", Resources.Lang.FirstName);
        TemplateHandler.Assign(ref list, "LastName", Resources.Lang.LastName);
        TemplateHandler.Assign(ref list, "Status", Resources.Lang.Status);
        TemplateHandler.Assign(ref list, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref list, "CreatedDate", Resources.Lang.CreateDate);
        TemplateHandler.Assign(ref list, "Policy", Resources.Lang.Policy);
        TemplateHandler.Assign(ref list, "Reset", Resources.Lang.Password);

        StringBuilder copycontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            DataTable dtb = dbr.ResultSet.Tables[0];
            string strContent = TemplateHandler.CopyBlock(list, "content");
            TemplateHandler.Assign(ref strContent, "over", Common.RowBgOver);
            for (int i = 0; i < dtb.Rows.Count; i++)
            {

                counter++;
                string content = strContent;
                TemplateHandler.Assign(ref content, "No_Value", CastAide.AsString(i + 1));
                TemplateHandler.Assign(ref content, "UserName_Value", dtb.Rows[i]["username"]);
                TemplateHandler.Assign(ref content, "FirstName_Value", dtb.Rows[i]["firstname"]);
                TemplateHandler.Assign(ref content, "LastName_Value", dtb.Rows[i]["lastname"]);
                TemplateHandler.Assign(ref content, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Resources.Lang.Open : Resources.Lang.Closed);
                //TemplateHandler.Assign(ref content, "checked", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "Checked" : string.Empty);
                TemplateHandler.Assign(ref content, "css-div", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "ic-check" : "ic-uncheck");

                TemplateHandler.Assign(ref content, "Creator_Value", dtb.Rows[i]["creator"]);
                TemplateHandler.Assign(ref content, "CreatedDate_Value", Common.IBCDateTimeFormat(dtb.Rows[i]["createdate"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref content, "Policy_Value", Resources.Lang.AccountPolicy);
                TemplateHandler.Assign(ref content, "Department_Value", dtb.Rows[i]["groupname"]);

                TemplateHandler.Assign(ref content, "custid", dtb.Rows[i]["custid"]);
                TemplateHandler.Assign(ref content, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                {
                    TemplateHandler.Assign(ref content, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref content, "Reset_Value", Resources.Lang.Reset);
                    TemplateHandler.Assign(ref content, "changepass-val", Resources.Lang.ChangePassword);
                    TemplateHandler.Assign(ref content, "Class-val", string.Empty);
                }
                else
                {
                    //TemplateHandler.Assign(ref content, "Color", "#cfcfcf");
                    TemplateHandler.ReplaceBlock(ref content, "reset", String.Empty);
                    TemplateHandler.Assign(ref content, "color", Common.RowBgClosed);
                }
                TemplateHandler.Assign(ref content, "Depid", dtb.Rows[i]["groupid"]);
                TemplateHandler.Assign(ref content, "isAdmin", "1");
                copycontent.Append(content);
            }
            TemplateHandler.ReplaceBlock(ref list, "content", copycontent.ToString());
            TemplateHandler.Assign(ref list, "disabled", string.Empty);

        }
        else
        {
            TemplateHandler.ReplaceBlock(ref list, "content", "<tr><td colspan='10' class='c'>" + Resources.Lang.NoInfo + "</td></tr>");
            TemplateHandler.Assign(ref list, "disabled", "disabled");
        }

        TemplateHandler.Assign(ref list, "AddAcc-val", Resources.Lang.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddExistedAcc-val", Resources.Lang.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "numUser", counter.ToString());
        TemplateHandler.Assign(ref list, "DeptId_value", dep);

        this.HtmlContent = list;
    }
}
