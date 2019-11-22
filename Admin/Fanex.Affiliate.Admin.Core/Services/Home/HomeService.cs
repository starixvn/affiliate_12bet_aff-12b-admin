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
                            AllMemberDepositUpToNow = Convert.ToInt32(reader["MemInProAllNum"]),
                            AllMemberDepositThisMonth = Convert.ToInt32(reader["MemInProMonthNum"])
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
                        result = new GeneralInfo
                        {
                            PaymentRequestNumber = Convert.ToInt32(reader["PaymentReqNum"]),
                            BannerNumber = Convert.ToInt32(reader["BannerNum"]),
                            ImageBannerNumber = Convert.ToInt32(reader["ImageBannerNum"]),
                            FlashBannerNumber = Convert.ToInt32(reader["FlashBannerNum"]),
                            TextBannerNumber = Convert.ToInt32(reader["TextBannerNum"]),
                            AllAffiliateNumber = Convert.ToInt32(reader["AffInProAllNum"]),
                            PendingAffiliateNumber = Convert.ToInt32(reader["PendingAllNum"]),
                            ApprovedAffiliateNumber = Convert.ToInt32(reader["ApprovedAllNum"]),
                            DisapprovedAffiliateNumber = Convert.ToInt32(reader["DisapprovedAllNum"]),
                            ClosedAffiliateNumber = Convert.ToInt32(reader["ClosedAllNum"]),
                            AllMemberDepositNumber = Convert.ToInt32(reader["DepositMemAllNum"]),
                            AllDepositNumber = Convert.ToDecimal(reader["DepositAll"]),
                            AllWinlostNumber = Convert.ToDecimal(reader["WinlostAll"]),
                            AllBonusNumber = Convert.ToDecimal(reader["BonusAll"]),
                            AllPSPFeeNumber = Convert.ToDecimal(reader["PSPFeeAll"]),
                            AllNetWinlostNumber = Convert.ToDecimal(reader["NetWinlostAll"]),
                            AllAffiliateMonthNumber = Convert.ToInt32(reader["AffInProMonthNum"]),
                            PendingAffiliateMonthNumber = Convert.ToInt32(reader["PendingMonthNum"]),
                            ApprovedAffiliateMonthNumber = Convert.ToInt32(reader["ApprovedMonthNum"]),
                            DisapprovedAffiliateMonthNumber = Convert.ToInt32(reader["DisapprovedMonthNum"]),
                            ClosedAffiliateMonthNumber = Convert.ToInt32(reader["ClosedMonthNum"]),
                            AllMemberDepositMonthNumber = Convert.ToInt32(reader["DepositMemMonthNum"]),
                            AllDepositMonthNumber = Convert.ToDecimal(reader["DepositMonth"]),
                            AllWinlostMonthNumber = Convert.ToDecimal(reader["WinlostMonth"]),
                            AllBonusMonthNumber = Convert.ToDecimal(reader["BonusMonth"]),
                            AllPSPFeeMonthNumber = Convert.ToDecimal(reader["PSPFeeMonth"]),
                            AllNetWinlostMonthNumber = Convert.ToDecimal(reader["NetWinlostMonth"])
                        };
                    }
                }
            }

            return result;
        }
    }
}
