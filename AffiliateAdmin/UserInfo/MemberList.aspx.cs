using System;
using System.Collections;
using System.Data;
using System.Text;
using DNAide.Core;
using DNAide.Web;

public partial class UserInfo_MemberList : IBCPage
{
    string strTemplate = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        strTemplate = TemplateHandler.LoadTemplate("UserInfo/MemberList.htm");

        AssignText();

        //Search content
        DateTime dtmTmp = DateTime.Now.AddDays(-(DateTime.Now.Day - 1));
        DateTime dtmFDate = CastAide.AsDateTime(Request["fdate"], dtmTmp);
        DateTime dtmTDate = CastAide.AsDateTime(Request["tdate"], DateTime.Now);
        int intMemberType = CastAide.AsInt32(Request["slType"], 0);
        bool isGetAll = (CastAide.AsString(Request["isGetAll"], string.Empty) == "ic-check" ? true : false);

        TemplateHandler.Assign(ref strTemplate, "cssgetall", isGetAll ? "ic-check" : "ic-uncheck");
        TemplateHandler.Assign(ref strTemplate, "fdate", dtmFDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTemplate, "tdate", dtmTDate.ToString("MM/dd/yyyy"));
        TemplateHandler.Assign(ref strTemplate, "type-val", intMemberType);

        LoadData(dtmFDate, dtmTDate, intMemberType, isGetAll);
        //Load submenu
        TemplateHandler.Assign(ref strTemplate, "submenu", SubMenu.GenSubMenu(SubMenu.AffApprovedLink, SubMenu.AffApprovedText, 6, 0, SubMenu.AffApprovedText[6], "i_customer.png"));

        this.HtmlContent = strTemplate;
    }

    void AssignText()
    {
        TemplateHandler.Assign(ref strTemplate, "membertype", Resources.Lang.Member);
        TemplateHandler.Assign(ref strTemplate, "select", Resources.Lang.Select);
        TemplateHandler.Assign(ref strTemplate, "deposit", Resources.Lang.Deposited);
        TemplateHandler.Assign(ref strTemplate, "nondeposit", Resources.Lang.NonDeposited);
        TemplateHandler.Assign(ref strTemplate, "fdate-txt", Resources.Lang.From);
        TemplateHandler.Assign(ref strTemplate, "tdate-txt", Resources.Lang.To);
        TemplateHandler.Assign(ref strTemplate, "username", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTemplate, "fname", Resources.Lang.FirstName);
        TemplateHandler.Assign(ref strTemplate, "lname", Resources.Lang.LastName);
        TemplateHandler.Assign(ref strTemplate, "email", Resources.Lang.Email);
        TemplateHandler.Assign(ref strTemplate, "registerdate", Resources.Lang.registerdate);
        TemplateHandler.Assign(ref strTemplate, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTemplate, "searchby", Resources.Lang.SearchBy);
        TemplateHandler.Assign(ref strTemplate, "themepath", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref strTemplate, "over", Common.RowBgOver);
    }

    void LoadData(DateTime dtmFDate, DateTime dtmTDate, int intMemberType, bool isGetAll)
    {
        int width = 700;
        int intTotalPages = 0;
        int intPage = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);

        var param = string.IsNullOrEmpty(Request["affId"]) ? AppSettings.Session.AffId.ToString() : Request["affId"];
        int intCustid = int.Parse(param);

        TemplateHandler.Assign(ref strTemplate, "width", width);
        TemplateHandler.Assign(ref strTemplate, "page", intPage);

        DataTable dtbCurrency = DataHandler.ExchangeRate.Exchange_Sel(DateTime.Now);

        Hashtable hsCurrency = ConvertDataTableToHashTable(dtbCurrency, "ExchangeId", "Currency");

        DataTable dt = DataHandler.Member.SelDepositMemInfo(intCustid, dtmFDate, dtmTDate, intMemberType, isGetAll, intPage, intPageSize, ref intTotalPages);
        int intNumMem = dt.Rows.Count;
        StringBuilder strBuild = new StringBuilder();
        if (intNumMem > 0)
        {
            int intcount = (intPage - 1) * intPageSize;
            string strContent = TemplateHandler.CopyBlock(strTemplate, "content");
            foreach (DataRow dr in dt.Rows)
            {
                intcount++;
                string strTmp = strContent;
                TemplateHandler.Assign(ref strTmp, "color", (intcount % 2 == 0) ? Common.RowBgEven : Common.RowBgOdd);
                TemplateHandler.Assign(ref strTmp, "no", intcount);
                TemplateHandler.Assign(ref strTmp, "username-val", dr["username"]);
                TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"]);
                TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"]);
                TemplateHandler.Assign(ref strTmp, "email-val", dr["email"]);
                TemplateHandler.Assign(ref strTmp, "currency-val", hsCurrency[dr["currency"].ToString()]);
                TemplateHandler.Assign(ref strTmp, "registerdate-val", dr["registereddate"]);
                strBuild.Append(strTmp);
            }
            TemplateHandler.Assign(ref strTemplate, "paging-top", Common.GenPaging(intTotalPages, intPage, intPageSize, 0, "aPageTop", "txtPageTop", width, false, true));
            TemplateHandler.Assign(ref strTemplate, "paging-bottom", Common.GenPaging(intTotalPages, intPage, intPageSize, 0, "aPageBottom", "txtPageBottom", width, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref strTemplate, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTemplate, "paging-bottom", string.Empty);
            strBuild.Append(Common.NoInfo(7));
        }
        TemplateHandler.ReplaceBlock(ref strTemplate, "content", strBuild.ToString());
    }

    Hashtable ConvertDataTableToHashTable(DataTable dtIn, string keyField, string valueField)
    {
        Hashtable htOut = new Hashtable();
        foreach (DataRow drIn in dtIn.Rows)
        {
            htOut.Add(drIn[keyField].ToString(), drIn[valueField].ToString());
        }
        return htOut;
    }

}
