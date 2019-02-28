using System.Web;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;

namespace AffiliateAdmin.Business.Services
{
    public class UpdateDepartmentService
    {
        public void Update(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string status = (string.IsNullOrEmpty(context.Request.Form["sta"]) ? "Add" : context.Request.Form["sta"].ToString());

            string groupN = CastAide.AsString(context.Request.Form["depname"]);
            string groupD = CastAide.AsString(context.Request.Form["depdesc"]);
            DbResult dbr = null;

            switch (status)
            {
                case "Edit":
                    {
                        int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                        dbr = DataHandler.UserControl.UpdateDepartment(groupId, groupN, groupD, AppSettings.Session.CustID);
                        break;
                    }
                case "Add":
                    {
                        dbr = DataHandler.UserControl.AddDepartment(groupN, groupD, AppSettings.Session.CustID);
                        break;
                    }
                case "Del":
                    {
                        int groupId = CastAide.AsInt32(context.Request.Form["depid"]);
                        dbr = DataHandler.UserControl.DelDepartment(groupId, AppSettings.Session.CustID);
                        break;
                    }
            }

            if (CastAide.AsInt32(dbr.Outputs["errcode"]) == 0)
            {
                context.Response.Write("Ok");
            }
            else
            {
                context.Response.Write(dbr.Outputs["errmsg"].ToString());
            }
        }
    }
}