using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;
using System.Data;

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
            throw new NotImplementedException();
        }

        public ResultInfo InsertBanner(InsertUpdateBannerParam param)
        {
            throw new NotImplementedException();
        }

        public ResultInfo UpdateBanner(InsertUpdateBannerParam param)
        {
            throw new NotImplementedException();
        }

        public ResultInfo DeleteBanners(DeleteBannerParam param)
        {
            throw new NotImplementedException();
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
                DeviceType = Convert.ToString(reader["DeviceType"])
            };
        }
    }
}
