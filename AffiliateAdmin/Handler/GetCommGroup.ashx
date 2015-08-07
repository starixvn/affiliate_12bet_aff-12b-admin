<%@ WebHandler Language="C#" Class="GetSubCategory" %>
using System;
using System.Web;
using DNAide.Core;
using System.Web.SessionState;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using System.Data;

public class GetSubCategory : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.UrlReferrer != null)
        {
            IBCHandler.InitializeCulture(HttpContext.Current);
            context.Response.ContentType = "text/plain";
            string affTypeID = context.Request["affTypeId"];
            
            string assignTemplate = DNAide.Web.TemplateHandler.LoadTemplate("GroupSetting/GroupAssignment.htm");
            string optionTemplate = DNAide.Web.TemplateHandler.CopyBlock(assignTemplate, "SelectOption");
            StringBuilder list = new StringBuilder();
            //DbResult result = DataHandler.GroupSetting.CommGroupSelByAffType(CastAide.AsInt32(affTypeID));            
            
            //foreach (DataRow row in result.ResultSet.Tables[0].Rows)
            //{
            //    string option = optionTemplate;
                
            //    DNAide.Web.TemplateHandler.Assign(ref option, "option_value", row["groupid"]);
            //    DNAide.Web.TemplateHandler.Assign(ref option, "option_name", row["groupname"]);
            //    list.Append(option);
            //}
            //context.Response.Write(list.ToString());                        
        }

    }
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}
