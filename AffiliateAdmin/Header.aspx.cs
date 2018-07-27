using System;
using System.Text;
using System.Web;
using AffiliateAdmin.Business.Constants;
using DNAide.Web;
public partial class Header : IBCPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string temp = TemplateHandler.LoadTemplate("Header.htm");
        TemplateHandler.Assign(ref temp, "Logout", Resources.Lang.Logout);
        TemplateHandler.Assign(ref temp, "resetpass", Resources.Lang.ChangeMyPwd);
        TemplateHandler.Assign(ref temp, "AffiliateAdministration", Resources.Lang.AffiliateAdministration);
        TemplateHandler.Assign(ref temp, "Home", Resources.Lang.Home);
        TemplateHandler.Assign(ref temp, "customer", Resources.Lang.Customer);
        TemplateHandler.Assign(ref temp, "Affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref temp, "member", Resources.Lang.Member);
        TemplateHandler.Assign(ref temp, "Payment", Resources.Lang.PaymentRequest);
        TemplateHandler.Assign(ref temp, "MediaGallery", Resources.Lang.MediaGallery);
        TemplateHandler.Assign(ref temp, "User", Resources.Lang.Administrator);
        TemplateHandler.Assign(ref temp, "exchangerate", Resources.Lang.ExchangeRate);
        TemplateHandler.Assign(ref temp, "report", Resources.Lang.Report);
        TemplateHandler.Assign(ref temp, "Message", Resources.Lang.Message);
        TemplateHandler.Assign(ref temp, "EmailHistory", Resources.Lang.EmailHistory);
        TemplateHandler.Assign(ref temp, "Theme_Dir", AppSettings.ThemeDir);
        TemplateHandler.Assign(ref temp, "Welcome", Resources.Lang.Welcome);
        TemplateHandler.Assign(ref temp, "CommGroup", Resources.Lang.CommGroup);
        TemplateHandler.Assign(ref temp, "Domain", Resources.Lang.Domain);
        string gmt;
        DateTime dt = DataHandler.ServerDateTime.GetCurrentDBTime(out gmt);
        TemplateHandler.Assign(ref temp, "gmt_offset", gmt);
        TemplateHandler.Assign(ref temp, "year", dt.Year);
        TemplateHandler.Assign(ref temp, "month", dt.Month);
        TemplateHandler.Assign(ref temp, "day", dt.Day);
        TemplateHandler.Assign(ref temp, "hour", dt.Hour);
        TemplateHandler.Assign(ref temp, "minute", dt.Minute);
        TemplateHandler.Assign(ref temp, "second", dt.Second);

        if (!Common.HasReadPermission(PermissionNames.Domain))
            TemplateHandler.RemoveBlock(ref temp, "domain");


        if (AppSettings.Session.RoleID == -1)
        {
            TemplateHandler.RemoveBlock(ref temp, "limit-logout");
            Response.Write("<script>parent.location.href='Signin.aspx';</script>");
        }
        else
        {
            TemplateHandler.Assign(ref temp, "username", AppSettings.Session.UserName);
        }
        this.HtmlContent = temp + GerScriptMsg();
    }
    string GerScriptMsg()
    {
        StringBuilder strScript = new StringBuilder();
        strScript.AppendLine("<script type=\"text/javascript\">");
        strScript.AppendLine("var Msg = {");
        strScript.AppendLine("CreateAgent:\"" + Resources.Lang.Err100 + "\",");
        strScript.AppendLine("ContactNumber:\"" + Resources.Lang.ErrContactNumber + "\",");
        strScript.AppendLine("ContactApp:\"" + Resources.Lang.RequireIMAccount + "\",");
        strScript.AppendLine("FName:\"" + Resources.Lang.ErrFName + "\",");
        strScript.AppendLine("LName:\"" + Resources.Lang.ErrLName + "\",");
        strScript.AppendLine("ErrGender:\"" + Resources.Lang.ErrGender + "\",");
        strScript.AppendLine("Email:\"" + Resources.Lang.ErrEmail + "\",");
        strScript.AppendLine("EmailValid:\"" + Resources.Lang.ErrEmailValid + "\",");
        strScript.AppendLine("Address:\"" + Resources.Lang.ErrAddress + "\",");
        strScript.AppendLine("City:\"" + Resources.Lang.ErrCity + "\",");
        strScript.AppendLine("State:\"" + Resources.Lang.ErrState + "\",");
        strScript.AppendLine("PostCode:\"" + Resources.Lang.ErrPostCode + "\",");
        strScript.AppendLine("Country:\"" + Resources.Lang.ErrCountry + "\",");
        strScript.AppendLine("SiteUrl:\"" + Resources.Lang.ErrSiteUrl + "\",");
        strScript.AppendLine("PreferLang:\"" + Resources.Lang.ErrPreferLang + "\",");
        strScript.AppendLine("UserName12Bet:\"" + Resources.Lang.ErrUserName12Bet + "\",");
        strScript.AppendLine("Neteller:\"" + Resources.Lang.ErrNeteller + "\",");
        strScript.AppendLine("Moneybooker:\"" + Resources.Lang.ErrMoneybooker + "\",");
        strScript.AppendLine("MoneybookerValid:\"" + Resources.Lang.ErrMoneybookerValid + "\",");
        strScript.AppendLine("BankName:\"" + Resources.Lang.ErrBankName + "\",");
        strScript.AppendLine("BankAddress:\"" + Resources.Lang.ErrBankAddress + "\",");
        strScript.AppendLine("BankCode:\"" + Resources.Lang.ErrBankCode + "\",");
        strScript.AppendLine("BankAccName:\"" + Resources.Lang.ErrBankAccName + "\",");
        strScript.AppendLine("BankAccNumber:\"" + Resources.Lang.ErrBankAccNumber + "\",");
        strScript.AppendLine("ChequeFullname:\"" + Resources.Lang.ErrChequeFullname + "\",");
        strScript.AppendLine("ChequeAddress:\"" + Resources.Lang.ErrChequeAddress + "\",");
        strScript.AppendLine("ChequeCity:\"" + Resources.Lang.ErrChequeCity + "\",");
        strScript.AppendLine("ChequeState:\"" + Resources.Lang.ErrChequeState + "\",");
        strScript.AppendLine("ChequePostcode:\"" + Resources.Lang.ErrChequePostcode + "\",");
        strScript.AppendLine("NewPass:\"" + Resources.Lang.ErrNewPass + "\",");
        strScript.AppendLine("RePass:\"" + Resources.Lang.ErrRePass + "\",");
        strScript.AppendLine("Name:\"" + Resources.Lang.ErrName + "\",");
        strScript.AppendLine("InvalidData:\"" + Resources.Lang.ErrInvalidData + "\",");
        strScript.AppendLine("NoteEmpty:\"" + Resources.Lang.ErrNoteEmpty + "\",");
        strScript.AppendLine("NoteAdd:\"" + Resources.Lang.ErrNoteAdd + "\",");
        strScript.AppendLine("UpdateSucc:\"" + Resources.Lang.ErrUpdateSucc + "\",");
        strScript.AppendLine("NoteConfirm:\"" + Resources.Lang.ErrNoteConfirm + "\",");
        strScript.AppendLine("NoteDel:\"" + Resources.Lang.ErrNoteDel + "\",");
        strScript.AppendLine("ChooseItem:\"" + Resources.Lang.ErrChooseItem + "\",");
        strScript.AppendLine("Department:\"" + Resources.Lang.ErrDepartment + "\",");
        strScript.AppendLine("Username:\"" + Resources.Lang.ErrUsername + "\",");
        strScript.AppendLine("PasswordRequired:\"" + Resources.Lang.ErrPasswordRequired + "\",");
        strScript.AppendLine("DepartmentChoose:\"" + Resources.Lang.ErrDepartmentChoose + "\",");
        strScript.AppendLine("Character:\"" + Resources.Lang.ErrCharacter + "\",");
        strScript.AppendLine("ErrDate:\"" + Resources.Lang.ErrDate + "\",");
        strScript.AppendLine("Banner:\"" + Resources.Lang.ErrBanner + "\",");
        strScript.AppendLine("Rate:\"" + Resources.Lang.ErrRate + "\",");
        strScript.AppendLine("RateRequire:\"" + Resources.Lang.ErrRateRequire + "\",");
        strScript.AppendLine("ErrDepartment:\"" + Resources.Lang.ErrDepartment + "\",");
        strScript.AppendLine("MonthlyStatus:\"" + Resources.Lang.Err30011 + "\",");
        strScript.AppendLine("AffDomain:\"" + Resources.Lang.ErrAffDomain + "\",");
        strScript.AppendLine("_12BetDomain:\"" + Resources.Lang.Err12BetDomain + "\"");
        strScript.AppendLine("};</script>");
        return strScript.ToString();
    }
}
