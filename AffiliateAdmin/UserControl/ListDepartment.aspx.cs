using System;
using System.Data;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Text;
public partial class ListDepartment : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string list = TemplateHandler.LoadTemplate("UserControl/ListDepartment.html");
        TemplateHandler.Assign(ref list, "UserList", Resources.Lang.AdministratorList);
        TemplateHandler.Assign(ref list, "AddUser", Resources.Lang.AddAdministrator);
        TemplateHandler.Assign(ref list, "AddUserExist", Resources.Lang.AddAdministratorExist);
        TemplateHandler.Assign(ref list, "ListDepartment", Resources.Lang.DepartmentList);
        TemplateHandler.Assign(ref list, "AddDepartment", Resources.Lang.AddDepartment);

        TemplateHandler.Assign(ref list, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "NoOfUser", Resources.Lang.NoOfUser);
        TemplateHandler.Assign(ref list, "Description", Resources.Lang.Description);
        TemplateHandler.Assign(ref list, "RolePolicy", Resources.Lang.RolePolicy);
        TemplateHandler.Assign(ref list, "Edit", Resources.Lang.Edit);
        TemplateHandler.Assign(ref list, "Del", Resources.Lang.Del);
        TemplateHandler.Assign(ref list, "themepath", AppSettings.ThemeDir);
        
        DbResult dbr = null;
        dbr = DataHandler.UserControl.GetListDepartment(null);
        string _content = TemplateHandler.CopyBlock(list, "content");
        TemplateHandler.Assign(ref _content, "over", Common.RowBgOver);
        string _listcontent = "";
        StringBuilder strcontent = new StringBuilder();
        int counter = 0;
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
            {
                _listcontent = _content;
                counter++;
                TemplateHandler.Assign(ref _listcontent, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref _listcontent, "No_Value", counter.ToString());
                TemplateHandler.Assign(ref _listcontent, "Department_Value", dr["groupname"]);
                if (string.Compare(CastAide.AsString(dr["groupname"]), "None", true) == 0)
                {
                    TemplateHandler.RemoveBlock(ref _listcontent, "lmt-DepNone");
                }
                if (CastAide.AsInt32(dr["nouser"]) <= 0)
                {
                    TemplateHandler.ReplaceBlock(ref _listcontent, "lmt-nouser", dr["nouser"].ToString());
                }
                else
                {
                    TemplateHandler.Assign(ref _listcontent, "NoOfUser_Value", CastAide.AsInt32(dr["nouser"]).ToString(Common.NumberFormatPlaceholder));
                }
                TemplateHandler.Assign(ref _listcontent, "Description_Value", dr["groupdesc"]);
                TemplateHandler.Assign(ref _listcontent, "RolePolicy_Value", Resources.Lang.RolePolicy);
                TemplateHandler.Assign(ref _listcontent, "Edit", Resources.Lang.Edit);
                TemplateHandler.Assign(ref _listcontent, "Del", Resources.Lang.Del);
                TemplateHandler.Assign(ref _listcontent, "depid", dr["groupid"]);
                strcontent.Append(_listcontent);
            }
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref _listcontent, "content", Common.GenNoInformation("7"));
        }
        TemplateHandler.Assign(ref list, "numDep", counter.ToString());
        TemplateHandler.ReplaceBlock(ref list, "content", strcontent.ToString());
        TemplateHandler.Assign(ref list, "submenu", SubMenu.GenSubMenu(SubMenu.AdminLink,
                                                                        SubMenu.AdminText,
                                                                        1, counter, Resources.Lang.Administrator,
                                                                        "i_admin.png"));
        TemplateHandler.Assign(ref list, "ChooseConfirm", Resources.Lang.ConfirmDelete);
        TemplateHandler.Assign(ref list, "chooseAtLeast", Resources.Lang.ChooseAtLeast);
        TemplateHandler.Assign(ref list, "AltStatusSucc", Resources.Lang.UpdateStaSucc);
        TemplateHandler.Assign(ref list, "AddDept-val", Resources.Lang.AddDepartment);


        this.HtmlContent = list;
    }
}
