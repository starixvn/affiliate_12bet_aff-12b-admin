using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Repository.Home
{
    public class HomeRepository : BaseRepository, IHomeRepository
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
                            LoggedInDate = Convert.ToDateTime(reader["LoggedIn"]),
                            LastRequestDate = Convert.ToDateTime(reader["LastRequest"]),
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
                            UpToNowDeposit = Convert.ToInt32(reader["MemInProAllNum"]),
                            ThisMonthDeposit = Convert.ToInt32(reader["MemInProMonthNum"])
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
                            ClosedAffiliateNumber = Convert.ToInt32(reader["DepositMemAllNum"]),
                            AllMemberDepositNumber = Convert.ToInt32(reader["ClosedAllNum"]),
                            AllDepositNumber = Convert.ToInt32(reader["DepositAll"]),
                            AllWinlostNumber = Convert.ToInt32(reader["WinlostAll"]),
                            AllBonusNumber = Convert.ToInt32(reader["BonusAll"]),
                            AllPSPFeeNumber = Convert.ToInt32(reader["PSPFeeAll"]),
                            AllAdjustmentNumber = Convert.ToInt32(reader["AdjustmentAll"]),
                            AllNetWinlostNumber = Convert.ToInt32(reader["NetWinlostAll"]),
                            AllAffiliateMonthNumber = Convert.ToInt32(reader["AffInProMonthNum"]),
                            PendingAffiliateMonthNumber = Convert.ToInt32(reader["PendingMonthNum"]),
                            ApprovedAffiliateMonthNumber = Convert.ToInt32(reader["ApprovedMonthNum"]),
                            DisapprovedAffiliateMonthNumber = Convert.ToInt32(reader["DisapprovedMonthNum"]),
                            ClosedAffiliateMonthNumber = Convert.ToInt32(reader["DepositMemMonthNum"]),
                            DepositMemberMonthNumber = Convert.ToInt32(reader["ClosedMonthNum"]),
                            AllDepositMonthNumber = Convert.ToInt32(reader["DepositMonth"]),
                            AllWinlostMonthNumber = Convert.ToInt32(reader["WinlostMonth"]),
                            AllBonusMonthNumber = Convert.ToInt32(reader["BonusMonth"]),
                            AllPSPFeeMonthNumber = Convert.ToInt32(reader["PSPFeeMonth"]),
                            AllAdjustmentMonthNumber = Convert.ToInt32(reader["AdjustmentMonth"]),
                            AllNetWinlostMonthNumber = Convert.ToInt32(reader["NetWinlostMonth"])
                        };
                    }
                }
            }

            return result;
        }
    }
}
