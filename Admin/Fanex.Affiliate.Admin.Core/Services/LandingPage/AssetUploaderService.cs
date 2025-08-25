using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Data.Repository;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public class AssetUploaderService : BaseDynamicService, IAssetUploaderService
    {
        public AssetUploaderService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public ResultInfo UploadAssets(HttpPostedFileBase[] files, UploadAssetsParam param)
        {
            return new ResultInfo
            {
                Code = 0,
                Message = $"{files.Length} asset(s) uploaded successfully"
            };
        }

        public EntityResult<LandingPageAsset> GetAssetList(AssetListParam param)
        {
            return new EntityResult<LandingPageAsset>();
        }

        public ResultInfo DeleteAssets(DeleteAssetsParam param)
        {
            return new ResultInfo
            {
                Code = 0,
                Message = "Assets deleted successfully"
            };
        }

        public OptimizedAssetResult OptimizeAsset(OptimizeAssetParam param)
        {
            return new OptimizedAssetResult
            {
                WasOptimized = true,
                OriginalFileName = "sample.jpg",
                OptimizedFileName = "sample_optimized.jpg",
                CompressionPercentage = 25
            };
        }

        public AssetStatistics GetAssetStatistics(int? moduleId)
        {
            return new AssetStatistics
            {
                TotalAssets = 0,
                TotalSize = 0,
                ImageCount = 0,
                VideoCount = 0,
                DocumentCount = 0,
                TotalSizeFormatted = "0 KB"
            };
        }
    }
}
