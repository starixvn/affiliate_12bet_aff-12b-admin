using System;
using DNAide.Web;
using DNAide.Core;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Xml.Linq;


public partial class MemDepositInfo : RootPageLogIn
{    
    protected void Page_Load(object sender, EventArgs e)
    {        

        string strTpl = TemplateHandler.LoadTemplate("UserInfo/MemDepositInfo.htm");
        #region load language
        TemplateHandler.Assign(ref strTpl, "total", Resources.Lang.Total);
        TemplateHandler.Assign(ref strTpl, "transactionid", Resources.Lang.TransactionID);
        TemplateHandler.Assign(ref strTpl, "date", Resources.Lang.Date);
        TemplateHandler.Assign(ref strTpl, "status", Resources.Lang.Status);
        TemplateHandler.Assign(ref strTpl, "accountnumber", Resources.Lang.AccountNumber);
        TemplateHandler.Assign(ref strTpl, "bankname", Resources.Lang.BankName);
        TemplateHandler.Assign(ref strTpl, "bankaddress", Resources.Lang.BankAddress);
        TemplateHandler.Assign(ref strTpl, "bankcountry", Resources.Lang.BankCountry);
        TemplateHandler.Assign(ref strTpl, "bankcity", Resources.Lang.BankCity);
        TemplateHandler.Assign(ref strTpl, "currency", Resources.Lang.Currency);
        TemplateHandler.Assign(ref strTpl, "original", Resources.Lang.Original);
        TemplateHandler.Assign(ref strTpl, "usdeqv", Resources.Lang.USDEqv);

        

        #endregion
        string strStatus = Request["status"];
        var user = CastAide.AsString(Request["user"], String.Empty);
        var date = CastAide.AsDateTime(Request["date"], DateTime.Now);
        var exrate = CastAide.AsDouble(Request["rate"], 1);
        var isdeposit = CastAide.AsDouble(Request["isdeposit"], 1) == 1;

        TemplateHandler.Assign(ref strTpl, "MemDepositInfo", isdeposit ? Resources.Lang.MemDepositInfo : Resources.Lang.MemberWithdrawalInfo);

        TemplateHandler.Assign(ref strTpl, "date-top", date.ToShortDateString());

        var service = new MemberInfoService.Service();
        // test
       // date = new DateTime(2013, 6, 4);       
        var xmlNode = isdeposit ? service.DepositInfo(user, date.ToShortDateString())
                    : service.WithdrawalInfo(user, date.ToShortDateString());
        
        var xml = XElement.Parse(xmlNode.OuterXml);

        if (xml.Element("ReturnCode").Value == "0")
        {
            
            string strContent = TemplateHandler.CopyBlock(strTpl, "content");
            //decimal dcTotal = 0;
            StringBuilder strBuild = new StringBuilder();
            

            string strRowStyle = "bgcgy";//bgwhite
            string strMouseStyle = "b";//bgwhite
            var total = 0d;
            foreach (var x in xml.Elements((isdeposit ? "DepositInfo": "WithdrawalInfo")))
            {
                
                string strTmp = strContent;
                //dcTotal += CastAide.AsDecimal(dr["amount"]);

                strRowStyle = (strRowStyle == "bgcgy") ? "bgwhite" : "bgcgy";
                strMouseStyle = (strMouseStyle == "b") ? "a" : "b";

                TemplateHandler.Assign(ref strTmp, "style", strRowStyle);
                TemplateHandler.Assign(ref strTmp, "style1", strMouseStyle);

                var amount = CastAide.AsDouble(x.Element("Amount").Value);
                TemplateHandler.Assign(ref strTmp, "transactionid-val", x.Element("TransactionID").Value);
                TemplateHandler.Assign(ref strTmp, "date-val", CastAide.AsDateTime(x.Element("TransactionDate").Value).ToString("M/d/yyyy h:m"));
                TemplateHandler.Assign(ref strTmp, "status-val",x.Element("Status").Value);
                TemplateHandler.Assign(ref strTmp, "accountnumber-val", x.Element("AccountNO").Value);
                TemplateHandler.Assign(ref strTmp, "bankname-val", x.Element("NameOfBank").Value);
                TemplateHandler.Assign(ref strTmp, "bankaddress-val", x.Element("BankAddress").Value);
                TemplateHandler.Assign(ref strTmp, "bankcountry-val", x.Element("BankCountry").Value);
                TemplateHandler.Assign(ref strTmp, "bankcity-val", x.Element("BankCityTown").Value);
                TemplateHandler.Assign(ref strTmp, "currency-val", x.Element("Currency").Value);
                TemplateHandler.Assign(ref strTmp, "original-val", DNAide.Core.DataFormat.FormatNumber20(amount));
                TemplateHandler.Assign(ref strTmp, "usdeqv-val", Common.FormatNumber(amount * exrate));
                total += amount;

                strBuild.Append(strTmp);
                
            }
            TemplateHandler.Assign(ref strTpl, "total-val", Common.FormatNumber(total));
            TemplateHandler.ReplaceBlock(ref strTpl, "content", strBuild.ToString());
            
        }
        else
        {
            TemplateHandler.ReplaceBlock(ref strTpl, "content", Common.NoInfo(11));
            TemplateHandler.RemoveBlock(ref strTpl, "total");
        }
        
        content.InnerHtml = strTpl;
    }
    
}
