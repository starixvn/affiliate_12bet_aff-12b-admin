using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class OfferPageRepository : BaseRepository, IOfferPageRepository
    {
        public ResultInfo DeleteOfferPage(DeleteOfferPageParam param)
        {
            const string spKey = "Aff_Admin_OfferPage_Del";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public OfferPage GetOfferPageById(int pageId)
        {
            const string spKey = "Aff_Admin_OfferPage_Sel";
            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.QueryEntity<OfferPage>(new { pageId });
            }
        }

        public EntityResult<OfferPage> GetOfferPageList(SelectOfferPageParam param)
        {
            const string spKey = "Aff_Admin_OfferPage_Sel";
            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
                var items = objectDb.Query<OfferPage>(param);

                return new EntityResult<OfferPage>
                {
                    MainData = items,
                    TotalItem = GetTotalRow(objectDb)
                };
            }
        }

        public ResultInfo InsertOfferPage(InsertOfferPageParam param)
        {
            const string spKey = "Aff_Admin_OfferPage_Ins";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateOfferPage(UpdateOfferPageParam param)
        {
            const string spKey = "Aff_Admin_OfferPage_Upd";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }
    }
}
