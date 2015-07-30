using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNAide.Core;
using System.Xml.Linq;

public partial class UserInfo_TestApi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var user = CastAide.AsString(Request["username"], String.Empty);
        var date = CastAide.AsDateTime(Request["date"], DateTime.Now);
        //var service = new MemberInfoService.Service();
        //var isdeposit = CastAide.AsDouble(Request["isdeposit"], 1) == 1;

        //var xmlNode = isdeposit ? service.DepositInfo(user, date.ToShortDateString())
        //          : service.WithdrawalInfo(user, date.ToShortDateString());
        //var xml = XElement.Parse(xmlNode.OuterXml);
        //output.InnerHtml = "<pre>" + xml.Element("ReturnCode").Value + "</pre>";
    }

    public static string ToString(System.Xml.XmlNode node, int indentation)
    {
        using (var sw = new System.IO.StringWriter())
        {
            using (var xw = new System.Xml.XmlTextWriter(sw))
            {
                xw.Formatting = System.Xml.Formatting.Indented;
                xw.Indentation = indentation;
                node.WriteContentTo(xw);
            }
            return sw.ToString();
        }
    }
}