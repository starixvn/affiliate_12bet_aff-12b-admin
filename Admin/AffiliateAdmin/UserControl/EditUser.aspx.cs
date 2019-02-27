using System;
using System.Data;
using System.Web;
using DNAide.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using System.Text;
public partial class EditUser : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.Private);
        string list = TemplateHandler.LoadTemplate("UserControl/EditUser.html");
        TemplateHandler.Assign(ref list, "FirstName", Resources.Lang.FirstName);
        TemplateHandler.Assign(ref list, "LastName", Resources.Lang.LastName);
        TemplateHandler.Assign(ref list, "Department", Resources.Lang.Department);
        TemplateHandler.Assign(ref list, "AlertSucc","The " + Request["us"].ToString() + " " + Common.GetAlertMsg(3));
        
        TemplateHandler.Assign(ref list, "UserName", Request["us"].ToString());

        int isdep = string.IsNullOrEmpty(Request["isdep"]) ? 0 : CastAide.AsInt32(Request["isdep"]);
        TemplateHandler.Assign(ref list, "isdep", isdep.ToString());

        int depid = CastAide.AsInt32(Request["depid"]);
        int custid =string.IsNullOrEmpty(Request["custid"]) ? 0 :  CastAide.AsInt32(Request["custid"]);
        string deptname = string.IsNullOrEmpty(Request["depname"]) ? "" : Request["depname"];

        TemplateHandler.Assign(ref list, "Dep_value", deptname);
        TemplateHandler.Assign(ref list, "depname", Request["deptname"]);
        TemplateHandler.Assign(ref list, "custid", custid.ToString());
        TemplateHandler.Assign(ref list, "depid", depid.ToString());
        StringBuilder res_opt = new StringBuilder();
        DbResult dbr = DataHandler.UserControl.GetSelectDepartment();
        string _option = "<Option value='{opt_value}' {selected}>{opt_name}</Option>";

        foreach (DataRow dr in dbr.ResultSet.Tables[0].Rows)
        {
            string option = _option;
            if (depid == CastAide.AsInt32(dr["groupid"]))
            {
                TemplateHandler.Assign(ref option, "selected", "selected");
            }
            else
            {
                TemplateHandler.Assign(ref option, "selected", string.Empty);
            }
            TemplateHandler.Assign(ref option, "opt_value", dr["groupid"]);
            TemplateHandler.Assign(ref option, "opt_name", dr["groupname"]);            
            res_opt.Append(option);
        }
      
        TemplateHandler.Assign(ref list, "option", res_opt.ToString());               
        TemplateHandler.Assign(ref list, "submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref list, "cancel", Resources.Lang.Cancel);

        DbResult dbruser = DataHandler.UserControl.GetOnlyUser(custid);
        if (dbruser.ResultSet.Tables[0].Rows.Count > 0)
        {
            TemplateHandler.Assign(ref list, "firstname_value", dbruser.ResultSet.Tables[0].Rows[0]["firstname"]);
            TemplateHandler.Assign(ref list, "lastname_value", dbruser.ResultSet.Tables[0].Rows[0]["lastname"]);
        }
        this.HtmlContent = list;
    }
}
