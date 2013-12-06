using System;
using DNAide.Web;
using DNAide.Core;

public partial class AffiliateList : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTemplate = TemplateHandler.LoadTemplate("UserInfo/AffList.htm");
        #region Assign submenu
        string strStatus = CastAide.AsString(Request["status"], Common.Approved);

        int currTab = 0;
        int width = 650;
        bool isFavor = false;
        bool isSelectAll = false;
        bool isClosed = false;
        switch (strStatus.ToLower())
        {
            //TemplateHandler.Assign(ref strTemplate, "submenu", Common.GenSubMenu(strArrLink, strArrText, 1171, 0));
            case "pending":
                currTab = 2;
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");
                TemplateHandler.RemoveBlock(ref strTemplate, "approved1"); 
                TemplateHandler.Assign(ref strTemplate, "tabpending", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Resources.Lang.registerdate);
                TemplateHandler.ReplaceBlock(ref strTemplate, "pending", Resources.Lang.Pending + " [<span id='nummem'>{nummem-val}</span>]");
                break;
            case "approved":
                isFavor = CastAide.AsInt32(Request["favor"], 0) == 1 ? true : false;
                isClosed = CastAide.AsBoolean(Request["isClosed"], false);
                //isSelectAll = true;
                currTab = isFavor ? 0 : 1;
                currTab = isClosed ? 4 : currTab;
                TemplateHandler.Assign(ref strTemplate, "groupname-text", Resources.Lang.GroupName);
                TemplateHandler.Assign(ref strTemplate, "tabapproved", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Resources.Lang.approvaldate);
                TemplateHandler.Assign(ref strTemplate, "pendingearning", Resources.Lang.PendingComm);
                TemplateHandler.Assign(ref strTemplate, "balancebroughtforward", Resources.Lang.BalanceForward);
                TemplateHandler.ReplaceBlock(ref strTemplate, "approved", Resources.Lang.Approved + " [<span id='nummem'>{nummem-val}</span>]");
                width = 1050;
                break;
            case "disapproved":
                currTab = 3;
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");
                TemplateHandler.RemoveBlock(ref strTemplate, "approved1"); 
                TemplateHandler.Assign(ref strTemplate, "tabdisapproved", string.Empty);
                TemplateHandler.Assign(ref strTemplate, "userdate", Resources.Lang.disapprovaldate);
                TemplateHandler.ReplaceBlock(ref strTemplate, "disapproved", Resources.Lang.Disapproved + " [<span id='nummem'>{nummem-val}</span>]");

                break;
            default:
                TemplateHandler.RemoveBlock(ref strTemplate, "GroupNameSearch");
                TemplateHandler.RemoveBlock(ref strTemplate, "total");
                TemplateHandler.RemoveBlock(ref strTemplate, "pass");   
                TemplateHandler.Assign(ref strTemplate, "userdate", Resources.Lang.registerdate);
                TemplateHandler.Assign(ref strTemplate, "tabpending", string.Empty);
                TemplateHandler.ReplaceBlock(ref strTemplate, "pending", Resources.Lang.Pending + " [{nummem}]");
                break;
        }
        TemplateHandler.Assign(ref strTemplate, "tabdisapproved", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "tabapproved", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "tabpending", "tabbottom");
        TemplateHandler.Assign(ref strTemplate, "disapproved", Resources.Lang.Disapproved);
        TemplateHandler.Assign(ref strTemplate, "approved", Resources.Lang.Approved);
        TemplateHandler.Assign(ref strTemplate, "pending", Resources.Lang.Pending);
        #endregion

        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "pending", Resources.Lang.Pending);
        TemplateHandler.Assign(ref strTemplate, "approved", Resources.Lang.Approved);
        TemplateHandler.Assign(ref strTemplate, "disapproved", Resources.Lang.Disapproved);
        TemplateHandler.Assign(ref strTemplate, "username-text", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTemplate, "email-text", Resources.Lang.Email);
        TemplateHandler.Assign(ref strTemplate, "id-text", Resources.Lang.ID);
        TemplateHandler.Assign(ref strTemplate, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTemplate, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTemplate, "language", Resources.Lang.Language);
        TemplateHandler.Assign(ref strTemplate, "group", Resources.Lang.CommissionGroup);
        TemplateHandler.Assign(ref strTemplate, "com", Resources.Lang.Com);
        TemplateHandler.Assign(ref strTemplate, "member", Resources.Lang.Member);
        TemplateHandler.Assign(ref strTemplate, "password", Resources.Lang.Password);        
        TemplateHandler.Assign(ref strTemplate, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTemplate, "report", Resources.Lang.Report);
        //TemplateHandler.Assign(ref strTemplate, "reportdetails", Resources.Lang.ReportDetails);

        //Search content
        int nummem = 0;
        int totalPage = 0;
        int intID = -1;
        try
        {
            intID = CastAide.AsInt32(Request["txtID"], -1);
        }
        catch
        {
            intID = -2;
            TemplateHandler.Assign(ref strTemplate, "id", Request["txtID"]);
        }
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);
        string strUserName = CastAide.AsString(Request["txtUsername"], string.Empty);
        string strEmail = CastAide.AsString(Request["txtEmail"], string.Empty);
        string groupName = CastAide.AsString(Request["txtGroupName"], string.Empty);
        int groupID = CastAide.AsInt32(Request["groupid"], -1);
        /*bool IsViewFromAccountPage = CastAide.AsBoolean(Request["IsViewFromAccountPage"],false);
        if (IsViewFromAccountPage)
        {
            TemplateHandler.Assign(ref strTemplate, "IsViewFromAccountPage_val", "true");
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "IsViewFromAccountPage_val", "false");
        }*/

        TemplateHandler.Assign(ref strTemplate, "GroupID_val", groupID);
        TemplateHandler.Assign(ref strTemplate, "username", string.IsNullOrEmpty(strUserName) ? Resources.Lang.Username : strUserName);
        TemplateHandler.Assign(ref strTemplate, "email", string.IsNullOrEmpty(strEmail) ? Resources.Lang.Email : strEmail);
        TemplateHandler.Assign(ref strTemplate, "id", intID == -1 ? Resources.Lang.ID : intID.ToString());

        strUserName = ((string.Compare(strUserName, Resources.Lang.Username, true) == 0 ? string.Empty : strUserName));
        strEmail = ((string.Compare(strEmail, Resources.Lang.Email, true) == 0 ? string.Empty : strEmail));
        groupName = ((string.Compare(groupName, Resources.Lang.GroupName, true) == 0 ? string.Empty : groupName));

        string strContent = AffAdmin.GenerateHTML.AffiliateList.GetAffList(groupID,ref groupName,strUserName, strEmail, intID, strStatus,isClosed, intPage, intPageSize, out nummem, ref totalPage, isFavor);
        if (string.Compare(strStatus,"approved",true)==0)
        {
            TemplateHandler.Assign(ref strTemplate, "groupname-val", groupName);
            TemplateHandler.Assign(ref strTemplate, "paging-top", Common.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", null, null, width, isSelectAll, true, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "paging-top", Common.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageTop", "txtPageTop", null, null, width, isSelectAll, true, false, false));
            TemplateHandler.RemoveBlock(ref strTemplate, "bottombtn");
        }
        TemplateHandler.Assign(ref strTemplate, "paging-bottom", Common.GenPaging(totalPage, intPage, intPageSize, nummem, "aPageBottom", "txtPageBottom", width, isSelectAll, false));
        if (nummem == 0)//No row return
        {
            TemplateHandler.Assign(ref strTemplate, "pagingcss", "hide");
            TemplateHandler.RemoveBlock(ref strTemplate, "bottombtn");            
            
            if (isFavor)//for favorite
            {
                if (Request["isclk"] == "1")//is click on menu header
                {
                    Response.Write("<script type='text/javascript'>alert('My favorite list is empty. You will be redirected to the Approved Page.'); location.href = 'AffiliateList.aspx?status=approved';</script>");
                    return;
                }
                if (string.IsNullOrEmpty(groupName) && string.IsNullOrEmpty(strUserName) && string.IsNullOrEmpty(strEmail) && intID == -1)//no info and no search
                    Response.Write("<script type='text/javascript'>location.href = 'AffiliateList.aspx?status=approved';</script>");                
            }
        }

        //Sub menu        
        TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, currTab, nummem, Resources.Lang.AffList,"i_affiliateslist.png"));
        

        TemplateHandler.Assign(ref strTemplate, "timer-val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);
        TemplateHandler.Assign(ref strTemplate, "status-val", strStatus.ToLower());
        TemplateHandler.Assign(ref strTemplate, "page", intPage);
        TemplateHandler.Assign(ref strTemplate, "pagesize", intPageSize);
        TemplateHandler.Assign(ref strTemplate, "nummem-val", nummem);
        TemplateHandler.Assign(ref strTemplate, "favor-val", Request["favor"]);
        TemplateHandler.Assign(ref strTemplate, "isclosed-val", isClosed);

        TemplateHandler.ReplaceBlock(ref strTemplate, "content", strContent);
        this.HtmlContent = strTemplate;
        
    }
    string RefreshIcon(string strStatus)
    {
        return "&nbsp;&nbsp;<span class=\"refresh\" title=\"refresh\" onclick=\"Refresh('AffiliateList.aspx?status=" + strStatus + "')\">&nbsp;</span>";
    }
}
