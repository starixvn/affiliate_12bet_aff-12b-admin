using Fanex.Affiliate.Admin.Core.Commands.MediaGallery;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO;
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
        private const string RestricedCharacterErrorMessage = "Special characters are not allowed:\n the blank/empty space and ; / ? : @ = & \" < > # % { } | \\ ^ ~ [ ] `";

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

        public ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = RestricedCharacterErrorMessage
                };
            }

            var command = new InsertLandingPageCommand();
            AssignBaseLandingPageCommand(command, param);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            if (HasRestrictedCharacter(param.PageCode))
            {
                return new ResultInfo
                {
                    Code = ErrCode.GeneralErrorCode,
                    Message = RestricedCharacterErrorMessage
                };
            }

            var command = new UpdateLandingPageCommand
            {
                PageId = param.PageId,
            };
            AssignBaseLandingPageCommand(command, param);

            return ExecuteReturnResultInfo(command);
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

        private static bool HasRestrictedCharacter(string input)
        {
            return RegexHelper.IsMatch(input, RestrictedCharacterPattern);
        }

        private static void AssignBaseLandingPageCommand(BaseLandingPageCommand command, InsertUpdateLandingPageParam param)
        {
            command.PageCode = param.PageCode;
            command.PageName = param.PageName;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }
    }
}