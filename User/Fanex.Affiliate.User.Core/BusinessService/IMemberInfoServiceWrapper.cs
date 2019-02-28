using System.Xml;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IMemberInfoServiceWrapper
    {
        XmlNode GetDepositInfo(string userName, string transDate);

        XmlNode GetWithdrawalInfo(string userName, string transDate);
    }
}
