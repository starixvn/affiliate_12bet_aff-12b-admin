using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class BannerRepository : BaseRepository, IBannerRepository
    {
        public IEnumerable<TargetProduct> GetTargetProductList()
        {
            const string SpKey = "Aff_TargetProduct_Sel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                return objectDb.Query<TargetProduct>();
            }
        }

        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Sel";
            var result = new EntityResult<BannerInfo>();
            var dataList = new List<BannerInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("languages", param.Languages);
                objectDb.SetParameterValue("targetProductId", param.TargetProductId);
                objectDb.SetParameterValue("Size", param.Size);
                objectDb.SetParameterValue("AffType", param.AffType);
                objectDb.SetParameterValue("CopiedFrom", param.FromDate);
                objectDb.SetParameterValue("CopiedTo", param.ToDate);
                objectDb.SetParameterValue("PageIndex", param.PageIndex);
                objectDb.SetParameterValue("PageSize", param.PageSize);

                objectDb.SetParameterValue("NRows", DefaultValue.Int);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bannerInfo = BuildBannerInfoItem(reader);
                        bannerInfo.TargetProductNames = Convert.ToString(reader["TargetProductNames"]).Split(',');
                        bannerInfo.AffTypes = reader.GetVal<string>("AffTypeNames").Split(',');
                        bannerInfo.NumberOfCopies = Convert.ToInt32(reader["NumberOfCopies"]);
                        bannerInfo.LastUpdate = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]);

                        dataList.Add(bannerInfo);
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public BannerInfo GetBannerDetail(int selectedBannerId)
        {
            const string SpKey = "Aff_Admin_Banner_Sel";
            BannerInfo item = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("bannerid", selectedBannerId);
                objectDb.SetParameterValue("languages", string.Empty);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = BuildBannerInfoItem(reader);
                        item.TargetProductIds = Convert.ToString(reader["TargetProductIds"]).Split(',');
                        item.AffTypes = reader.GetVal<string>("AffTypes").Split(',');
                    }
                }
            }

            return item;
        }

        public ResultInfo InsertBanner(InsertUpdateMultipleBannersParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Ins";

            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignParam(objectDb, param);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateBanner(InsertUpdateMultipleBannersParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Upd";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("bannerid", param.BannerId);
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
                objectDb.SetParameterValue("BannerIds", param.BannerIdList);
                objectDb.SetParameterValue("UserID", param.UserId);
                objectDb.SetParameterValue("IP", param.Ip);

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
                objectDb.SetParameterValue("TargetProductId", param.TargetProductId);
                objectDb.SetParameterValue("Languages", param.Languages);
                objectDb.SetParameterValue("AffType", param.AffType);

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

        private static void AssignParam(IObjectDb objectDb, InsertUpdateMultipleBannersParam param)
        {
            objectDb.SetParameterValue("Files", param.Files);
            objectDb.SetParameterValue("TargetProductIds", param.TargetProductIds);
            objectDb.SetParameterValue("Language", param.Language);
            objectDb.SetParameterValue("AffTypes", param.AffTypes);
            objectDb.SetParameterValue("UserID", param.UserId);
            objectDb.SetParameterValue("IP", param.Ip);

            AssignResultOutputParam(objectDb);
        }

        private static BannerInfo BuildBannerInfoItem(IDataReader reader)
        {
            return new BannerInfo
            {
                BannerId = Convert.ToInt32(reader["BannerID"]),
                FileName = Convert.ToString(reader["FileName"]),
                Language = Convert.ToString(reader["Language"]),
                Size = Convert.ToString(reader["Size"])
            };
        }
    }
}
