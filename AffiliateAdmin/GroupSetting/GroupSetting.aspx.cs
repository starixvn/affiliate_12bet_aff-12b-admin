using System;
using System.Text;
using DNAide.Web;
using DNAide.Core;

public partial class GroupSetting : IBCPage
{
    string strTpl = string.Empty;
    StringBuilder strListRow = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        string content = TemplateHandler.LoadTemplate("GroupSetting/GroupSetting.htm");
        TemplateHandler.Assign(ref content, "GroupName", Resources.Lang.GroupName);
      //  TemplateHandler.Assign(ref content, "No", Resources.Lang.No);
        TemplateHandler.Assign(ref content, "Name", Resources.Lang.Name);
        TemplateHandler.Assign(ref content, "Description", Resources.Lang.Description);
        TemplateHandler.Assign(ref content, "Creator", Resources.Lang.Creator);
        TemplateHandler.Assign(ref content, "CreatedDate", Resources.Lang.CreatedDate);
        TemplateHandler.Assign(ref content, "Policy", Resources.Lang.Policy);
        TemplateHandler.Assign(ref content, "Sport", Resources.Lang.Sport);
        TemplateHandler.Assign(ref content, "Tier", Resources.Lang.Tier);
        TemplateHandler.Assign(ref content, "MinAmt", Resources.Lang.MinAmount);
        TemplateHandler.Assign(ref content, "ServerDateTime", DateTime.Now.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref content, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref content, "MaxAmt", Resources.Lang.MaxAmount);
        TemplateHandler.Assign(ref content, "MinQM", Resources.Lang.MinQM);
        TemplateHandler.Assign(ref content, "MaxQM", Resources.Lang.MaxQM);
        TemplateHandler.Assign(ref content, "Comm", Resources.Lang.Comm);
       /* TemplateHandler.Assign(ref content, "Bonus", Resources.Lang.Bonus);
        TemplateHandler.Assign(ref content, "Adjustment", Resources.Lang.Adjustment);
        TemplateHandler.Assign(ref content, "MKT", Resources.Lang.MKT);
        TemplateHandler.Assign(ref content, "PSP", Resources.Lang.PSP);*/
        TemplateHandler.Assign(ref content, "MinAmount_Explain", Resources.Lang.MinAmountExplain);
        TemplateHandler.Assign(ref content, "MaxAmount_Explain", Resources.Lang.MaxAmountExplain);
        TemplateHandler.Assign(ref content, "MinQM_Explain", Resources.Lang.MinQMExplain);
        TemplateHandler.Assign(ref content, "MaxQM_Explain", Resources.Lang.MaxQMExplain);
        TemplateHandler.Assign(ref content, "Deductions", Resources.Lang.Deductions);

        TemplateHandler.Assign(ref content, "Action", Resources.Lang.Action);
        TemplateHandler.Assign(ref content, "NoOfAffiliate", Resources.Lang.NoOfAffiliate);
        TemplateHandler.Assign(ref content, "AddNewGroup", Resources.Lang.AddNewGroup);
        TemplateHandler.Assign(ref content, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref content, "To", Resources.Lang.To);
        TemplateHandler.Assign(ref content, "over", Common.RowBgOver);
        
        int minNo = 0, maxNo = 0, totalRows = 0, totalpage = 0;
        
        int currPage = CastAide.AsInt32(Request["page"], 1);//get the current page from hidden field in form
        int currPageSize = CastAide.AsInt32(Request["slPageSize"],AppSettings.PageSize);//get the current page from hidden field in form
        TemplateHandler.Assign(ref content, "pagesize_val", currPageSize);
        TemplateHandler.Assign(ref content, "timer_val", string.IsNullOrEmpty(Request["slTime"]) ? "0" : Request["slTime"]);

        string strGroupName = CastAide.AsString(Request["txtGroupName"], string.Empty);
        TemplateHandler.Assign(ref content, "txtGroupName_val", strGroupName);
        

        object fdate = null, tdate = null;
        int hasDate = CastAide.AsInt32(Request["hdchkDate"], 0);

        if (hasDate>0)
        {
             fdate = CastAide.AsDateTime(Request["fdate"], null);
             tdate = CastAide.AsDateTime(Request["tdate"], null);
        }
        TemplateHandler.ReplaceBlock(ref content, "Content", GenerateHTML.GroupSetting.GetGroupSettingList(strGroupName,fdate,tdate,currPage, ref minNo, ref maxNo, ref totalRows, currPageSize,ref totalpage));
               
        TemplateHandler.Assign(ref content, "hdchkDate_val", hasDate.ToString());
        TemplateHandler.Assign(ref content, "hdfDate_val",CastAide.AsDateTime(fdate,DateTime.Now).ToShortDateString());
        TemplateHandler.Assign(ref content, "hdtDate_val", CastAide.AsDateTime(tdate, DateTime.Now).ToShortDateString());
        TemplateHandler.Assign(ref content, "hdTotalRows_val", totalRows);

        TemplateHandler.Assign(ref content, "minNo", minNo.ToString());
        TemplateHandler.Assign(ref content, "maxNo", maxNo.ToString());
        TemplateHandler.Assign(ref content, "page_val", currPage);


        TemplateHandler.Assign(ref content, "submenu", SubMenu.GenSubMenu(SubMenu.AffLink, SubMenu.AffText, 6, totalRows, Resources.Lang.CommissionGroup, "i_commg.png"));
        
        if (totalRows != 0)
        {
            TemplateHandler.Assign(ref content, "paging-top", Common.GenPaging(totalpage, currPage,currPageSize, maxNo, "aPageTop", "txtPageTop", new string[] { "Add New Group" }, new string[] { "AddNewGroup();" }, 1020, false, true, true, false));
            TemplateHandler.Assign(ref content, "paging-bottom", Common.GenPaging(totalpage, currPage, currPageSize, maxNo, "aPageBottom", "txtPageBottom", 1020, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref content, "paging-top", "");
            TemplateHandler.Assign(ref content, "paging-bottom", "");
        }
        this.HtmlContent = content;
    }

    
}
