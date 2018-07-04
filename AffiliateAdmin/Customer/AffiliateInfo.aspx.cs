using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using AffiliateAdmin.Business.DataAccess;
using DNAide.Core;
using DNAide.Utils;
using DNAide.Web;

public partial class Customer_AffiliateInfo : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strTpl = TemplateHandler.LoadTemplate("Customer/AffiliateInfo.htm");
        TemplateHandler.Assign(ref strTpl, "XsrfTokenFormValue", _XsrfTokenFormValue);
        TemplateHandler.Assign(ref strTpl, "affiliate", Resources.Lang.Affiliate);
        TemplateHandler.Assign(ref strTpl, "fname", Resources.Lang.Fname);
        TemplateHandler.Assign(ref strTpl, "lname", Resources.Lang.Lname);
        TemplateHandler.Assign(ref strTpl, "gender", Resources.Lang.Gender);
        TemplateHandler.Assign(ref strTpl, "number", Resources.Lang.ContactNumber);
        TemplateHandler.Assign(ref strTpl, "mobile", Resources.Lang.Mobile);
        TemplateHandler.Assign(ref strTpl, "language", Resources.Lang.Language);
        TemplateHandler.Assign(ref strTpl, "siteurl", Resources.Lang.SiteUrl);
        TemplateHandler.Assign(ref strTpl, "lastlogin", Resources.Lang.LastLogin);
        TemplateHandler.Assign(ref strTpl, "lastloginip", Resources.Lang.LastLoginIP);
        TemplateHandler.Assign(ref strTpl, "pass", Resources.Lang.Password);
        TemplateHandler.Assign(ref strTpl, "change", Resources.Lang.Change);
        TemplateHandler.Assign(ref strTpl, "CommGroup", Resources.Lang.CommGroup);
        TemplateHandler.Assign(ref strTpl, "comm", Resources.Lang.Comm);

        TemplateHandler.Assign(ref strTpl, "balforward", Resources.Lang.BalanceForward);
        TemplateHandler.Assign(ref strTpl, "setting", Resources.Lang.Setting);
        TemplateHandler.Assign(ref strTpl, "status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "closed", Resources.Lang.Closed);
        TemplateHandler.Assign(ref strTpl, "currlist", Resources.Lang.CurrOnlineList);
        TemplateHandler.Assign(ref strTpl, "ip", Resources.Lang.IP);
        TemplateHandler.Assign(ref strTpl, "lastcheckin", Resources.Lang.LastCheckIn);
        TemplateHandler.Assign(ref strTpl, "reportdetail", Resources.Lang.ReportDetails);
        TemplateHandler.Assign(ref strTpl, "memberlist", Resources.Lang.MemList);

        TemplateHandler.Assign(ref strTpl, "afftype", Resources.Lang.AffiliateType);

        string strUsername = Server.UrlEncode(Request["username"]);
        if (!string.IsNullOrEmpty(strUsername))
            AppSettings.Session.AffUsername = strUsername.Trim();

        if (string.IsNullOrEmpty(strUsername) && string.IsNullOrEmpty(AppSettings.Session.AffUsername))
        {
            TemplateHandler.Assign(ref strTpl, "username", string.Empty);
            TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", string.Empty);
            TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
            TemplateHandler.Assign(ref strTpl, "isReload-val", string.Empty);
            TemplateHandler.Assign(ref strTpl, "togshow", "none");
            this.HtmlContent = strTpl;
            return;
        }

        TemplateHandler.Assign(ref strTpl, "username", AppSettings.Session.AffUsername);
        TemplateHandler.Assign(ref strTpl, "pageid-val", Request["pageid"]);

        if (!string.IsNullOrEmpty(Request["isReload"]) && string.Compare(Request["isReload"], "0", true) != 0)//proccess refresh page
        {
            TemplateHandler.Assign(ref strTpl, "noinfo-val", "2");
            TemplateHandler.Assign(ref strTpl, "isReload-val", Request["isReload"]);
        }
        TemplateHandler.Assign(ref strTpl, "isReload-val", "0");

        DbResult result = DataHandler.Affiliate.SelAffInfo_LeftPanel(AppSettings.Session.AffUsername);
        if (result.ResultSet.Tables.Count > 0)
        {
            DataTable dtbInfo = result.ResultSet.Tables[0];
            if (dtbInfo.Rows.Count > 0)
            {
                DataRow dr = dtbInfo.Rows[0];

                AppSettings.Session.AffType = (AffType)CastAide.AsInt32(dr["afftype"], 2);
                TemplateHandler.Assign(ref strTpl, "afftype-val", AppSettings.Session.AffType.ToString());

                TemplateHandler.Assign(ref strTpl, "url", GerUrl(AppSettings.Session.AffUsername));//Link to affiliate
                TemplateHandler.Assign(ref strTpl, "noinfo-val", "0");

                string strStatus = dr["Status"].ToString();
                bool isClosed = CastAide.AsBoolean(dr["Closed"]);

                string _displayURL = dr["siteurl"].ToString().Replace("http://", string.Empty).Replace("www.", string.Empty);
                _displayURL = (_displayURL.Length <= 23) ? _displayURL : _displayURL.Substring(0, 20) + "...";

                AppSettings.Session.AffId = CastAide.AsInt32(dr["affid"]);
                AppSettings.Session.IsCasino = CastAide.AsBoolean(result.Outputs["iscasino"]);

                TemplateHandler.Assign(ref strTpl, "custid-val", dr["affid"]);
                TemplateHandler.Assign(ref strTpl, "fnametext-val", cutLongText(dr["firstname"].ToString(), 23));
                TemplateHandler.Assign(ref strTpl, "fname-val", dr["firstname"]);
                TemplateHandler.Assign(ref strTpl, "lname-val", cutLongText(dr["lastname"].ToString(), 23));
                if (DBNull.Value.Equals(dr["gender"]))
                    TemplateHandler.Assign(ref strTpl, "gender-val", string.Empty);
                else
                    TemplateHandler.Assign(ref strTpl, "gender-val", (CastAide.AsBoolean(dr["gender"], true) ? Resources.Lang.Male : Resources.Lang.Female));
                TemplateHandler.Assign(ref strTpl, "number-val", cutLongText(dr["phone"].ToString(), 23));
                TemplateHandler.Assign(ref strTpl, "mobile-val", cutLongText(dr["mobilephone"].ToString(), 23));
                TemplateHandler.Assign(ref strTpl, "email-val", dr["email"]);
                TemplateHandler.Assign(ref strTpl, "language-val", Common.GerFullTextLang(dr["preferredlanguage"]));
                TemplateHandler.Assign(ref strTpl, "preferredpayment-val", dr["preferredpayment"]);
                TemplateHandler.Assign(ref strTpl, "player_username-val", dr["player_username"]);
                TemplateHandler.Assign(ref strTpl, "lang-val", dr["preferredlanguage"]);
                TemplateHandler.Assign(ref strTpl, "siteurlorg-val", _displayURL);
                TemplateHandler.Assign(ref strTpl, "mailoption-val", (CastAide.AsBoolean(dr["mailoption"], true) ? "1" : "0"));
                if (dr["siteurl"].ToString().IndexOf("http://") >= 0)
                {
                    TemplateHandler.Assign(ref strTpl, "siteurl-val", dr["siteurl"]);
                    TemplateHandler.Assign(ref strTpl, "WebsiteCheckerURL-val", Common.GetWebsiteCheckerURL(dr["siteurl"].ToString()));
                }
                else
                {
                    TemplateHandler.Assign(ref strTpl, "siteurl-val", "http://" + dr["siteurl"].ToString());
                    TemplateHandler.Assign(ref strTpl, "WebsiteCheckerURL-val", Common.GetWebsiteCheckerURL("http://" + dr["siteurl"].ToString()));
                }
                TemplateHandler.Assign(ref strTpl, "lastlogin-val", Common.IBCDateTimeFormat(dr["lastLoginDate"], DTFormatType.DT24h));
                TemplateHandler.Assign(ref strTpl, "lastloginip-val", dr["lastloginip"]);
                TemplateHandler.Assign(ref strTpl, "LastLoginIPCheckerURL-val", Common.GetIPCheckerURL(dr["lastloginip"].ToString()));
                TemplateHandler.Assign(ref strTpl, "CommGroup-val", dr["groupname"].ToString());
                TemplateHandler.Assign(ref strTpl, "GroupID-val", dr["groupid"]);

                if (Convert.ToInt32(dr["groupid"]) > 1000)
                {
                    TemplateHandler.RemoveBlock(ref strTpl, "PublicGroup");
                }
                else
                {
                    string groupName = dr["groupname"].ToString();
                    groupName = (groupName.Length <= 14) ? groupName : groupName.Substring(0, 11) + "...";
                    TemplateHandler.Assign(ref strTpl, "CommGroup1-val", groupName);
                    TemplateHandler.RemoveBlock(ref strTpl, "PrivateGroup");
                    if (isClosed)
                        TemplateHandler.ReplaceBlock(ref strTpl, "PublicGroup", groupName);
                }

                TemplateHandler.Assign(ref strTpl, "balforward-val", Common.FormatNumber(dr["balbroughtforward"]));

                if (string.Compare(strStatus, Resources.Lang.Approved, true) != 0)
                {
                    TemplateHandler.Assign(ref strTpl, "spUN", string.Empty);
                    TemplateHandler.Assign(ref strTpl, "lnkUN", "none");
                    TemplateHandler.ReplaceBlock(ref strTpl, "closed", string.Empty);
                    TemplateHandler.ReplaceBlock(ref strTpl, "commission", string.Empty);
                    TemplateHandler.ReplaceBlock(ref strTpl, "onlinelist", string.Empty);
                    TemplateHandler.ReplaceBlock(ref strTpl, "Password", string.Empty);
                    TemplateHandler.RemoveBlock(ref strTpl, "MemberList");
                }
                else
                {
                    TemplateHandler.ReplaceBlock(ref strTpl, "status-val", strStatus);
                    if (isClosed)
                    {
                        TemplateHandler.Assign(ref strTpl, "spUN", string.Empty);
                        TemplateHandler.Assign(ref strTpl, "lnkUN", "none");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref strTpl, "spUN", "none");
                        TemplateHandler.Assign(ref strTpl, "lnkUN", string.Empty);
                    }
                }
                TemplateHandler.Assign(ref strTpl, "status-val", strStatus);
                if (isClosed)
                {
                    TemplateHandler.Assign(ref strTpl, "closed-val", Resources.Lang.Yes);
                }
                else
                {
                    TemplateHandler.Assign(ref strTpl, "closed-val", Resources.Lang.No);
                }
            }
            else
            {
                TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", Resources.Lang.NoUser);
                TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
                TemplateHandler.Assign(ref strTpl, "togshow", "none");
            }
            DataTable dtbOnline = result.ResultSet.Tables[1];
            if (dtbOnline.Rows.Count > 0)
            {
                int rowNo = 0;
                string strOnlineContent = TemplateHandler.CopyBlock(strTpl, "currlist");
                StringBuilder strBuild = new StringBuilder();
                foreach (DataRow dr in dtbOnline.Rows)
                {
                    rowNo++;
                    string strTmp = strOnlineContent;
                    TemplateHandler.Assign(ref strTmp, "no", rowNo);
                    TemplateHandler.Assign(ref strTmp, "ip-val", dr["ip"]);
                    TemplateHandler.Assign(ref strTmp, "IPCheckerURL-val", Common.GetIPCheckerURL(dr["ip"].ToString()));
                    TemplateHandler.Assign(ref strTmp, "time", Common.IBCDateTimeFormat(dr["reload_time"], DTFormatType.DT24h));
                    strBuild.Append(strTmp);
                }
                TemplateHandler.ReplaceBlock(ref strTpl, "currlist", strBuild.ToString());
            }
            else
                TemplateHandler.ReplaceBlock(ref strTpl, "currlist", "<tr><td colspan='3' class='c'>" + Resources.Lang.ErrUserOnline + "</td></tr>");
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "noinfo", Resources.Lang.NoUser);
            TemplateHandler.Assign(ref strTpl, "noinfo-val", "1");
            TemplateHandler.Assign(ref strTpl, "togshow", "none");
        }
        TemplateHandler.Assign(ref strTpl, "isAdmin", "0");

        this.HtmlContent = strTpl;
    }

    private string GerUrl(string custName)
    {
        string rndStr = Encryption.RandomString(6);
        string accKey = GenerateSHA512String(Session.SessionID + rndStr);
        string accID = AppSettings.Session.CustID.ToString();
        string accOnId = AppSettings.Session.OnUser.ToString();
        string _url = AppSettings.Session.AffType == AffType.European ? AppSettings.InternalEuropeanAffiliateURL : AppSettings.InternalAsianAffiliateURL;
        _url += "?chalengeStr=" + rndStr;
        _url += "&accKey=" + accKey;
        _url += "&custName=" + custName;
        _url += "&accId=" + AppSettings.Session.CustID.ToString(); ;
        _url += "&accOnId=" + accOnId;
        return _url;
    }

    private string cutLongText(string val, int maxLength)
    {
        return (val.Length <= maxLength) ? val : "<span title='" + Server.HtmlEncode(val) + "'>" + val.Substring(0, maxLength - 3) + "...</span>";
    }

    public static string GenerateSHA512String(string inputString)
    {
        var sha512 = SHA512.Create();
        var bytes = Encoding.UTF8.GetBytes(inputString);
        var hash = sha512.ComputeHash(bytes);

        return GetStringFromHash(hash);
    }

    private static string GetStringFromHash(byte[] hash)
    {
        var result = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }

        return result.ToString();
    }
}