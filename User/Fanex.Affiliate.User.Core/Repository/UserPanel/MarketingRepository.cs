using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MarketingRepository : IMarketingRepository
    {
        private readonly IObjectDBBuilder _builder;

        public MarketingRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(SpKey, dbMode))
            {
                return db.Query<LandingPage>();
            }
        }

        public IEnumerable<TargetProduct> GetTargetProductList(bool isInternal)
        {
            const string uSpKey = "Aff_TargetProduct_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<TargetProduct>();
            }
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaSize_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<SizeType>(param);
            }
        }

        public IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountryCode, bool isInternal)
        {
            const string uSpKey = "Aff_User_Domain_Sel";

            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<Domain>(new { targetCountryCode });
            }
        }

        public IEnumerable<Banner> GetBannerList(MarketingBannerParam bannerParam)
        {
            const string uSpKey = "Aff_Admin_Banner_Sel";

            var dbMode = bannerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<Banner>(bannerParam);
            }
        }

        public IEnumerable<LandingPage> GetLandingPageListByTargetCountry(string targetCountryCode, bool isInternal)
        {
            const string SpKey = "Aff_User_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(SpKey, dbMode))
            {
                return db.Query<LandingPage>(new { targetCountryCode });
            }
        }

        public ResultInfo CountNumberOfCopies(CopiedBannerCounterParam param)
        {
            const string uSpKey = "Aff_User_BannerStatistics_Ins";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(param);
            }

            return new ResultInfo
            {
                IsOk = param.ErrCode == 0,
                Message = param.ErrMsg
            };
        }
    }
}