using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using AffiliateUser;
using DNAide.Core;
using DNAide.Web;


public partial class UserInfo_Customers : RootPageLogIn
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AppSettings.Session.CommGroupName != "Korean Agent Comm")
        {
            Response.Redirect("~/Root.aspx");
        }
        ltrStaHis.Text = Resources.Lang.YourCustomers;

        string strTpl = TemplateHandler.LoadTemplate("UserInfo/Customers.htm");
        #region load language
        TemplateHandler.Assign(ref strTpl, "UserName", Resources.Lang.Username);
        TemplateHandler.Assign(ref strTpl, "FName", Resources.Lang.FName);
        TemplateHandler.Assign(ref strTpl, "LName", Resources.Lang.LName);
        TemplateHandler.Assign(ref strTpl, "Email", Resources.Lang.Email);
        TemplateHandler.Assign(ref strTpl, "MobileNumber", Resources.Lang.MobileNumber);
        TemplateHandler.Assign(ref strTpl, "RegisteredDate", Resources.Lang.RegisteredDate);
        TemplateHandler.Assign(ref strTpl, "FirstDepositDate", Resources.Lang.FirstDepositDate);

        TemplateHandler.Assign(ref strTpl, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref strTpl, "To", Resources.Lang.To);
        TemplateHandler.Assign(ref strTpl, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref strTpl, "All", Resources.Lang.All);
        TemplateHandler.Assign(ref strTpl, "Deposit", Resources.Lang.Deposit);
        #endregion

        var currentCulture = Thread.CurrentThread.CurrentCulture;

        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        var getall = CastAide.AsBoolean(Request["getall"], false);
        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];

        if (dtmFrom == null || dtmTo == null)
        {            
            DateTime now = DateTime.Now;
            dtmFrom = new DateTime(now.Year, now.Month, 1);
            dtmTo = now;
            getall = true;
        }
                
        TemplateHandler.Assign(ref strTpl, "fdate", Common.ConvertDateTime(dtmFrom, "/", true, false));
        TemplateHandler.Assign(ref strTpl, "tdate", Common.ConvertDateTime(dtmTo, "/", true, false));


        
        var deposit = CastAide.AsInt32(Request["deposit"],0);

        TemplateHandler.Assign(ref strTpl, "depositlist", GenDepositOptions(deposit));
        TemplateHandler.Assign(ref strTpl, "getall", getall ?"checked": null);                
        
        int intPageIndex = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 20);

        int intTotalPage = 0;
        int affID = AppSettings.Session.CustID;
        

        var result = DataHandler.MemberInfo.SelDepositMemInfo(affID, 
            getall ? null : dtmFrom,
            getall ? null : dtmTo, deposit, intPageIndex, intPageSize, ref intTotalPage);
        
       // var regex = new Regex(@"12bet\$", RegexOptions.IgnoreCase);

        
        if (result!=null && result.ResultSet.Tables.Count > 0 && result.ResultSet.Tables[0].Rows.Count>0)
        {

            var usernames = new StringBuilder();
            DataTable dtb = result.ResultSet.Tables[0];
            foreach (DataRow item in dtb.Rows)
            {
                usernames.Append(item["username"]).Append(",");
            }
            var tb = DataHandler.MemberInfo.SelCreatedDates(usernames.ToString());
            tb.PrimaryKey = new []{tb.Columns["username"]};

            
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //decimal dcTotal = 0;
            StringBuilder strBuild = new StringBuilder();
            int rowNo = (intPageIndex - 1) * intPageSize;
            bool isCheckAll = false;

            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite
            foreach (DataRow dr in dtb.Rows)
            {
                rowNo++;
                string strTmp = strContent;
                //dcTotal += CastAide.AsDecimal(dr["amount"]);
                
                strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                strMouseStyle = (strMouseStyle == "b") ? "a" : "b";

                string username = CastAide.AsString(dr["UserName"]);
               // username = regex.Replace(username, "");

                var row = tb.Rows.Find(username);
                var createddate = row!=null?row["createddate"]: null;

                TemplateHandler.Assign(ref strTmp, "no", rowNo);
                TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);

                TemplateHandler.Assign(ref strTmp, "username-val", username);
                TemplateHandler.Assign(ref strTmp, "fname-val", dr["firstname"]);
                TemplateHandler.Assign(ref strTmp, "lname-val", dr["lastname"]);
                TemplateHandler.Assign(ref strTmp, "email-val", dr["email"]);
                TemplateHandler.Assign(ref strTmp, "mobilenumber-val", dr["phone"]);                
                TemplateHandler.Assign(ref strTmp, "registereddate-val", dr["RegisteredDate"]);
                TemplateHandler.Assign(ref strTmp, "firstdepositdate-val", createddate);                

                strBuild.Append(strTmp);
            }
            TemplateHandler.Assign(ref strTpl, "page", intPageIndex);
            TemplateHandler.Assign(ref strTpl, "paging-top", Common.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref strTpl, "paging-bottom", Common.GenPaging(intTotalPage, intPageIndex, intPageSize, rowNo, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            TemplateHandler.Assign(ref strTpl, "disabled", isCheckAll ? "" : "disabled");
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(8));
            TemplateHandler.Assign(ref strTpl, "paging-top", string.Empty);
            TemplateHandler.Assign(ref strTpl, "paging-bottom", string.Empty);
        }
        Thread.CurrentThread.CurrentCulture = currentCulture;
        TemplateHandler.Assign(ref strTpl, "PageSize-val", intPageSize);
        ltrContent.Text = strTpl;

    }

    string GenDepositOptions(int value)
    {
        var s = new StringBuilder();        
        s.AppendFormat("<option value='0' {0}>{1}</option>",(value==0?"selected":null), Resources.Lang.All);
        s.AppendFormat("<option value='1' {0}>{1}</option>", (value == 1 ? "selected" : null), Resources.Lang.Non_Deposit);
        s.AppendFormat("<option value='2' {0}>{1}</option>", (value == 2 ? "selected" : null), Resources.Lang.Deposit);        

        return s.ToString();
    }
}


