using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IPageBuilderService
    {
        IEnumerable<MediaSourceInfo> GetMediaSources(MediaSourceParam param);
        ResultInfo InsertPageBuilder(InsertUpdatePageBuilderParam param);
        ResultInfo UpdatePageBuilder(InsertUpdatePageBuilderParam param);
        PageBuilderDetail GetPageBuilderDetail(int pageId);
    }
}
