using System;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Web;
public partial class EditDepartment : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/EditDepartment.html");
        string depid = Request["depid"];
        TemplateHandler.Assign(ref list, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "Description", Resources.Lang.Description);
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);
        TemplateHandler.Assign(ref list, "depid", depid);

        TemplateHandler.Assign(ref list, "requireDepartmentName", depid);
        TemplateHandler.Assign(ref list, "updateSuccess", depid);

        DbResult dbr = null;
        dbr = DataHandler.UserControl.GetListDepartment(depid);
        if (dbr.ResultSet.Tables[0].Rows.Count > 0)
        {
            TemplateHandler.Assign(ref list, "DepartmentName", dbr.ResultSet.Tables[0].Rows[0]["groupname"]);
            TemplateHandler.Assign(ref list, "DesciptionName", dbr.ResultSet.Tables[0].Rows[0]["groupdesc"]);
        }
        this.HtmlContent = list;
    }
}
