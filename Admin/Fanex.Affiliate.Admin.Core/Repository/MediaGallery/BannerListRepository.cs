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
    public class BannerListRepository : BaseRepository, IBannerListRepository
    {
        public EntityResult<BannerInfo> GetBannerList(BannerParam param)
        {
            const string SpKey = "Aff_Admin_Banner_Sel";
            var result = new EntityResult<BannerInfo>();
            var dataList = new List<BannerInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.SetParameterValue("languages", param.Languages);
                objectDb.SetParameterValue("category", param.Category);
                objectDb.SetParameterValue("subcategory", param.SubCategory);
                objectDb.SetParameterValue("currency", param.Currency);
                objectDb.SetParameterValue("ImageType", param.ImageType);
                objectDb.SetParameterValue("Size", param.Size);
                objectDb.SetParameterValue("AffType", param.AffType);
                objectDb.SetParameterValue("devicetype", param.DeviceType);
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
                        bannerInfo.NumberOfCopies = 1000;
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
                objectDb.SetParameterValue("Category", param.Category);
                objectDb.SetParameterValue("SubCategory", param.SubCategory);
                objectDb.SetParameterValue("Languages", param.Languages);
                objectDb.SetParameterValue("Currency", param.Currency);
                objectDb.SetParameterValue("ImageType", param.ImageType);
                objectDb.SetParameterValue("AffType", param.AffType);
                objectDb.SetParameterValue("DeviceType", param.DeviceType);

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
            objectDb.SetParameterValue("Categories", param.Categories);
            objectDb.SetParameterValue("SubCategories", param.SubCategories);
            objectDb.SetParameterValue("Language", param.Language);
            objectDb.SetParameterValue("Currencies", param.Currencies);
            objectDb.SetParameterValue("AffTypes", param.AffTypes);
            objectDb.SetParameterValue("DeviceTypes", param.DeviceTypes);
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
                Categories = Convert.ToString(reader["Categories"]).Split(','),
                SubCategories = Convert.ToString(reader["SubCategories"]).Split(','),
                Language = Convert.ToString(reader["Language"]),
                Currencies = Convert.ToString(reader["Currencies"]).Split(','),
                ImageType = Convert.ToString(reader["ImageType"]),
                Size = Convert.ToString(reader["Size"]),
                DeviceTypes = reader.GetVal<string>("DeviceTypes").Split(',')
            };
        }
    }
}
