using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Newtonsoft.Json;
using System;
using System.Xml;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class XmlHelper
    {
        public static string ParseXmlToJson(string xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString) && xmlString.TrimStart().StartsWith("<"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);

                    var rootNode = doc.SelectSingleNode("root");
                    var childNodes = rootNode.ChildNodes;

                    if (childNodes.Count != 1)
                    {
                        return string.Empty;
                    }

                    return JsonConvert.SerializeXmlNode(doc);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex);

                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static PaymentApproveParam GetPaymentApproveParam(string xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString) && xmlString.TrimStart().StartsWith("<"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);

                    var rootNode = doc.SelectSingleNode("root");
                    var childNodes = rootNode.ChildNodes;

                    if (childNodes.Count != 0)
                    {
                        var item = childNodes.Item(0);
                        var attributes = item.Attributes;

                        return new PaymentApproveParam
                        {
                            UserId = Convert.ToInt32(attributes.GetNamedItem("UserID").Value),
                            Ip = attributes.GetNamedItem("IP").Value,
                            RefId = attributes.GetNamedItem("RefID").Value,
                            AffPayout = attributes.GetNamedItem("AffPayout").Value,
                            AffUserName = attributes.GetNamedItem("AffUserName").Value,
                            BallanceCustId = attributes.GetNamedItem("Ballance_CustID").Value,
                            PlayerCurrency = attributes.GetNamedItem("Player_Currency").Value,
                            PlayerIbcCustId = attributes.GetNamedItem("Player_IBCCustID").Value,
                        };
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex);

                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
