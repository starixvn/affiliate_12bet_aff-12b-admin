using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Data;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    class FavoriteRepository : BaseRepository, IFavoriteRepository
    {
        public ResultInfo UpdateFavorite(InsertUpdateFavoriteParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Favorite_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateFavoriteParam param)
        {
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter("AffId", DbType.Int32, param.AffId);
            objectDb.AddInParameter("IsFavorite", DbType.Boolean, param.IsFavorite);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }
    }
}
