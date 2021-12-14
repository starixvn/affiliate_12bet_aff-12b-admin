using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class ExclusivePageService : IExclusivePageService
    {
        private readonly IExclusivePageRepository _exclusivePageRepository;

        public ExclusivePageService(IExclusivePageRepository exclusivePageRepository)
        {
            _exclusivePageRepository = exclusivePageRepository;
        }

        public ResultInfo DeleteExclusivePage(DeleteExclusivePageParam param)
        {
            return _exclusivePageRepository.DeleteExclusivePage(param);
        }

        public ExclusivePage GetExclusivePageById(int exclusivePageId)
        {
            return _exclusivePageRepository.GetExclusivePageById(exclusivePageId);
        }

        public EntityResult<ExclusivePage> GetExclusivePageList(SelectExclusivePageParam param)
        {
            return _exclusivePageRepository.GetExclusivePageList(param);
        }

        public ResultInfo InsertExclusivePage(HttpPostedFileBase file, InsertExclusivePageParam param)
        {
            return _exclusivePageRepository.InsertExclusivePage(param);
        }

        public ResultInfo UpdateExclusivePage(HttpPostedFileBase file, UpdateExclusivePageParam param)
        {
            return _exclusivePageRepository.UpdateExclusivePage(param);
        }
    }
}
