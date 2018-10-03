using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public class LandingPageService : ILandingPageService
    {
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

        public ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            return _landingPageRepository.InsertLandingPage(param);
        }

        public ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            return _landingPageRepository.UpdateLandingPage(param);
        }

        public ResultInfo DeleteLandingPage(DeleteLandingPageParam param)
        {
            return _landingPageRepository.DeleteLandingPage(param);
        }
    }
}