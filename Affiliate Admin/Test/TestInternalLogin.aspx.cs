using System;
using System.Web.Security;
using DNAide.Utils;

public partial class Test_TestInternalLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rndStr = Encryption.RandomString(6);
        string accKey = FormsAuthentication.HashPasswordForStoringInConfigFile(Session.SessionID + rndStr, "MD5");
        string custName = Request["username"];
        string accID = AppSettings.Session.CustID.ToString();
        string accOnId = AppSettings.Session.OnUser.ToString();
        string _url = AppSettings.InternalURL;
        _url += "?chalengeStr=" + rndStr;
        _url += "&accKey=" + accKey;
        _url += "&custName=" + custName;
        _url += "&accId=" + AppSettings.Session.CustID.ToString(); ;
        _url += "&accOnId=" + accOnId;
        Response.Redirect(_url);
    }
}
