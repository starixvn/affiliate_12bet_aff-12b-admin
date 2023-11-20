using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Data;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class LandingPageRepository : BaseRepository, ILandingPageRepository
    {
        public IEnumerable<LandingPage> GetLandingPageList(int userId)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<LandingPage>(new { userId });
            }
        }

        public LandingPage GetLandingPageById(int userId, int pageId)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.QueryEntity<LandingPage>(new { pageId, userId });
            }
        }

        public IEnumerable<TargetCountry> GetExcludedTargetCountryList(ExcludedTargetCountryParam param)
        {
            const string SpKey = "Aff_LandingPage_ExcludedTargetCountry_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<TargetCountry>(param);
            }
        }

        public ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param)
        {
            const string SpKey = "Aff_Admin_LandingPage_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param)
        {
            const string SpKey = "Aff_Admin_LandingPage_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue(Field.PageId, param.PageId);
                AssignCommonParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteLandingPage(DeleteLandingPageParam param)
        {
            const string SpKey = "Aff_Admin_LandingPage_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("UserId", param.UserId);
                objectDb.SetParameterValue("PageIds", param.PageIds);
                objectDb.SetParameterValue("Ip", param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignCommonParam(IObjectDb objectDb, InsertUpdateLandingPageParam param)
        {
            objectDb.SetParameterValue("UserId", param.UserId);
            objectDb.SetParameterValue("PageName", param.PageName);
            objectDb.SetParameterValue("PageCode", param.PageCode);
            objectDb.SetParameterValue("ExcludedTargetCountry", param.ExcludedTargetCountries);
            objectDb.SetParameterValue("Ip", param.Ip);
        }
    }
}