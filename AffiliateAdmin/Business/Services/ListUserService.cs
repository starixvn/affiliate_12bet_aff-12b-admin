using System.Data;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Web;

namespace AffiliateAdmin.Business.Services
{
    public class ListUserService
    {
        public void ListUser(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            string list = TemplateHandler.LoadTemplate("UserControl/ListUserContent.html");

            int dep = (string.IsNullOrEmpty(context.Request["depid"]) ? 0 : CastAide.AsInt32(context.Request["depid"]));
            string depname = (string.IsNullOrEmpty(context.Request["depname"]) ? "" : context.Request["depname"]);

            string username = (string.IsNullOrEmpty(context.Request["us"])) ? "All" : context.Request["us"];
            string firstletter = (string.IsNullOrEmpty(context.Request["fl"])) ? "All" : context.Request["fl"];
            TemplateHandler.Assign(ref list, "search", Resources.Lang.Search);
            TemplateHandler.Assign(ref list, "alphabet", DataHandler.UserControl.GetListAlPhabet(firstletter));

            TemplateHandler.Assign(ref list, "Dep_value", depname);
            TemplateHandler.Assign(ref list, "DeptId_value", dep.ToString());

            TemplateHandler.Assign(ref list, "numcol", "8");
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
            TemplateHandler.Assign(ref list, "ViewAllAccount", Resources.Lang.ViewAllAccount);

            TemplateHandler.Assign(ref list, "keyword", Resources.Lang.Keyword);
            TemplateHandler.Assign(ref list, "firstletter", Resources.Lang.FirstLetter);
            TemplateHandler.Assign(ref list, "isAdmin", "1");

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
                    TemplateHandler.Assign(ref content, "color", (counter % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                    TemplateHandler.Assign(ref content, "No_Value", CastAide.AsString(i + 1));
                    TemplateHandler.Assign(ref content, "UserName_Value", dtb.Rows[i]["username"]);
                    TemplateHandler.Assign(ref content, "FirstName_Value", dtb.Rows[i]["firstname"]);
                    TemplateHandler.Assign(ref content, "LastName_Value", dtb.Rows[i]["lastname"]);
                    TemplateHandler.Assign(ref content, "Status_Value", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? Resources.Lang.Open : Resources.Lang.Closed);
                    TemplateHandler.Assign(ref content, "checked", (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false) ? "Checked" : string.Empty);

                    TemplateHandler.Assign(ref content, "Creator_Value", dtb.Rows[i]["creator"]);
                    TemplateHandler.Assign(ref content, "CreatedDate_Value", dtb.Rows[i]["createdate"]);
                    TemplateHandler.Assign(ref content, "Policy_Value", Resources.Lang.AccountPolicy);

                    TemplateHandler.Assign(ref content, "custid", dtb.Rows[i]["custid"]);
                    TemplateHandler.Assign(ref content, "status", CastAide.AsBoolean(dtb.Rows[i]["closed"]));
                    if (CastAide.AsBoolean(dtb.Rows[i]["closed"]) == false)
                    {
                        TemplateHandler.Assign(ref content, "Color", "#ffffff");
                        TemplateHandler.Assign(ref content, "Reset_Value", Resources.Lang.Reset);
                    }
                    else
                    {
                        TemplateHandler.Assign(ref content, "Color", "#cfcfcf");
                        TemplateHandler.ReplaceBlock(ref content, "reset", Resources.Lang.Reset);
                    }
                    TemplateHandler.Assign(ref content, "Depid", dtb.Rows[i]["groupid"]);
                    copycontent.Append(content);
                }
                TemplateHandler.ReplaceBlock(ref list, "content", copycontent.ToString());
                TemplateHandler.Assign(ref list, "disabled", string.Empty);
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref list, "content", Common.NoInfo(10));
                TemplateHandler.Assign(ref list, "disabled", "disabled");
            }

            TemplateHandler.Assign(ref list, "numUser", counter.ToString());

            context.Response.Write(list);
        }
    }
}