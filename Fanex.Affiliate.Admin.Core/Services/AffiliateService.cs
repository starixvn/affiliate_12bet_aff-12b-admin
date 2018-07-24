using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.Enum;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Core;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepository _affRepository;

        private readonly ISessionService _sessionService;

        private readonly IAppSetting _appSetting;

        public AffiliateService(ISessionService sessionService, IAppSetting appSetting, IAffiliateRepository affRepository)
        {
            _sessionService = sessionService;
            _appSetting = appSetting;
            _affRepository = affRepository;
        }

        public ResultInfo UpdateInfo(LeftPanelUpdateInfoParam param)
        {
            return _affRepository.UpdateInfo(param);
        }

        public ResultInfo ChangePassword(LeftPanelUpdateInfoParam param)
        {
            param.Password = CryptographyHelper.Encrypt(param.Password);

            return _affRepository.UpdateInfo(param);
        }

        public AffLeftPanelInfo GetInfo(string username)
        {
            var leftPanelInfo = _affRepository.GetInfo(username);
            var generalInfo = leftPanelInfo.GeneralInfo;

            if (generalInfo != null)
            {
                AssignSession(generalInfo);

                generalInfo.InternalUrl = GenerateInternalUrl(username, _sessionService.SelectedAffType);
            }

            return leftPanelInfo;
        }

        private void AssignSession(AffInfo affInfo)
        {
            _sessionService.SelectedAffId = affInfo.AffId;
            _sessionService.SelectedAffUserName = affInfo.UserName;
            _sessionService.SelectedAffType = (AffType)affInfo.AffType;
        }

        private string GenerateInternalUrl(string affUserName, AffType type)
        {
            var randomString = Generator.GetRandomString(10);
            var encryptedKey = Generator.GenerateSHA512String(_sessionService.SessionId + randomString);
            var custId = _sessionService.CustId;
            var onUserId = _sessionService.OnUserId;
            var internalUrl = type == AffType.European ? _appSetting.InternalEuropeanAffiliateUrl : _appSetting.InternalAsianAffiliateUrl;

            return $"{internalUrl}?chalengeStr={randomString}&accKey={encryptedKey}&custName={affUserName}&accId={custId}&accOnId={onUserId}";
        }
    }
}