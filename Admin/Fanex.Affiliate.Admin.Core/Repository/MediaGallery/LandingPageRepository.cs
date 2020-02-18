using System;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class LandingPageRepository : BaseRepository, ILandingPageRepository
    {
        public IEnumerable<LandingPage> GetLandingPageList()
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            var result = new List<LandingPage>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new LandingPage
                        {
                            PageId = Convert.ToInt32(reader[Field.PageId]),
                            PageName = Convert.ToString(reader["PageName"]),
                            PageCode = Convert.ToString(reader["PageCode"])
                        });
                    }
                }
            }

            return result;
        }

        public LandingPage GetLandingPageById(int pageId)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            LandingPage result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter(Field.PageId, DbType.Int32, pageId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new LandingPage
                        {
                            PageId = Convert.ToInt32(reader[Field.PageId]),
                            PageName = Convert.ToString(reader["PageName"]),
                            PageCode = Convert.ToString(reader["PageCode"])
                        };
                    }
                }
            }

            return result;
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
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter(Field.PageId, DbType.Int32, param.PageId);
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
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("PageIds", DbType.String, param.PageIds);
                objectDb.AddInParameter("Ip", DbType.String, param.Ip);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignCommonParam(IObjectDb objectDb, InsertUpdateLandingPageParam param)
        {
            objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
            objectDb.AddInParameter("PageName", DbType.String, param.PageName);
            objectDb.AddInParameter("PageCode", DbType.String, param.PageCode);
            objectDb.AddInParameter("Ip", DbType.String, param.Ip);
        }
    }
}