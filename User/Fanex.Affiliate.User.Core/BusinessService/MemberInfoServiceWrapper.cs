using System.Xml;
using Fanex.Affiliate.User.Core.MemberInfoWebService;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class MemberInfoServiceWrapper : IMemberInfoServiceWrapper
    {
        private readonly ServiceSoap _service;

        public MemberInfoServiceWrapper(ServiceSoap service)
        {
            _service = service;
        }

        public XmlNode GetDepositInfo(string userName, string transDate)
        {
            return _service.DepositInfo(userName, transDate);
        }

        public XmlNode GetWithdrawalInfo(string userName, string transDate)
        {
            return _service.WithdrawalInfo(userName, transDate);
        }
    }
}
