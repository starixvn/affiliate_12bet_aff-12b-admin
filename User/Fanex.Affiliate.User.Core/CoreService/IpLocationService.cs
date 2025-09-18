namespace Fanex.Affiliate.User.Core.CoreService
{
    using Criterias;
    using Data.Repository;
    using DTO;

    public class IpLocationService : BaseDynamicService, IIpLocationService
    {
        public IpLocationService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IpLocationInfo GetCountryFromIp(string ip)
        {
            return GetTData<IpLocationInfo>(new GetIpInfoCriteria(ip));
        }
    }
}
