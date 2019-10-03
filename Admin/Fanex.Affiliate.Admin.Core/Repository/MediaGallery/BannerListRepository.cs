using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Data;
using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class BannerListRepository : BaseRepository, IBannerListRepository
    {
        public EntityResult<BannerInfo> GetBannerList(BannerParam param, string spKey)
        {
            var result = new EntityResult<BannerInfo>();
            var dataList = new List<BannerInfo>();

            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.AddInParameter("category", DbType.String, param.Category);
                objectDb.AddInParameter("language", DbType.String, param.Language);
                objectDb.AddInParameter("currency", DbType.String, param.Currency);
                objectDb.AddInParameter("ImageType", DbType.String, param.ImageType);
                objectDb.AddInParameter("Size", DbType.String, param.Size);
                objectDb.AddInParameter("AffType", DbType.Byte, param.AffType);
                objectDb.AddInParameter("devicetype", DbType.Byte, param.DeviceType);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildBannerInfoItem(reader));
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            string SpKey = "Aff_BannerSelDetail_1";
            BannerInfo item = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("bannerid", DbType.Int32, selectedBannerId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = BuildBannerInfoItem(reader);
                        item.AffType = Convert.ToByte(reader["AffType"]);
                    }
                }
            }

            return item;
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            const string SpKey = "Aff_BannerMediaIns";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter("bannerid", DbType.Int32, 4);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            const string SpKey = "Aff_BannerUpd_1";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("bannerid", DbType.Int32, param.BannerId);
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            const string SpKey = "Aff_BannerDel_1";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("ListBannerID", DbType.String, param.BannerIdList);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateBannerParam param)
        {
            objectDb.AddInParameter("filename", DbType.String, param.FileName);
            objectDb.AddInParameter("category", DbType.String, param.Category);
            objectDb.AddInParameter("language", DbType.String, param.Language);
            objectDb.AddInParameter("currency", DbType.String, param.Currency);
            objectDb.AddInParameter("size", DbType.String, param.Size);
            objectDb.AddInParameter("afftype", DbType.Byte, param.AffType);
            objectDb.AddInParameter("devicetype", DbType.Byte, param.DeviceType);
            objectDb.AddInParameter("imagetype", DbType.String, param.ImageType);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            AssignResultOutputParam(objectDb);
        }

        private static BannerInfo BuildBannerInfoItem(IDataReader reader)
        {
            return new BannerInfo
            {
                BannerId = Convert.ToInt32(reader["BannerID"]),
                FileName = Convert.ToString(reader["FileName"]),
                Category = Convert.ToString(reader["Category"]),
                Language = Convert.ToString(reader["Language"]),
                Currency = Convert.ToString(reader["Currency"]),
                ImageType = Convert.ToString(reader["ImageType"]),
                Size = Convert.ToString(reader["Size"]),
                DeviceType = Convert.ToString(reader["DeviceType"]),
            };
        }
    }
}
