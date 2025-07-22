using Fanex.Affiliate.Admin.Core.Commands.MediaGallery;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class LandingPageService : BaseDynamicService, ILandingPageService
    {
        private const string RestrictedCharacterPattern = "[;/\\?:@=&\"<>#%\\{\\}\\|\\\\\\^~\\[\\]`\\s+]";
        private const string RestrictedCharacterErrorMessage = "Special characters are not allowed:\n the blank/empty space and ; / ? : @ = & \" < > # % { } | \\ ^ ~ [ ] `";

        public LandingPageService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<LandingPage> GetLandingPageList()
        {
            return FetchTData<LandingPage>(new LandingPageCriteria());
        }

        public LandingPage GetLandingPageById(int pageId)
        {
            return GetTData<LandingPage>(new LandingPageCriteria { PageId = pageId });
        }

        public IEnumerable<TargetCountry> GetExcludedTargetCountryList(int pageId)
        {
            return FetchTData<TargetCountry>(new GetBlockTargetCountriesByLandingPageCriteria { PageId = pageId });
        }

        public ResultInfo DeleteLandingPage(DeleteLandingPageParam param)
        {
            var command = new DeleteLandingPageCommand
            {
                PageIds = param.PageIds,
                UserId = param.UserId,
                Ip = param.Ip,
            };

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return ResultInfo.GenerateErr(RestrictedCharacterErrorMessage);
            }

            return isInsert ? InsertLandingPage(param) : UpdateLandingPage(param);
        }

        private ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            var command = new InsertLandingPageCommand();
            AssignBaseLandingPageCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            var command = new UpdateLandingPageCommand
            {
                PageId = param.PageId,
            };
            AssignBaseLandingPageCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        private static bool HasRestrictedCharacter(string input)
        {
            return RegexHelper.IsMatch(input, RestrictedCharacterPattern);
        }

        private static void AssignBaseLandingPageCommand(BaseLandingPageCommand command, InsertUpdateLandingPageParam param)
        {
            command.PageCode = param.PageCode;
            command.PageName = param.PageName;
            command.BlockTargetCountry = param.BlockTargetCountry;
            command.IsMobile = param.IsMobile;
            command.IsExcluded = param.IsExcluded;
            command.DealIds = param.DealIds;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}