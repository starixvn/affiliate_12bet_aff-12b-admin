using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class LandingPageService : ILandingPageService
    {
        private const string RestrictedCharacterPattern = "[;\\?:@=&\"<>#%\\{\\}\\|\\\\\\^~\\[\\]`\\s+]";
        private const string RestricedCharacterErrorMessage = "Special characters are not allowed:\n the blank/empty space and ; ? : @ = & \" < > # % { } | \\ ^ ~ [ ] `";

        private readonly ILandingPageRepository _landingPageRepository;

        public LandingPageService(ILandingPageRepository landingPageRepository)
        {
            _landingPageRepository = landingPageRepository;
        }

        public IEnumerable<LandingPage> GetLandingPageList(int userId)
        {
            return _landingPageRepository.GetLandingPageList(userId);
        }

        public LandingPage GetLandingPageById(int userId, int pageId)
        {
            return _landingPageRepository.GetLandingPageById(userId, pageId);
        }

        public IEnumerable<TargetCountry> GetExcludedTargetCountryList(ExcludedTargetCountryParam param)
        {
            return _landingPageRepository.GetExcludedTargetCountryList(param).ExcludeEmpty();
        }

        public ResultInfo HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return ResultInfo.GenerateErr(RestricedCharacterErrorMessage);
            }

            return isInsert ? _landingPageRepository.InsertLandingPage(param) : _landingPageRepository.UpdateLandingPage(param);
        }

        public ResultInfo DeleteLandingPage(DeleteLandingPageParam param)
        {
            return _landingPageRepository.DeleteLandingPage(param);
        }

        private static bool HasRestrictedCharacter(string input)
        {
            return Regex.IsMatch(input, RestrictedCharacterPattern);
        }
    }
}