using System;
using System.Data;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
public partial class AddUser : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/AddUser.html");
        TemplateHandler.Assign(ref list, "UserList", Resources.Lang.AdministratorList);
        TemplateHandler.Assign(ref list, "AddUser", Resources.Lang.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddUserExist", Resources.Lang.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "ListDepartment", Resources.Lang.DepartmentList);
        TemplateHandler.Assign(ref list, "AddDepartment", Resources.Lang.AddDepartment);

        TemplateHandler.Assign(ref list, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref list, "password", Resources.Lang.Password);
        TemplateHandler.Assign(ref list, "firstname", Resources.Lang.FirstName);
        TemplateHandler.Assign(ref list, "lastname", Resources.Lang.LastName);
        TemplateHandler.Assign(ref list, "department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "Add", Resources.Lang.Add);
        TemplateHandler.Assign(ref list, "Cancel", Resources.Lang.Cancel);

        TemplateHandler.Assign(ref list, "requireUserName", Resources.Lang.ErrUsername);
        TemplateHandler.Assign(ref list, "requirePassword", Resources.Lang.ErrPasswordRequired);
        TemplateHandler.Assign(ref list, "requireDepartment", Resources.Lang.ErrDepartmentChoose);
        TemplateHandler.Assign(ref list, "errSpecialCharacter", Resources.Lang.ErrCharacter);

        StringBuilder res_opt = new StringBuilder();
        DbResult dbr = DataHandler.UserControl.GetSelectDepartment();
        string _option = "<Option value='{opt_value}'>{opt_name}</Option>";

        foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
        {
            string option = _option;
            TemplateHandler.Assign(ref option, "opt_value", dr["groupid"]);
            TemplateHandler.Assign(ref option, "opt_name", dr["groupname"]);
            TemplateHandler.Assign(ref option, "selected", "selected");
            res_opt.Append(option);
        }
        TemplateHandler.Assign(ref _option, "opt_value", "0");
        TemplateHandler.Assign(ref _option, "opt_name", "[Select Dept]");
        TemplateHandler.Assign(ref list, "option", _option + res_opt.ToString());
        this.HtmlContent = list;
    }
}
