using Fanex.Affiliate.Admin.Core.Commands.LandingPage;
using Fanex.Affiliate.Admin.Core.Criterias.LandingPage;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Data.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class PageBuilderService : BaseDynamicService, IPageBuilderService
    {
        private readonly ISystemFile _systemFile;

        public PageBuilderService(IDynamicRepository dynamicRepository, ISystemFile systemFile) : base(dynamicRepository)
        {
            _systemFile = systemFile ?? throw new ArgumentNullException(nameof(systemFile));
        }

        public IEnumerable<MediaSourceInfo> GetMediaSources(MediaSourceParam param)
        {
            var criteria = param.MapTo<MediaSourceParam, GetMediaSourceCriteria>();

            return FetchTData<MediaSourceInfo>(criteria);
        }

        public PageBuilderDetail GetPageBuilderDetail(int pageId)
        {
            var criteria = new GetPageBuilderDetailCriteria { PageId = pageId };
            
            var result = DynamicRepository.FetchMultiple<PageBuilderDetail, ModuleComponentItem>(criteria);
            var pageDetail = result.Item1.FirstOrDefault();
            if(pageDetail != null)
            {
                pageDetail.ModuleComponentsSetting = result.Item2;
            }    

            return pageDetail;
        }

        public ResultInfo InsertPageBuilder(InsertUpdatePageBuilderParam param)
        {
            var command = param.MapTo<InsertUpdatePageBuilderParam, InsertPageBuilderCommand>();
            command.UserId = param.UserId;
            command.ComponentStructure = JsonConvert.SerializeObject(param.ModuleComponentsSetting);

            var result = ExecuteReturnResultInfo(command);
            result.Message = result.IsSuccessful ? command.PageId.ToString() : result.Message;

            return result;
        }

        public ResultInfo UpdatePageBuilder(InsertUpdatePageBuilderParam param)
        {
            var command = param.MapTo<InsertUpdatePageBuilderParam, UpdatePageBuilderCommand>();
            command.UserId = param.UserId;
            command.ComponentStructure = JsonConvert.SerializeObject(param.ModuleComponentsSetting);

            return ExecuteReturnResultInfo(command);
        }
    }
}
