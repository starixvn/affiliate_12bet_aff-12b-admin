using System.Text;
using System.Web;

namespace AffiliateAdmin.Business.Services
{
    public class GetCommGroupService
    {
        public void GetCommGroup(HttpContext context)
        {
            if (context.Request.UrlReferrer != null)
            {
                IBCHandler.InitializeCulture(HttpContext.Current);
                context.Response.ContentType = "text/plain";
                string affTypeID = context.Request["affTypeId"];

                string assignTemplate = DNAide.Web.TemplateHandler.LoadTemplate("GroupSetting/GroupAssignment.htm");
                string optionTemplate = DNAide.Web.TemplateHandler.CopyBlock(assignTemplate, "SelectOption");
                StringBuilder list = new StringBuilder();
            }
        }
    }
}