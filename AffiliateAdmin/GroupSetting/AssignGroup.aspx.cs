using System;
using DNAide.Core;

public partial class GroupSetting_AssignGroup : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.IsGetAllCss = false;
        int affID = CastAide.AsInt32(Request["affID"],-1);
        bool isApproved = DNAide.Core.CastAide.AsBoolean(Request["IsApproved"], false);
        string affName = DNAide.Core.CastAide.AsString(Request["affName"],string.Empty).ToUpper();
        string MailOption = Request["mailoption"];
        this.HtmlContent = GenerateHTML.GroupSetting.GetAssignmentForm(affName, affID, isApproved, MailOption);   
    }
}
