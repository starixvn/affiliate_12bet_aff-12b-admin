using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class OfferAffRepository : BaseRepository, IOfferAffRepository
    {
        public ResultInfo DeleteOfferAff(DeleteOfferAffParam param)
        {
            const string spKey = "Aff_Admin_OfferAffiliate_Del";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public EntityResult<OfferAff> GetOfferAffs(SelectOfferAffParam param)
        {
            const string spKey = "Aff_Admin_OfferAffiliate_Sel";
            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.SetParameterValue("NRows", DefaultValue.Int);
                var items = objectDb.Query<OfferAff>(param);

                return new EntityResult<OfferAff>
                {
                    MainData = items,
                    TotalItem = GetTotalRow(objectDb)
                };
            }
        }

        public ResultInfo InsertOfferAff(InsertOfferAffParam param)
        {
            const string spKey = "Aff_Admin_OfferAffiliate_Ins";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }
    }
}
