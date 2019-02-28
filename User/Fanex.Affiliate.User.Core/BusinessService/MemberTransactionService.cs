using System.Collections.Generic;
using System.Xml.Linq;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class MemberTransactionService : IMemberTransactionService
    {
        private readonly IMemberInfoServiceWrapper _serviceWrapper;

        public MemberTransactionService(IMemberInfoServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        public IEnumerable<Transaction> GetTransactionInfo(MemberTransactionParam param)
        {
            var transactionList = new List<Transaction>();

            var xmlNode = param.IsDeposit ?
                _serviceWrapper.GetDepositInfo(param.UserName, param.Date.Value.ToShortDateString()) :
                _serviceWrapper.GetWithdrawalInfo(param.UserName, param.Date.Value.ToShortDateString());

            var xml = XElement.Parse(xmlNode.OuterXml);

            if (xml.Element("ReturnCode").Value == "0")
            {
                var elementName = param.IsDeposit ? "DepositInfo" : "WithdrawalInfo";
                foreach (var x in xml.Elements(elementName))
                {
                    transactionList.Add(new Transaction
                    {
                        TransactionId = x.Element("TransactionID").Value,
                        OriginalAmount = x.Element("Amount").Value,
                        OriginalTransactionDate = x.Element("TransactionDate").Value,
                        Status = x.Element("Status").Value,
                        AccountNo = x.Element("AccountNO").Value,
                        NameOfBank = x.Element("NameOfBank").Value,
                        BankAddress = x.Element("BankAddress").Value,
                        BankCountry = x.Element("BankCountry").Value,
                        BankCityTown = x.Element("BankCityTown").Value,
                        Currency = x.Element("Currency").Value
                    });
                }
            }

            return transactionList;
        }
    }
}