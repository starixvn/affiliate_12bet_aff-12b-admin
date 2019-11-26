using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using System;
using Fanex.Affiliate.Admin.Core.Repository;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public class HomeService: BaseRepository, IHomeService
    {
        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            const string SpKey = "Aff_UserOnlineList";

            var result = new EntityResult<OnlineListInfo>();
            var dataList = new List<OnlineListInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("PageIndex", DbType.Int32, pageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, pageSize);

                objectDb.AddOutParameter("TotalPages", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new OnlineListInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            LoggedInDate = reader["LoggedIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LoggedIn"]),
                            LastRequestDate = reader["LastRequest"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastRequest"]),
                            Ip = Convert.ToString(reader["IP"]),
                            Role = Convert.ToString(reader["RoleName"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public decimal GetPSPToday()
        {
            const string SpKey = "DEP_AffiliateGetPSP";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter("PSP", DbType.Int32, Size.SizeOfCurrency);

                objectDb.ExecuteNonQuery();

                var psp = objectDb.GetParameterValue("PSP");
                return Convert.ToDecimal(psp);
            }
        }

        public DepositInfo GetDepositInfo()
        {
            const string SpKey = "DEP_AffiliateGeneralInfo";
            DepositInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new DepositInfo
                        {
                            AllMemberDepositUpToNow = reader["MemInProAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MemInProAllNum"]),
                            AllMemberDepositThisMonth = reader["MemInProMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MemInProMonthNum"])
                        };
                    }
                }
            }

            return result;
        }

        public GeneralInfo GetGeneralInfo()
        {
            const string SpKey = "Aff_Admin_GeneralInfo_Sel";
            GeneralInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildItem(reader);
                    }
                }
            }

            return result;
        }

        private GeneralInfo BuildItem(IDataReader reader)
        {
            var item = BuildItem_1(reader);
            item.PaymentRequestNumber = reader["PaymentReqNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PaymentReqNum"]);
            item.BannerNumber = reader["BannerNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["BannerNum"]);
            item.ImageBannerNumber = reader["ImageBannerNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ImageBannerNum"]);
            item.FlashBannerNumber = reader["FlashBannerNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FlashBannerNum"]);
            item.TextBannerNumber = reader["TextBannerNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TextBannerNum"]);
            item.AllAffiliateNumber = reader["AffInProAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AffInProAllNum"]);
            item.PendingAffiliateNumber = reader["PendingAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PendingAllNum"]);
            item.ApprovedAffiliateNumber = reader["ApprovedAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ApprovedAllNum"]);
            item.DisapprovedAffiliateNumber = reader["DisapprovedAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DisapprovedAllNum"]);

            return item;
        }

        public GeneralInfo BuildItem_1(IDataReader reader)
        {
            var item = BuildItem_2(reader);
            item.ClosedAffiliateNumber = reader["ClosedAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClosedAllNum"]);
            item.AllMemberDepositNumber = reader["DepositMemAllNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DepositMemAllNum"]);
            item.AllDepositNumber = reader["DepositAll"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DepositAll"]);
            item.AllWinlostNumber = reader["WinlostAll"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["WinlostAll"]);
            item.AllBonusNumber = reader["BonusAll"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BonusAll"]);
            item.AllPSPFeeNumber = reader["PSPFeeAll"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PSPFeeAll"]);
            item.AllNetWinlostNumber = reader["NetWinlostAll"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["NetWinlostAll"]);
            item.AllAffiliateMonthNumber = reader["AffInProMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AffInProMonthNum"]);
            item.PendingAffiliateMonthNumber = reader["PendingMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PendingMonthNum"]);

            return item;

        }

        public GeneralInfo BuildItem_2(IDataReader reader)
        {
            return new GeneralInfo
            {
                ApprovedAffiliateMonthNumber = reader["ApprovedMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ApprovedMonthNum"]),
                DisapprovedAffiliateMonthNumber = reader["DisapprovedMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DisapprovedMonthNum"]),
                ClosedAffiliateMonthNumber = reader["ClosedMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClosedMonthNum"]),
                AllMemberDepositMonthNumber = reader["DepositMemMonthNum"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DepositMemMonthNum"]),
                AllDepositMonthNumber = reader["DepositMonth"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DepositMonth"]),
                AllWinlostMonthNumber = reader["WinlostMonth"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["WinlostMonth"]),
                AllBonusMonthNumber = reader["BonusMonth"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["BonusMonth"]),
                AllPSPFeeMonthNumber = reader["PSPFeeMonth"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PSPFeeMonth"]),
                AllNetWinlostMonthNumber = reader["NetWinlostMonth"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["NetWinlostMonth"])
            };
        }
    }
}
