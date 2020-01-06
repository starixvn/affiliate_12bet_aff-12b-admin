using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Data;
using Fanex.Data;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class BannerListRepository : BaseRepository, IBannerListRepository
    {
        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Sel";
            var result = new EntityResult<BannerInfo>();
            var dataList = new List<BannerInfo>();

            using (var objectDb = GetObjectDb(SpKey))
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

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bannerInfo = BuildBannerInfoItem(reader);
                        bannerInfo.LastUpdate = reader["LastUpdate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastUpdate"]);

                        dataList.Add(bannerInfo);
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
            const string SpKey = "Aff_Admin_Banner_Sel";
            BannerInfo item = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("bannerid", DbType.Int32, selectedBannerId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = BuildBannerInfoItem(reader);
                        item.AffType = reader.GetVal<byte>("AffType");
                    }
                }
            }

            return item;
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Upd";

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
            const string SpKey = "Aff_Admin_Banner_Del";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("BannerIds", DbType.String, param.BannerIdList);
                objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
                objectDb.AddInParameter("IP", DbType.String, param.Ip);

                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public IEnumerable<MediaGalleryFilterItem> GetSizeList(SizeParam param)
        {
            const string SpKey = "Aff_Admin_MediaSize_Sel";
            var result = new List<MediaGalleryFilterItem>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("Category", DbType.String, param.Category);
                objectDb.AddInParameter("Language", DbType.String, param.Language);
                objectDb.AddInParameter("Currency", DbType.String, param.Currency);
                objectDb.AddInParameter("ImageType", DbType.String, param.ImageType);
                objectDb.AddInParameter("AffType", DbType.String, param.AffType);
                objectDb.AddInParameter("DeviceType", DbType.String, param.DeviceType);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MediaGalleryFilterItem
                        {
                            Id = Convert.ToInt32(reader["SizeId"]),
                            Name = Convert.ToString(reader["Size"])
                        });
                    }
                }
            }

            return result;
        }

        private static void AssignParam(IObjectDb objectDb, InsertUpdateBannerParam param)
        {
            objectDb.AddInParameter("filename", DbType.String, param.FilePath);
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
                DeviceType = reader.GetVal<byte>("DeviceType")
            };
        }
    }
}
