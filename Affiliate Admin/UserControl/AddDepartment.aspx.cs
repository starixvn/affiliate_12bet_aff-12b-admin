using System;
using System.Web;
using DNAide.Web;

public partial class AddDepartment : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/AddDepartment.html");
        TemplateHandler.Assign(ref list, "UserList", Resources.Lang.AdministratorList);
        TemplateHandler.Assign(ref list, "AddUser", Resources.Lang.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddUserExist", Resources.Lang.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "ListDepartment", Resources.Lang.DepartmentList);
        TemplateHandler.Assign(ref list, "AddDepartment", Resources.Lang.AddDepartment);
        TemplateHandler.Assign(ref list, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "Description", Resources.Lang.Description);
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Add);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);
        //TemplateHandler.Assign(ref list, "submenu", Common.GenSubMenu(new string[] { "ListUser.aspx", "AddUser.aspx", "AddUserExist.aspx", "ListDepartment.aspx", "AddDepartment.aspx" }, new string[] { Resources.Lang.AdministratorList, Resources.Lang.AddAdministrator, Resources.Lang.AddAdministratorExist, Resources.Lang.DepartmentList, Resources.Lang.AddDepartment }, 731, 4));
        this.HtmlContent = list;

    }
}
