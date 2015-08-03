using System;
using System.Text;
using DNAide.Web;
using DNAide.Core;

public partial class GroupSetting_EditGroup : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.IsGetAllCss = false;
        string result=string.Empty;        
        string groupid=Request["groupid"];
        if (string.IsNullOrEmpty(groupid) ){
            result="There is no information available!";
            this.HtmlContent = result;
            return;
        }
        int groupID=-1;
        try{
            groupID = Convert.ToInt32(groupid);
             
        }catch{
            result = "There is no information available!";
        }
        if (string.IsNullOrEmpty(result))
        {
            result = GenerateHTML.GroupSetting.GetGroupSettingForEdit(groupID);
        }
        this.HtmlContent=result;
    }
}
