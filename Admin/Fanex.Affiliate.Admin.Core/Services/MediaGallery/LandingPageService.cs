using Fanex.Affiliate.Admin.Core.Constants;
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

        public IEnumerable<LandingPage> GetLandingPageList()
        {
            return _landingPageRepository.GetLandingPageList();
        }

        public LandingPage GetLandingPageById(int pageId)
        {
            return _landingPageRepository.GetLandingPageById(pageId);
        }

        public IEnumerable<TargetCountry> GetExcludeTargetCountryList(ExcludeTargetCountryParam param)
        {
            return _landingPageRepository.GetExcludeTargetCountryList(param).ExcludeEmpty();
        }

        public ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return ResultInfo.GenerateErr(RestricedCharacterErrorMessage);
            }

            return _landingPageRepository.InsertLandingPage(param);
        }

        public ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return ResultInfo.GenerateErr(RestricedCharacterErrorMessage);
            }

            return _landingPageRepository.UpdateLandingPage(param);
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